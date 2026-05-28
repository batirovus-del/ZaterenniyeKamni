# Как поменять UI спрайты в Unity проекте "ZateryannieKamni"

## Способ 1: Через Inspector (самый простой)

### Шаг 1: Найти UI элемент
1. Открой сцену `Lobby.unity` или `Game.unity` в Unity
2. В окне **Hierarchy** найди UI элемент (например, кнопку)
3. Кликни на него

### Шаг 2: Заменить спрайт
1. В окне **Inspector** найди компонент **Image** или **SpriteRenderer**
2. Найди поле **Source Image** (или **Sprite**)
3. Кликни на кружок справа от поля
4. Выбери новый спрайт из списка

## Способ 2: Замена файла спрайта

### Если хочешь заменить спрайт везде, где он используется:

1. Найди оригинальный спрайт в папке `Assets\Sprites\`
2. Запомни его имя (например, `Btn_Normal.png`)
3. Подготовь новый спрайт с **таким же именем**
4. Удали старый файл из Unity
5. Перетащи новый файл в ту же папку
6. Unity автоматически обновит все ссылки

## Способ 3: Через скрипт (программно)

Если нужно менять спрайты через код:

```csharp
using UnityEngine;
using UnityEngine.UI;

public class ChangeSprite : MonoBehaviour
{
    public Image targetImage; // UI Image компонент
    public Sprite newSprite;  // Новый спрайт
    
    void Start()
    {
        // Меняем спрайт
        targetImage.sprite = newSprite;
    }
}
```

## Основные UI спрайты в проекте

Вот список основных UI спрайтов, которые ты можешь заменить:

### Кнопки:
- `Btn_Normal.png` - обычная кнопка
- `Btn_Locked.png` - заблокированная кнопка
- `SR_IngameUI_Btn_Buy.png` - кнопка покупки
- `SR_Lobby_Btn_TopBar.png` - кнопка верхней панели
- `SR_PopupBg_btn.png` - кнопка в попапе

### Иконки:
- `shop_icon.png` - иконка магазина
- `SR_icon_star.png` - иконка звезды
- `SR_icon_plus.png` - иконка плюса
- `SR_icon_arrow.png` - иконка стрелки

### Фоны:
- `BG_3.png` - основной фон
- `Bg_top.png` - верхняя панель
- `Bg_bottom.png` - нижняя панель

## Важные настройки спрайта в Unity

После импорта нового спрайта, настрой его:

1. Выбери спрайт в Project
2. В Inspector установи:
   - **Texture Type**: Sprite (2D and UI)
   - **Sprite Mode**: Single (или Multiple для атласов)
   - **Pixels Per Unit**: 100 (стандарт)
   - **Filter Mode**: Bilinear
   - **Compression**: Normal Quality
3. Нажми **Apply**

## Для 9-Slice спрайтов (растягиваемые кнопки)

Если кнопка должна растягиваться:

1. Выбери спрайт
2. Нажми **Sprite Editor**
3. Настрой границы (зеленые линии)
4. Нажми **Apply**
5. В компоненте Image установи **Image Type**: Sliced

## Быстрый поиск UI элементов

Чтобы найти, где используется спрайт:

1. Кликни правой кнопкой на спрайт в Project
2. Выбери **Find References In Scene**
3. Unity покажет все объекты, использующие этот спрайт

## Пример: Замена кнопки "Play"

1. Открой сцену `Lobby.unity`
2. В Hierarchy найди кнопку (обычно в Canvas → UI → Button)
3. В Inspector найди компонент **Image**
4. В поле **Source Image** выбери новый спрайт
5. Готово!

## Советы

- **Сохраняй размеры**: новый спрайт должен быть примерно того же размера, что и старый
- **Формат PNG**: используй PNG с прозрачностью для UI
- **Разрешение**: для мобильных игр обычно 512x512 или 1024x1024 для больших элементов
- **Атласы**: если спрайты в атласе, нужно пересобрать атлас через Sprite Packer

## Где находятся UI спрайты

```
ZateryannieKamni/
└── Assets/
    └── Sprites/
        ├── Btn_Normal.png
        ├── Btn_Locked.png
        ├── SR_IngameUI_Btn_Buy.png
        ├── SR_Lobby_Btn_TopBar.png
        ├── shop_icon.png
        └── ... (другие спрайты)
```

## Если используется Sprite Atlas

Если проект использует Sprite Atlas:

1. Найди атлас в `Assets/`
2. Открой его (двойной клик)
3. Добавь/удали спрайты
4. Нажми **Pack Preview** для проверки
5. Unity автоматически пересоберет атлас при билде
