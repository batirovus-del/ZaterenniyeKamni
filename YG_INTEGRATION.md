# Интеграция PluginYG v1.6 — Справочник

## 1. Архитектура

```
YandexGame (singleton, DontDestroyOnLoad)
  ├── InfoYG (ScriptableObject) — настройки плагина
  ├── SavesYG — игровые сохранения
  └── YandexAdManager — проектная обёртка (делегирует в YandexGame)
```

Все вызовы идут через статические методы `YandexGame.*`.

---

## 2. Реклама

### 2.1 Полноэкранная (Interstitial)

**Проверка и показ:**
```csharp
// Проверка: реклама не идёт + таймер готов
if (!YandexGame.nowAdsShow && YandexGame.timerShowAd >= YandexGame.Instance.infoYG.fullscreenAdInterval)
{
    YandexGame.FullscreenShow(OnStart, OnClose);
}
```

**Методы:**
| Сигнатура | Назначение |
|---|---|
| `FullscreenShow(Action OnStart, Action OnClose)` | Показать межстраничную рекламу |
| `YandexGame.timerShowAd` | float — таймер с последнего показа |
| `YandexGame.nowFullAd` | bool — идёт ли сейчас полноэкранная |
| `YandexGame.nowAdsShow` | bool = `nowFullAd \|\| nowVideoAd` |

**События в UnityEvent (инспектор YandexGame):**
- `OpenFullscreenAd` — реклама открылась
- `CloseFullscreenAd` — реклама закрылась
- `ErrorFullscreenAd` — ошибка показа

**Статические события (C# Action):**
- `YandexGame.OpenFullAdEvent`
- `YandexGame.CloseFullAdEvent`
- `YandexGame.ErrorFullAdEvent`

**Editor-симуляция** (в `CallingAnEvent.cs`):
- Создаёт Canvas с зелёным оверлеем на `durationOfAdSimulation` секунд
- Задержка: `loadAdWithDelaySimulation` (сек)
- Настройки в `Assets/YandexGame/WorkingData/InfoYG.asset`

### 2.2 Реклама с наградой (Rewarded)

**Показ:**
```csharp
YandexGame.RewVideoShow(int id, Action onReward);
// id — идентификатор награды (0 по умолчанию)
// onReward — колбэк при успешном просмотре
```

**Методы:**
| Сигнатура | Назначение |
|---|---|
| `RewVideoShow(int id, Action onReward)` | Показать rewarded |
| `YandexGame.nowVideoAd` | bool — идёт ли сейчас rewarded |

**События:**
- `OpenVideoAd`, `CloseVideoAd`, `RewardVideoAd`, `ErrorVideoAd` — UnityEvent
- `OpenVideoEvent`, `CloseVideoEvent`, `RewardVideoEvent`, `ErrorVideoEvent` — Action

**Editor-симуляция:**
- Синий оверлей (или красный, если `testErrorOfRewardedAdsInEditor = true`)
- Время просмотра: `durationOfAdSimulation`
- Награда приходит через `RewardVideo(id)` если `Time.unscaledTime > timeOnOpenRewardedAds + 2`
- `rewardedAfterClosing` (InfoYG) — награда после закрытия или сразу

### 2.3 Sticky Banner
```csharp
YandexGame.StickyAdActivity(bool activity); // Вкл/Выкл
```

### 2.4 Ad Notification
```csharp
YandexGame.onAdNotification; // Action — вызывается перед любым показом
```

### 2.5 TimerBeforeAdsYG
- Компонент на сцене: отсчёт 3-2-1 перед рекламой
- Использует `YandexGame.timerShowAd` и `YandexGame.FullscreenShow()`
- Работает в реальном времени (`realtimeSeconds`)

---

## 3. Платежи (IAP)

**Модуль:** `Assets/YandexGame/ScriptsYG/Payments/`

**Данные покупки (`Purchase`):**
```csharp
public class Purchase {
    public string id;
    public string title;
    public string description;
    public string imageURI;
    public string priceValue;
    public bool consumed;
}
```

**Методы:**
| Сигнатура | Назначение |
|---|---|
| `YandexGame.BuyPayments(string id)` | Совершить покупку |
| `YandexGame.GetPayments()` | Получить список покупок |
| `YandexGame.PurchaseByID(string ID)` | Найти покупку по id |
| `YandexGame.ConsumePurchaseByID(string id)` | Потратить (consumed = true) |
| `YandexGame.ConsumePurchases()` | Потратить все непотраченные |

**События:**
- `PurchaseSuccess`, `PurchaseFailed` — UnityEvent
- `PurchaseSuccessEvent(string id)`, `PurchaseFailedEvent(string id)` — Action
- `GetPaymentsEvent` — когда каталог загружен

**Хранилище:** `YandexGame.purchases` (статический массив `Purchase[]`)

**Editor-симуляция:**
- `BuyPayments(id)` сразу вызывает `OnPurchaseSuccess(id)` в Editor
- `GetPayments()` загружает из `InfoYG.purshasesSimulation`
- `ConsumePurchasesYG` — автоматически потребляет все на старте

**Компоненты:**
- `PurchaseYG` — визуальный элемент покупки (title, price, image)
- `PaymentsCatalogYG` — спавнит список покупок из префаба
- `ConsumePurchasesYG` — автоматическое потребление при старте

---

## 4. Сохранения (Saves)

**Модуль:** `Assets/YandexGame/ScriptsYG/Storage/Storage_yg.cs`

**Структура (`SavesYG`):**
```csharp
public class SavesYG {
    public int idSave;          // версия сохранения
    public bool isFirstSession; // первая сессия
    public string language;     // язык
    public bool promptDone;     // был ли промпт
    public int HighScore;       // рекорд проекта
    public string json;         // словарь JSON (Base64 для string)
}
```

**Кастомное хранилище (Dictionary-based):**
```csharp
// Чтение
SavesYG.GetInt("key", default);
SavesYG.GetFloat("key", default);
SavesYG.GetBool("key", default);
SavesYG.GetString("key", default); // автодешифровка Base64
SavesYG.HasKey("key");

// Запись (автосохранение при изменении)
SavesYG.SetInt("key", value);
SavesYG.SetFloat("key", value);
SavesYG.SetBool("key", value);
SavesYG.SetString("key", value); // автошифровка в Base64

// Управление
SavesYG.DeleteKey("key");
SavesYG.DeleteAll();
```

**Сохранение через SavesYG:**
```csharp
YandexGame.savesData.Save();  // обновить json + вызвать YandexGame.SaveProgress()
```

**Методы YandexGame:**
```csharp
YandexGame.SaveProgress();  // Сохранить (Editor → SaveEditor, Web → Cloud/Local)
YandexGame.LoadProgress();  // Загрузить
YandexGame.ResetSaveProgress(); // Сброс
```

**Типы хранения:**
- В Editor: `Assets/YandexGame/WorkingData/Editor/SavesEditorYG.json`
- Web: Локальное (localStorage) + Облачное (Яндекс) с синхронизацией по idSave
- Настройки: `saveCloud`, `localSaveSync`, `saveCloudInterval`, `flush`

**Стандартные поля SavesYG (не удалять):**
```csharp
public int idSave;
public bool isFirstSession = true;
public string language = "ru";
public bool promptDone;
```

---

## 5. Лидерборд

**Модуль:** `Assets/YandexGame/ScriptsYG/Leaderboard/`

```csharp
YandexGame.NewLeaderboardScores("nameLB", score);        // запись рекорда
YandexGame.NewLBScoreTimeConvert("nameLB", floatSeconds); // рекорд по времени
YandexGame.GetLeaderboard(nameLB, maxPlayers, top, around, photoSize);
```

**Событие:** `YandexGame.onGetLeaderboard` (`Action<LBData>`)

**Компонент:** `LeaderboardYG` — отрисовка таблицы, обновление, пагинация.

**В проекте используется:** `SavesYG.SetNewHighscore()` → `YandexGame.NewLeaderboardScores("Leaderboard", HighScore)`

---

## 6. Язык / Локализация

```csharp
YandexGame.lang;               // текущий язык
YandexGame.savesData.language; // сохранённый язык
YandexGame.EnvironmentData.language; // язык из окружения Я.Игр
YandexGame.LangEnable();       // язык загружен?
```
**Событие:** `YandexGame.SwitchLangEvent`

Проект использует **I2 Localization** — YG только определяет стартовый язык.

---

## 7. Инициализация и жизненный цикл

```csharp
YandexGame.SDKEnabled;          // true после готовности SDK
YandexGame.GetDataEvent;       // колбэк готовности SDK
YandexGame.GameReadyAPI();     // сигнал Я.Играм: игра загружена
YandexGame.auth;               // авторизован ли игрок
YandexGame.playerName;         // имя игрока
YandexGame.playerId;           // уникальный ID
YandexGame.EnvironmentData;    // данные окружения
```

**Порядок старта (в Awake → Start):**
1. `CallInitYG()` — инициализация модулей (через атрибут `[InitYG]`)
2. `savesData.LoadSave()` — загрузка сохранений
3. `Localization.InitTranslations()` — инициализация языка
4. `FullscreenShow()` — если `AdWhenLoadingScene = true`
5. `InitLeaderboard()` — если `leaderboardEnable = true`
6. `GetPayments()` — загрузка каталога покупок
7. `CallStartYG()` — пост-инициализация
8. `_SDKEnabled = true` + `GetDataInvoke()`

---

## 8. Прочие API

```csharp
// Промпт "добавить на рабочий стол"
YandexGame.PromptShow();
// События: PromptSuccessEvent, PromptFailEvent

// Отзыв
YandexGame.ReviewShow(bool authDialog);
// Событие: ReviewSentEvent(bool sent)

// Навигация
YandexGame.OnURL(string url);
```

---

## 9. Взаимосвязи в проекте (что где вызывается)

### Скрипт → Вызов YG → Назначение

| Файл | Вызов | Цель |
|---|---|---|
| `SceneLobby.cs` | `YandexGame.GameReadyAPI()` | Сигнал готовности игры |
| `SceneLobby.cs` | `YandexGame.ConsumePurchases()` | Потратить купленное |
| `UILevelBallButtonEvent.cs` | `YandexGame.FullscreenShow()` | Реклама при выборе уровня |
| `UIOptionButton.cs` | `YandexGame.FullscreenShow()` | Реклама при открытии опций |
| `PopupGameStart.cs` | `YandexGame.FullscreenShow()` | Реклама при старте/закрытии |
| `PopupDailySpinReward.cs` | `YandexGame.RewVideoShow(0)` | Награда за просмотр рекламы |
| `PopupShopCoin.cs` | `YandexGame.RewVideoShow(0)` | Награда за просмотр |
| `ItemsInGameStore.cs` | `YandexGame.RewVideoShow(0)` | Награда за просмотр |
| `YandexAdManager.cs` | `YandexGame.RewVideoShow(0)` | Обёртка для rewarded |
| `YandexAdManager.cs` | `YandexGame.FullscreenShow()` | Обёртка для interstitial |
| `SavesYG.cs` | `YandexGame.NewLeaderboardScores()` | Запись рекорда |
| `SavesYG.cs` | `YandexGame.SaveProgress()` | Сохранение |
| `PlayerDataManager.cs` | `YandexGame.savesData.Save()` | Сохранение данных |
| `AudioAssistant.cs` | `YandexGame.savesData.Save()` | Сохранение настроек |
| `UIManager.cs` | `YandexGame.savesData.Save()` | Сохранение данных |

### Паттерн показа Interstitial (везде одинаковый):
```csharp
if (!YandexGame.nowAdsShow && YandexGame.timerShowAd >= YandexGame.Instance.infoYG.fullscreenAdInterval)
{
    YandexGame.FullscreenShow(null, callbackAfterAd);
}
else
{
    callbackAfterAd();
}
```

---

## 10. Инспектор YandexGame (GameObject)

На префабе `Assets/YandexGame/Prefabs/YandexGame.prefab` выставляются:
- **InfoYG** — ссылка на `InfoYG.asset`
- **Singleton** — true (не уничтожать при смене сцены)
- **UnityEvent'ы** — `OpenFullscreenAd`, `CloseFullscreenAd`, `ErrorFullscreenAd`, `OpenVideoAd`, `CloseVideoAd`, `RewardVideoAd`, `ErrorVideoAd`, `PurchaseSuccess`, `PurchaseFailed`, `PromptDo`, `PromptFail`, `ReviewDo`, `ResolvedAuthorization`, `RejectedAuthorization`

---

## 11. Editor-отладка

- Реклама симулируется зелёным/синим/красным оверлеем
- Панель `DebuggingModeYG` для теста всех функций
- Сохранения: `Assets/YandexGame/WorkingData/Editor/SavesEditorYG.json`
- Настройки: `Assets/YandexGame/WorkingData/InfoYG.asset`
