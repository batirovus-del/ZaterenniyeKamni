using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyMobile;

public class EasyIAPBusters : MonoBehaviour


{

    private ServerItemIndex rewardBoosterType;

    private void Awake()
    {
        if (!RuntimeManager.IsInitialized())
            RuntimeManager.Init();
    }

    void Start()
    {
        bool isInitialized = InAppPurchasing.IsInitialized();
    }

    private void PurchasingCompleteHandle(IAPProduct product)
    {
        switch (product.Name)
        {
            case EM_IAPConstants.Product_One_Bust_Pack:
                MonoSingleton<PlayerDataManager>.Instance.IncreaseCoin(20);
                rewardBoosterType = ServerItemIndex.BoosterVBomb;
                MonoSingleton<PlayerDataManager>.Instance.RewardServerItem(rewardBoosterType, 3, AppEventManager.ItemEarnedBy.Package_Product);
                SoundSFX.Play(SFXIndex.PopupHideAfterClick);
                MonoSingleton<PopupManager>.Instance.Close();
                break;
            case EM_IAPConstants.Product_Two_Bust_Pack:
                MonoSingleton<PlayerDataManager>.Instance.IncreaseCoin(20);
                rewardBoosterType = ServerItemIndex.BoosterHBomb;
                MonoSingleton<PlayerDataManager>.Instance.RewardServerItem(rewardBoosterType, 3, AppEventManager.ItemEarnedBy.Package_Product);
                SoundSFX.Play(SFXIndex.PopupHideAfterClick);
                MonoSingleton<PopupManager>.Instance.Close();
                break;
            case EM_IAPConstants.Product_Three_Bust_Pack:
                MonoSingleton<PlayerDataManager>.Instance.IncreaseCoin(20);
                rewardBoosterType = ServerItemIndex.BoosterCandyPack;
                MonoSingleton<PlayerDataManager>.Instance.RewardServerItem(rewardBoosterType, 3, AppEventManager.ItemEarnedBy.Package_Product);
                SoundSFX.Play(SFXIndex.PopupHideAfterClick);
                MonoSingleton<PopupManager>.Instance.Close();
                break;
            case EM_IAPConstants.Product_Four_Bust_Pack:
                MonoSingleton<PlayerDataManager>.Instance.IncreaseCoin(20);
                rewardBoosterType = ServerItemIndex.BoosterHammer;
                MonoSingleton<PlayerDataManager>.Instance.RewardServerItem(rewardBoosterType, 3, AppEventManager.ItemEarnedBy.Package_Product);
                SoundSFX.Play(SFXIndex.PopupHideAfterClick);
                MonoSingleton<PopupManager>.Instance.Close();
                break;
            default:
                break;
        }
    }

    void PurchaseFailedHandler(IAPProduct product, string failureReason)
    {
        Debug.Log("The purchase of product " + product.Name + " has failed with reason: " + failureReason);
    }

    private void OnEnable()
    {
        InAppPurchasing.PurchaseCompleted += PurchasingCompleteHandle;
        InAppPurchasing.PurchaseFailed += PurchaseFailedHandler;
    }

    private void OnDisable()
    {
        InAppPurchasing.PurchaseCompleted += PurchasingCompleteHandle;
        InAppPurchasing.PurchaseFailed += PurchaseFailedHandler;
    }

    public void PurchaseBustPack1()
    {
        InAppPurchasing.Purchase(EM_IAPConstants.Product_One_Bust_Pack);
    }

    public void PurchaseBustPack2()
    {
        InAppPurchasing.Purchase(EM_IAPConstants.Product_Two_Bust_Pack);
    }

    public void PurchaseBustPack3()
    {
        InAppPurchasing.Purchase(EM_IAPConstants.Product_Three_Bust_Pack);
    }

    public void PurchaseBustPack4()
    {
        InAppPurchasing.Purchase(EM_IAPConstants.Product_Four_Bust_Pack);
    }

    public void testVBOMB1()
    {
        MonoSingleton<PlayerDataManager>.Instance.IncreaseCoin(20);
        rewardBoosterType = ServerItemIndex.BoosterVBomb;
        MonoSingleton<PlayerDataManager>.Instance.RewardServerItem(rewardBoosterType, 3, AppEventManager.ItemEarnedBy.Package_Product);
        SoundSFX.Play(SFXIndex.PopupHideAfterClick);
        MonoSingleton<PopupManager>.Instance.Close();
    }

    public void testHBOMB2()
    {
        MonoSingleton<PlayerDataManager>.Instance.IncreaseCoin(20);
        rewardBoosterType = ServerItemIndex.BoosterHBomb;
        MonoSingleton<PlayerDataManager>.Instance.RewardServerItem(rewardBoosterType, 3, AppEventManager.ItemEarnedBy.Package_Product);
        SoundSFX.Play(SFXIndex.PopupHideAfterClick);
        MonoSingleton<PopupManager>.Instance.Close();
    }

    public void testCANDYPACK3()
    {
        MonoSingleton<PlayerDataManager>.Instance.IncreaseCoin(20);
        rewardBoosterType = ServerItemIndex.BoosterCandyPack;
        MonoSingleton<PlayerDataManager>.Instance.RewardServerItem(rewardBoosterType, 3, AppEventManager.ItemEarnedBy.Package_Product);
        SoundSFX.Play(SFXIndex.PopupHideAfterClick);
        MonoSingleton<PopupManager>.Instance.Close();
    }

    public void testHAMMER()
    {
        MonoSingleton<PlayerDataManager>.Instance.IncreaseCoin(20);
        rewardBoosterType = ServerItemIndex.BoosterHammer;
        MonoSingleton<PlayerDataManager>.Instance.RewardServerItem(rewardBoosterType, 3, AppEventManager.ItemEarnedBy.Package_Product);
        SoundSFX.Play(SFXIndex.PopupHideAfterClick);
        MonoSingleton<PopupManager>.Instance.Close();
    }


}
