using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdManager : MonoBehaviour
{

    public bool TestAds = false;        //This has to be true when testing, and has to be false when publishing!!!
    public bool unityAds = true;        //Set this false if you want to use Admob, set this true if you want to use Unity Ads!!!

    private BannerView bannerView;
    private InterstitialAd interstitialView;
    private RewardedAd rewardBasedVideoAd;


    public static bool firstTime = true;

    public static AdManager admanagerInstance = null;

    //These IDs have to be changed to the actual app and ad IDs!!!
    [SerializeField] private string appID = "ca-app-pub-6834237205411725~5492442510";
    [SerializeField] private string bannerID = "ca-app-pub-6834237205411725/6561981604";
    [SerializeField] private string interstitialID = "ca-app-pub-6834237205411725/9240115836";
    [SerializeField] private string rewardVideoID = "ca-app-pub-6834237205411725/2674707485";

    /*void Awake()
    {
        if (admanagerInstance == null)
        {
            admanagerInstance = this;
        }
        else if (admanagerInstance != this)
        {
            Destroy(gameObject);
        }

        if (firstTime)
        {
            firstTime = false;
            DontDestroyOnLoad(gameObject);

            MobileAds.Initialize(initStatus => { RequestInterstitial(); });
        }
    }

    void Start()
    {
        // Called when the user should be rewarded for watching a video.
        

        //request reward video
        admanagerInstance.RequestRewardBasedVideo();
    }*/

    //Call this to show banner ad
    public void ShowAdmobBanner()
    {
        bannerView.Show();
    }

    //Call this to hide banner ad
    public void HideAdmobBanner()
    {
        bannerView.Hide();
    }

    //Call this to show interstitial ad
    public void ShowAdmobInterstitial()
    {
        if (admanagerInstance.interstitialView.IsLoaded())
            admanagerInstance.interstitialView.Show();

        RequestInterstitial();
    }

    //Call this to show reward video ad
    public void ShowAdmobRewardVideo()
    {
        if (rewardBasedVideoAd.IsLoaded())
        {
            rewardBasedVideoAd.Show();
        }
    }


    #region AdmobRequests
    private void RequestBanner()
    {
        bannerView = new BannerView(bannerID, AdSize.Banner, AdPosition.Bottom);

        AdRequest request = null;
        request = new AdRequest.Builder().Build();
        bannerView.LoadAd(request);
    }

    private void RequestInterstitial()
    {
        if (admanagerInstance.interstitialView != null)
        {
            admanagerInstance.interstitialView.Destroy();
        }

        admanagerInstance.interstitialView = new InterstitialAd(interstitialID);

        AdRequest request = null;
        request = new AdRequest.Builder().Build();
        admanagerInstance.interstitialView.LoadAd(request);
    }

    private void RequestRewardBasedVideo()
    {
        rewardBasedVideoAd = new RewardedAd(rewardVideoID);
        rewardBasedVideoAd.OnUserEarnedReward += HandleRewardBasedVideoRewarded;
        AdRequest request = new AdRequest.Builder().Build();
        rewardBasedVideoAd.LoadAd(request);
    }
    #endregion

    //This is called when user completes Admob reward video
    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        Debug.Log("The ad was shown successfully");
        FindObjectOfType<RewardButton>().AddReward();
    }
}
