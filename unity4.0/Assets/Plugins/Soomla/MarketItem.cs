using UnityEngine;
using System.Collections;

namespace com.soomla.unity{

	/// <summary>
	/// This class represents an item in Google Play or AppStore.
	/// Every VirtualCurrencyPack has an instance of this class which is a
	/// representation of the same currency pack as an item on Google Play ot AppStore.
	/// </summary>
	public class MarketItem {
		
		public enum Consumable{
			NONCONSUMABLE,
			CONSUMABLE,
			SUBSCRIPTION,
		}
		
		public string ProductId;
		public Consumable consumable;
		public double Price;
		
		public MarketItem(string productId, Consumable consumable, double price){
			this.ProductId = productId;
			this.consumable = consumable;
			this.Price = price;
		}
		
#if UNITY_ANDROID
		public MarketItem(AndroidJavaObject jniMarketItem) {
			ProductId = jniMarketItem.Call<string>("getProductId");
			Price = jniMarketItem.Call<double>("getPrice");
			int managedOrdinal = jniMarketItem.Call<AndroidJavaObject>("getManaged").Call<int>("ordinal");
			switch(managedOrdinal){
				case 0:
					this.consumable = Consumable.NONCONSUMABLE;
					break;
				case 1:
					this.consumable = Consumable.CONSUMABLE;
					break;
				case 2:
					this.consumable = Consumable.SUBSCRIPTION;
					break;
				default:
					this.consumable = Consumable.CONSUMABLE;
					break;
			}
		}
		
		public AndroidJavaObject toAndroidJavaObject(AndroidJavaObject jniUnityStoreAssets) {
			return jniUnityStoreAssets.CallStatic<AndroidJavaObject>("createGoogleMarketItem"
					, this.ProductId
					, (int)(this.consumable)
					, this.Price);
		}
#elif UNITY_IOS
		public MarketItem(JSONObject jsonMi) {
			ProductId = jsonMi[JSONConsts.MARKET_ITEM_PRODUCT_ID].str;
			Price = jsonMi[JSONConsts.MARKET_ITEM_PRICE].n;
			int cOrdinal = System.Convert.ToInt32(((JSONObject)jsonMi[JSONConsts.MARKET_ITEM_CONSUMABLE]).n);
			if (cOrdinal == 0) {
				this.consumable = Consumable.NONCONSUMABLE;
			} else if (cOrdinal == 1){
				this.consumable = Consumable.CONSUMABLE;
			} else {
				this.consumable = Consumable.SUBSCRIPTION;
			}
		}
		
		public JSONObject toJSONObject() {
			JSONObject obj = new JSONObject(JSONObject.Type.OBJECT);
			obj.AddField(JSONConsts.MARKET_ITEM_PRODUCT_ID, this.ProductId);
			obj.AddField(JSONConsts.MARKET_ITEM_CONSUMABLE, (int)(consumable));
			obj.AddField(JSONConsts.MARKET_ITEM_PRICE, (float)this.Price);
			return obj;
		}
#endif
	}
}
