using UnityEngine;
using System;

namespace com.soomla.unity {
	public class Soomla : MonoBehaviour {
		
		private static Soomla instance = null;
		
		void Awake(){
			if(instance == null){ 	//making sure we only initialize one instance.
				instance = this;
				GameObject.DontDestroyOnLoad(this.gameObject);
			} else {					//Destroying unused instances.
				GameObject.Destroy(this);
			}
		}
		
		public string customSecret = "SET ONLY ONCE";
		public string publicKey = "YOUR GOOGLE PLAY PUBLIC KEY";
		public string soomSec = "SET ONLY ONCE";
		
		public static Soomla GetInstance(){
			return instance;
		}
		
		public void onMarketPurchase(string message) {
			Debug.Log ("SOOMLA/UNITY onMarketPurchase:" + message);
			
			MarketItem mi = null;
			try {
				mi = StoreInfo.GetNonConsumableItemByProductId(message);
			} catch (VirtualItemNotFoundException e) {
				VirtualCurrencyPack vcp = StoreInfo.GetPackByProductId(message);
				mi = vcp.MarketItem;
			}
			StoreEventHandlers.onMarketPurchase(mi);
		}
		
		public static void onMarketRefund(string message) {
			Debug.Log("SOOMLA/UNITY onMarketRefund:" + message);
			
			MarketItem mi = null;
			try {
				mi = StoreInfo.GetNonConsumableItemByProductId(message);
			} catch (VirtualItemNotFoundException e) {
				VirtualCurrencyPack vcp = StoreInfo.GetPackByProductId(message);
				mi = vcp.MarketItem;
			}
			StoreEventHandlers.onMarketRefund(mi);
		}
	
		
		public void onVirtualGoodPurchased(string message) {
			Debug.Log("SOOMLA/UNITY onVirtualGoodPurchased:" + message);
			
			VirtualGood vg = StoreInfo.GetVirtualGoodByItemId(message);
			StoreEventHandlers.onVirtualGoodPurchased(vg);
		}
	
	
		public void onVirtualGoodEquipped(string message) {
			Debug.Log("SOOMLA/UNITY onVirtualGoodEquiped:" + message);
			
			VirtualGood vg = StoreInfo.GetVirtualGoodByItemId(message);
			StoreEventHandlers.onVirtualGoodEquipped(vg);
		}
	
	
		public void onVirtualGoodUnequipped(string message) {
			Debug.Log("SOOMLA/UNITY onVirtualGoodUnEquiped:" + message);
			
			VirtualGood vg = StoreInfo.GetVirtualGoodByItemId(message);
			StoreEventHandlers.onVirtualGoodUnequipped(vg);
		}
	
	
		public void onBillingSupported(string message) {
			Debug.Log("SOOMLA/UNITY onBillingSupported");
			
			StoreEventHandlers.onBillingSupported();
		}
	
		
		public void onBillingNotSupported(string message) {
			Debug.Log("SOOMLA/UNITY onBillingNotSupported");
			
			StoreEventHandlers.onBillingNotSupported();
		}
	
		
		public void onMarketPurchaseProcessStarted(string message) {
			Debug.Log("SOOMLA/UNITY onMarketPurchaseProcessStarted: " + message);
			
			MarketItem mi = null;
			try {
				mi = StoreInfo.GetNonConsumableItemByProductId(message);
			} catch (VirtualItemNotFoundException e) {
				VirtualCurrencyPack vcp = StoreInfo.GetPackByProductId(message);
				mi = vcp.MarketItem;
			}
			StoreEventHandlers.onMarketPurchaseProcessStarted(mi);
		}
	
		
		public void onGoodsPurchaseProcessStarted(string message) {
			Debug.Log("SOOMLA/UNITY onGoodsPurchaseProcessStarted");
			
			StoreEventHandlers.onGoodsPurchaseProcessStarted();
		}
	
		
		public void onClosingStore(string message) {
			Debug.Log("SOOMLA/UNITY onClosingStore");
			
			StoreEventHandlers.onClosingStore();
		}
	
		
		public void onUnexpectedErrorInStore(string message) {
			Debug.Log("SOOMLA/UNITY onUnexpectedErrorInStore");
			
			StoreEventHandlers.onUnexpectedErrorInStore();
		}
	
		
		public void onOpeningStore(string message) {
			Debug.Log("SOOMLA/UNITY onOpeningStore");
			
			StoreEventHandlers.onOpeningStore();
		}
		
	}
}