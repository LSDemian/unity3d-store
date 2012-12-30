using System;

namespace com.soomla.unity
{
	/// <summary>
	/// This interface represents a single game's metadata.
 	/// Use this interface to create your assets class that will be transferred to StoreInfo
 	/// upon initialization.
	/// </summary>
	public interface IStoreAssets
	{
		/**
		 * NOTE: The order of the items in the array will be their order when shown to the user (if you're using a store generated from designer.soom.la).
		 **/
		
		/// <summary>
		/// A representation of your game's virtual currencies.
		/// </summary>
		/// <returns>
		/// The virtual currencies.
		/// </returns>
	    VirtualCurrency[] GetVirtualCurrencies();
	

		/// <summary>
		/// An array of all virtual goods served by your store.
		/// </summary>
		/// <returns>
		/// The virtual goods.
		/// </returns>
	    VirtualGood[] GetVirtualGoods();
	
		/// <summary>
		/// An array of all virtual currency packs served by your store.
		/// </summary>
		/// <returns>
		/// The virtual currency packs.
		/// </returns>
	    VirtualCurrencyPack[] GetVirtualCurrencyPacks();
	
		/// <summary>
		/// An array of all virtual categories served by your store.
		/// </summary>
		/// <returns>
		/// The virtual categories.
		/// </returns>
	    VirtualCategory[] GetVirtualCategories();
	
		/// <summary>
		/// You can define non-consumable items that you'd like to use for your needs.
		/// NON-CONSUMABLE items are usually just currency packs. If you use SOOMLA's storefront, it'll take care of
		/// the CONSUMABLE items for you in the UI.
		/// NON-CONSUMABLE items are usually used to let users purchase a "no-ads" token.
		/// Make sure you set the type of the items you add here as Consumable.NONCONSUMABLE.
		/// </summary>
		/// <returns>
		/// The Non Consumable items.
		/// </returns>
	    MarketItem[] GetNonConsumableItems();
	}
}

