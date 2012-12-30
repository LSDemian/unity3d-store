using System;

namespace com.soomla.unity.example
{
	public class ExampleEventHandler : IStoreEventHandler
	{
		
		public ExampleEventHandler ()
		{
		}
		
		public void onMarketPurchase(MarketItem marketItem) {
			ExampleLocalStoreInfo.UpdateBalances();
		}
		
		public void onMarketRefund(MarketItem marketItem) {
			ExampleLocalStoreInfo.UpdateBalances();
		}
		
		public void onVirtualGoodPurchased(VirtualGood good) {
			ExampleLocalStoreInfo.UpdateBalances();
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
	}
}

