using UnityEngine;
using System.Collections.Generic;

namespace com.soomla.unity
{
	public class StoreEventHandlers
	{
		
		public static void AddEventHandler(IStoreEventHandler handler) {
			eventHandlers.Add(handler);
		}
		
		public static void RemoveEventHandler(IStoreEventHandler handler) {
			eventHandlers.Remove(handler);
		}
		
		
		//Event Handlers
		
		public static void onMarketPurchase(MarketItem mi) {
			foreach(IStoreEventHandler handler in eventHandlers) {
				handler.onMarketPurchase(mi);
			}
		}
	
		
		public static void onMarketRefund(MarketItem mi) {
			foreach(IStoreEventHandler handler in eventHandlers) {
				handler.onMarketRefund(mi);
			}
		}
	
		
		public static void onVirtualGoodPurchased(VirtualGood vg) {
			foreach(IStoreEventHandler handler in eventHandlers) {
				handler.onVirtualGoodPurchased(vg);
			}
		}
	
	
		public static void onVirtualGoodEquipped(VirtualGood vg) {
			foreach(IStoreEventHandler handler in eventHandlers) {
				handler.onVirtualGoodEquipped(vg);
			}
		}
	
	
		public static void onVirtualGoodUnequipped(VirtualGood vg) {
			foreach(IStoreEventHandler handler in eventHandlers) {
				handler.onVirtualGoodUnequipped(vg);
			}
		}
	
	
		public static void onBillingSupported() {
			foreach(IStoreEventHandler handler in eventHandlers) {
				handler.onBillingSupported();
			}
		}
	
		
		public static void onBillingNotSupported() {
			foreach(IStoreEventHandler handler in eventHandlers) {
				handler.onBillingNotSupported();
			}
		}
	
		
		public static void onMarketPurchaseProcessStarted(MarketItem mi) {
			foreach(IStoreEventHandler handler in eventHandlers) {
				handler.onMarketPurchaseProcessStarted(mi);
			}
		}
	
		
		public static void onGoodsPurchaseProcessStarted() {
			foreach(IStoreEventHandler handler in eventHandlers) {
				handler.onGoodsPurchaseProcessStarted();
			}
		}
	
		
		public static void onClosingStore() {
			foreach(IStoreEventHandler handler in eventHandlers) {
				handler.onClosingStore();
			}
		}
	
		
		public static void onUnexpectedErrorInStore() {
			foreach(IStoreEventHandler handler in eventHandlers) {
				handler.onUnexpectedErrorInStore();
			}
		}
	
		
		public static void onOpeningStore() {
			foreach(IStoreEventHandler handler in eventHandlers) {
				handler.onOpeningStore();
			}
		}
		
		private static List<IStoreEventHandler> eventHandlers = new List<IStoreEventHandler>();
	}
}

