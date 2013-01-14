using System;
using UnityEngine;

namespace com.soomla.unity
{
	public class Events
	{
		public delegate void MarketPurchaseDelegate(MarketItem marketItem);
		public static MarketPurchaseDelegate OnMarketPurchase;
		
		public delegate void MarketRefundDelegate(MarketItem marketItem);
		public static MarketRefundDelegate OnMarketRefund;
		
		public delegate void VirtualGoodPurchasedDelegate(VirtualGood good);
		public static VirtualGoodPurchasedDelegate OnVirtualGoodPurchased;
		
		public delegate void VirtualGoodEquippedDelegate(VirtualGood good);
		public static VirtualGoodEquippedDelegate OnVirtualGoodEquipped;
		
		public delegate void VirtualGoodUnEquippedDelegate(VirtualGood good);
		public static VirtualGoodUnEquippedDelegate OnVirtualGoodUnEquipped;
		
		public delegate void BillingSupportedDelegate();
		public static BillingSupportedDelegate OnBillingSupported;
		
		public delegate void BillingNotSupportedDelegate();
		public static BillingNotSupportedDelegate OnBillingNotSupported;
		
		public delegate void MarketPurchaseProcessStartedDelegate(MarketItem marketItem);
		public static MarketPurchaseProcessStartedDelegate OnMarketPurchaseProcessStarted;
		
		public delegate void GoodsPurchaseProcessStartedDelegate();
		public static GoodsPurchaseProcessStartedDelegate OnGoodsPurchaseProcessStarted;
		
		public delegate void ClosingStoreDelegate();
		public static ClosingStoreDelegate OnClosingStore;
		
		public delegate void OpeningStoreDelegate();
		public static OpeningStoreDelegate OnOpeningStore;
		
		public delegate void UnexpectedErrorInStoreDelegate();
		public static UnexpectedErrorInStoreDelegate OnUnexpectedErrorInStore;
		
		public delegate void CurrencyBalanceChangedDelegate(VirtualCurrency virtualCurrency, int balance);
		public static CurrencyBalanceChangedDelegate OnCurrencyBalanceChanged;
		
		public delegate void GoodBalanceChangedDelegate(VirtualGood virtualGood, int balance);
		public static GoodBalanceChangedDelegate OnGoodBalanceChanged;
	}
}

