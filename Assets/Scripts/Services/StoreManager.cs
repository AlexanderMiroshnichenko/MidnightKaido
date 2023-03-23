using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;

public class StoreManager : MonoBehaviour, IStoreListener
{
    IStoreController m_StoreController;
     int m_GoldCount;

    public Text GoldCountText;
    
    public StoreData storeData;
  
  void Start()
        {
            InitializePurchasing();
            UpdateUI();
        }
        void InitializePurchasing()
        {
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            //Add products that will be purchasable and indicate its type.
            builder.AddProduct(storeData.products[0].androidId, ProductType.Consumable);
          

            UnityPurchasing.Initialize(this, builder);
        }
     public void BuyGold()
        {
            m_StoreController.InitiatePurchase(storeData.products[0].androidId);
        }
     public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            Debug.Log("In-App Purchasing successfully initialized");
            m_StoreController = controller;
        }

    public void OnInitializeFailed(InitializationFailureReason error, string? message=null)
    {
        var errorMessage = $"Purchasing failed to initialize. Reason: {error}.";

            if (message != null)
            {
                errorMessage += $" More details: {message}";
            }

            Debug.Log(errorMessage);
    }

   
     public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            m_onFail?.Invoke();
            Debug.Log($"Purchase failed - Product: '{product.definition.id}', PurchaseFailureReason: {failureReason}");
        }

   public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
        {
            //Retrieve the purchased product
            var product = args.purchasedProduct;

            //Add the purchased product to the players inventory
           m_onSuccess?.Invoke();
          

            Debug.Log($"Purchase Complete - Product: {product.definition.id}");

            //We return Complete, informing IAP that the processing on our side is done and the transaction can be closed.
            return PurchaseProcessingResult.Complete;
        }

        void AddGold()
        {
            m_GoldCount++;
           UpdateUI();
        }

        void UpdateUI()
        {
            GoldCountText.text = $"Your Gold: {m_GoldCount}";
           
        }

private System.Action m_onSuccess;
private System.Action m_onFail;
        public void InitializePurchase(string productID, System.Action onSuccess, System.Action onFail)
        {
            m_StoreController.InitiatePurchase(productID);
            m_onSuccess = onSuccess;
            m_onFail=onFail;
        }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        OnInitializeFailed(error,null);

    }
}

