using System;

namespace com.soomla.unity.example
{
	public class ExampleEventHandler
	{
		
		public ExampleEventHandler ()
		{
			Events.OnMarketPurchase += onMarketPurchase;
			Events.OnMarketRefund += onMarketRefund;
			Events.OnVirtualGoodPurchased += onVirtualGoodPurchased;
			Events.OnVirtualGoodEquipped += onVirtualGoodEquipped;
			Events.OnVirtualGoodUnEquipped += onVirtualGoodUnequipped;
			Events.OnBillingSupported += onBillingSupported;
			Events.OnBillingNotSupported += onBillingNotSupported;
			Events.OnMarketPurchaseProcessStarted += onMarketPurchaseProcessStarted;
			Events.OnGoodsPurchaseProcessStarted += onGoodsPurchaseProcessStarted;
			Events.OnClosingStore += onClosingStore;
			Events.OnOpeningStore += onOpeningStore;
			Events.OnUnexpectedErrorInStore += onUnexpectedErrorInStore;
			Events.OnCurrencyBalanceChanged += onCurrencyBalanceChanged;
			Events.OnGoodBalanceChanged += onGoodBalanceChanged;
		}
		
		public void onMarketPurchase(MarketItem marketItem) {
			
		}
		
		public void onMarketRefund(MarketItem marketItem) {

		}
		
		public void onVirtualGoodPurchased(VirtualGood good) {

		}
		
		public void onVirtualGoodEquipped(VirtualGood good) {
			
		}
		
		public void onVirtualGoodUnequipped(VirtualGood good) {
			
		}
		
		public void onBillingSupported() {
			
		}
		
		public void onBillingNotSupported() {
			
		}
		
		public void onMarketPurchaseProcessStarted(MarketItem marketItem) {
			
		}
		
		public void onGoodsPurchaseProcessStarted() {
			
		}
		
		public void onClosingStore() {
			
		}
		
		public void onUnexpectedErrorInStore() {
			
		}
		
		public void onOpeningStore() {
			
		}
		
		public void onCurrencyBalanceChanged(VirtualCurrency virtualCurrency, int balance) {
			ExampleLocalStoreInfo.UpdateBalances();
		}
		
		public void onGoodBalanceChanged(VirtualGood good, int balance) {
			ExampleLocalStoreInfo.UpdateBalances();
		}
	}
}

