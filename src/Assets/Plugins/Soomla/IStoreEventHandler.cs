using UnityEngine;

namespace com.soomla.unity
{
	/**
 * This interface represents an event handler.
 * If you want, you can implement your own using this interface and
 * add it to {@link StoreEventHandlers} using
 * 'StoreEventHandlers.getInstance().addEventHandler([your handler here]);'
 */
	/// <summary>
	/// This interface represents an event handler.
 	/// If you want, you can implement your own using this interface and
 	/// add it to StoreEventHandlers using
 	/// 'StoreEventHandlers.AddEventHandler([your handler here]);'
	/// </summary>
	public interface IStoreEventHandler
	{
	    void onMarketPurchase(MarketItem marketItem);	
	    void onMarketRefund(MarketItem marketItem);	
	    void onVirtualGoodPurchased(VirtualGood good);	
	    void onVirtualGoodEquipped(VirtualGood good);	
	    void onVirtualGoodUnequipped(VirtualGood good);	
	    void onBillingSupported();
	    void onBillingNotSupported();
	    void onMarketPurchaseProcessStarted(MarketItem marketItem);	
	    void onGoodsPurchaseProcessStarted();	
	    void onClosingStore();	
	    void onUnexpectedErrorInStore();	
	    void onOpeningStore();
	}
}

