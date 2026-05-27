using UnityEngine;
using YG;

public class YandexAdManager : MonoBehaviour
{
    public static YandexAdManager Instance;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    public void ShowRewardedAd()
    {
        YG2.RewardedAdvShow("0");
    }

    public void ShowInterstitial()
    {
    }
}
