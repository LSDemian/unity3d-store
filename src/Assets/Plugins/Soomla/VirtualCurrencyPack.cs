using UnityEngine;
using System.Collections;

namespace com.soomla.unity{	

	/// <summary>
	/// This class represents a pack of the game's virtual currency.
 	/// For example: If you have a "Coin" as a virtual currency, you might
	/// want to sell packs of "Coins". e.g. "10 Coins Set" or "Super Saver Pack".
 	/// The currency pack usually has a google item related to it. As a developer,
 	/// you'll define the Play/Appstore item in Google/Apple's in-app purchase dashboard.
	/// </summary>
	public class VirtualCurrencyPack : AbstractVirtualItem{
		public MarketItem MarketItem;
		public double Price;
		public int CurrencyAmount;
		public VirtualCurrency Currency;
		
		public VirtualCurrencyPack(string name, string description, string itemId, string productId, double price, int currencyAmount, VirtualCurrency currency)
			: base(name, description, itemId)
		{
			this.MarketItem = new MarketItem(productId, MarketItem.Consumable.CONSUMABLE);
			this.Price = price;
			this.CurrencyAmount = currencyAmount;
			this.Currency = currency;
		}
		
#if UNITY_ANDROID
		public VirtualCurrencyPack(AndroidJavaObject jniVirtualCurrencyPack) 
			: base(jniVirtualCurrencyPack)
		{
			this.Price = jniVirtualCurrencyPack.Call<double>("getPrice");
			this.CurrencyAmount = jniVirtualCurrencyPack.Call<int>("getCurrencyAmount");
			// Google Market Item
			AndroidJavaObject jniGoogleMarketItem = jniVirtualCurrencyPack.Call<AndroidJavaObject>("getmGoogleItem");
			this.MarketItem = new MarketItem(jniGoogleMarketItem);
			// Virtual Currency
			AndroidJavaObject jniVirtualCurrency = jniVirtualCurrencyPack.Call<AndroidJavaObject>("getVirtualCurrency");
			this.Currency = new VirtualCurrency(jniVirtualCurrency);
		}
		
		public AndroidJavaObject toAndroidJavaObject(AndroidJavaObject jniVirtualCurrency) {
			return new AndroidJavaObject("com.soomla.store.domain.data.VirtualCurrencyPack", this.Name, this.Description, 
				this.ItemId, this.MarketItem.ProductId, this.Price, this.CurrencyAmount, jniVirtualCurrency);
		}
#elif UNITY_IOS
		public VirtualCurrencyPack(JSONObject jsonVcp)
			: base(jsonVcp)
		{
			this.Price = ((JSONObject)jsonVcp[JSONConsts.CURRENCYPACK_PRICE]).n;
			this.CurrencyAmount = System.Convert.ToInt32(((JSONObject)jsonVcp[JSONConsts.CURRENCYPACK_AMOUNT]).n);
			string productId = jsonVcp[JSONConsts.CURRENCYPACK_PRODUCT_ID].str;
			this.MarketItem = new MarketItem(productId, com.soomla.unity.MarketItem.Consumable.CONSUMABLE);
			
			string currencyItemId = jsonVcp[JSONConsts.CURRENCYPACK_CURRENCYITEMID].str;
			try {
				this.Currency = StoreInfo.GetVirtualCurrencyByItemId(currencyItemId);
			} catch (VirtualItemNotFoundException e) {
				Debug.Log("Couldn't find the associated currency. itemId: " + currencyItemId);
			}
		}
		
		public override JSONObject toJSONObject() {
			JSONObject obj = base.toJSONObject();
			obj.AddField(JSONConsts.CURRENCYPACK_PRICE, (float)this.Price);
			obj.AddField(JSONConsts.CURRENCYPACK_AMOUNT, this.CurrencyAmount);
			obj.AddField(JSONConsts.CURRENCYPACK_PRODUCT_ID, this.MarketItem.ProductId);
			obj.AddField(JSONConsts.CURRENCYPACK_CURRENCYITEMID, this.Currency.ItemId);
			
			return obj;
		}
#endif
	}
}