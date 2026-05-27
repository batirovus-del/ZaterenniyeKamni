using UnityEngine;
using UnityEngine.Events;
using YG;

namespace YG.Example
{
    [HelpURL("https://www.notion.so/PluginYG-d457b23eee604b7aa6076116aab647ed#10e7dfffefdc42ec93b39be0c78e77cb")]
    public class ReceivingPurchaseExample : MonoBehaviour
    {
        public static ReceivingPurchaseExample Instance;

        public Booster.BoosterType BoosterType;

        [SerializeField] UnityEvent successPurchased;
        [SerializeField] UnityEvent failedPurchased;

        private void OnEnable()
        {
        }

        private void OnDisable()
        {
        }

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            //PlayerPrefs.DeleteAll();
            //YandexGame.ResetSaveProgress();
        }

        //private void Start()
        //{
            //SoundManager.SetVolumeMusic(0f);
            //MonoSingleton<PlayerDataManager>.Instance.IsOnSoundBGM = false;
            //MonoSingleton<PlayerDataManager>.Instance.SaveOptionSound();
            //YandexGame.ConsumePurchases();
        //}

        public void ThreeCoins()
        {
            MonoSingleton<PlayerDataManager>.Instance.IncreaseCoin(3);
            YG2.SaveProgress();
        }

        void SuccessPurchased(string id)
        {
            successPurchased?.Invoke();

            Debug.Log("GetBuyMy = " + id);
            switch (id)
            {
                case "coins1":
                    MonoSingleton<PlayerDataManager>.Instance.IncreaseCoin(50);
                    break;
                case "coins2":
                    MonoSingleton<PlayerDataManager>.Instance.IncreaseCoin(115);
                    break;
                case "coins3":
                    MonoSingleton<PlayerDataManager>.Instance.IncreaseCoin(175);
                    break;
                case "coins4":
                    MonoSingleton<PlayerDataManager>.Instance.IncreaseCoin(250);
                    break;
                case "coins5":
                    MonoSingleton<PlayerDataManager>.Instance.IncreaseCoin(500);
                    break;



                case "coins1x2":
                    MonoSingleton<PlayerDataManager>.Instance.IncreaseCoin(100);
                    break;
                case "coins2x2":
                    MonoSingleton<PlayerDataManager>.Instance.IncreaseCoin(230);
                    break;
                case "coins3x2":
                    MonoSingleton<PlayerDataManager>.Instance.IncreaseCoin(350);
                    break;
                case "coins4x2":
                    MonoSingleton<PlayerDataManager>.Instance.IncreaseCoin(500);
                    break;
                case "coins5x2":
                    MonoSingleton<PlayerDataManager>.Instance.IncreaseCoin(1000);
                    break;
            }

            YG2.SaveProgress();
            // ��� ��� ��� ��������� �������. ��������:
            //if (id == "50")
            //    YandexGame.savesData.money += 50;
            //else if (id == "250")
            //    YandexGame.savesData.money += 250;
            //else if (id == "1500")
            //    YandexGame.savesData.money += 1500;
            //YandexGame.SaveProgress();
        }

        void FailedPurchased(string id)
        {
            failedPurchased?.Invoke();
        }
    }
}