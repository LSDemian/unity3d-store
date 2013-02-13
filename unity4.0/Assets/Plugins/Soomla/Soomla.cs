using UnityEngine;
using System;
using System.Text.RegularExpressions;

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
				NonConsumableItem non = StoreInfo.GetNonConsumableItemByProductId(message);
				mi = non.MarketItem;
			} catch (VirtualItemNotFoundException e) {
				VirtualCurrencyPack vcp = StoreInfo.GetPackByProductId(message);
				mi = vcp.MarketItem;
			}
			Events.OnMarketPurchase(mi);
		}
		
		public static void onMarketRefund(string message) {
			Debug.Log("SOOMLA/UNITY onMarketRefund:" + message);
			
			MarketItem mi = null;
			try {
				NonConsumableItem non = StoreInfo.GetNonConsumableItemByProductId(message);
				mi = non.MarketItem;
			} catch (VirtualItemNotFoundException e) {
				VirtualCurrencyPack vcp = StoreInfo.GetPackByProductId(message);
				mi = vcp.MarketItem;
			}
			Events.OnMarketRefund(mi);
		}
	
		
		public void onVirtualGoodPurchased(string message) {
			Debug.Log("SOOMLA/UNITY onVirtualGoodPurchased:" + message);
			
			VirtualGood vg = StoreInfo.GetVirtualGoodByItemId(message);
			Events.OnVirtualGoodPurchased(vg);
		}
	
	
		public void onVirtualGoodEquipped(string message) {
			Debug.Log("SOOMLA/UNITY onVirtualGoodEquiped:" + message);
			
			VirtualGood vg = StoreInfo.GetVirtualGoodByItemId(message);
			Events.OnVirtualGoodEquipped(vg);
		}
	
	
		public void onVirtualGoodUnequipped(string message) {
			Debug.Log("SOOMLA/UNITY onVirtualGoodUnEquiped:" + message);
			
			VirtualGood vg = StoreInfo.GetVirtualGoodByItemId(message);
			Events.OnVirtualGoodUnEquipped(vg);
		}
	
	
		public void onBillingSupported(string message) {
			Debug.Log("SOOMLA/UNITY onBillingSupported");
			
			Events.OnBillingSupported();
		}
	
		
		public void onBillingNotSupported(string message) {
			Debug.Log("SOOMLA/UNITY onBillingNotSupported");
			
			Events.OnBillingNotSupported();
		}
	
		
		public void onMarketPurchaseProcessStarted(string message) {
			Debug.Log("SOOMLA/UNITY onMarketPurchaseProcessStarted: " + message);
			
			MarketItem mi = null;
			try {
				NonConsumableItem non = StoreInfo.GetNonConsumableItemByProductId(message);
				mi = non.MarketItem;
			} catch (VirtualItemNotFoundException e) {
				VirtualCurrencyPack vcp = StoreInfo.GetPackByProductId(message);
				mi = vcp.MarketItem;
			}
			Events.OnMarketPurchaseProcessStarted(mi);
		}
	
		
		public void onGoodsPurchaseProcessStarted(string message) {
			Debug.Log("SOOMLA/UNITY onGoodsPurchaseProcessStarted");
			
			Events.OnGoodsPurchaseProcessStarted();
		}
	
		
		public void onClosingStore(string message) {
			Debug.Log("SOOMLA/UNITY onClosingStore");
			
			Events.OnClosingStore();
		}
	
		
		public void onUnexpectedErrorInStore(string message) {
			Debug.Log("SOOMLA/UNITY onUnexpectedErrorInStore");
			
			Events.OnUnexpectedErrorInStore();
		}
	
		
		public void onOpeningStore(string message) {
			Debug.Log("SOOMLA/UNITY onOpeningStore");
			
			Events.OnOpeningStore();
		}
		
		public void onCurrencyBalanceChanged(string message) {
			Debug.Log("SOOMLA/UNITY onCurrencyBalanceChanged:" + message);
			
			string[] vars = Regex.Split(message, "#SOOM#");
			
			VirtualCurrency vc = StoreInfo.GetVirtualCurrencyByItemId(vars[0]);
			int balance = int.Parse(vars[1]);
			Events.OnCurrencyBalanceChanged(vc, balance);
		}
		
		public void onGoodBalanceChanged(string message) {
			Debug.Log("SOOMLA/UNITY onGoodBalanceChanged:" + message);
			
			string[] vars = Regex.Split(message, "#SOOM#");
			
			VirtualGood vg = StoreInfo.GetVirtualGoodByItemId(vars[0]);
			int balance = int.Parse(vars[1]);
			Events.OnGoodBalanceChanged(vg, balance);
		}
		
	}
}