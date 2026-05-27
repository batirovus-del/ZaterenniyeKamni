using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyMobile;

public class EasyIAP : MonoBehaviour
{
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
            case EM_IAPConstants.Product_50_coins:
                MonoSingleton<PlayerDataManager>.Instance.IncreaseCoin(50);
                //Debug.Log("Purchase: " + reward1);
                break;
            case EM_IAPConstants.Product_115_coins:
                MonoSingleton<PlayerDataManager>.Instance.IncreaseCoin(115);
                //Debug.Log("Purchase: " + reward2);
                break;
            case EM_IAPConstants.Product_175_coins:
                MonoSingleton<PlayerDataManager>.Instance.IncreaseCoin(175);
                // Debug.Log("Purchase: " + reward3);
                break;
            case EM_IAPConstants.Product_250_coins:
                MonoSingleton<PlayerDataManager>.Instance.IncreaseCoin(250);
                //Debug.Log("Ads removed");
                break;
            case EM_IAPConstants.Product_500_coins:
                MonoSingleton<PlayerDataManager>.Instance.IncreaseCoin(500);
                // Debug.Log("Ads removed");
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

    public void PurchaseCoin1()
    {
        InAppPurchasing.Purchase(EM_IAPConstants.Product_50_coins);
    }

    public void PurchaseCoin2()
    {
        InAppPurchasing.Purchase(EM_IAPConstants.Product_115_coins);
    }

    public void PurchaseCoin3()
    {
        InAppPurchasing.Purchase(EM_IAPConstants.Product_175_coins);
    }

    public void PurchaseCoin4()
    {
        InAppPurchasing.Purchase(EM_IAPConstants.Product_250_coins);
    }

    public void PurchaseCoin5()
    {
        InAppPurchasing.Purchase(EM_IAPConstants.Product_500_coins);
    }

    public void TestBuyCoin()
    {
        MonoSingleton<PlayerDataManager>.Instance.IncreaseCoin(50);
        Debug.Log("CLICKED");
    }
}