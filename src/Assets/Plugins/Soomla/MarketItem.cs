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
		
		public MarketItem(string productId, Consumable consumable){
			this.ProductId = productId;
			this.consumable = consumable;
		}
		
#if UNITY_ANDROID
		public MarketItem(AndroidJavaObject jniMarketItem) {
			ProductId = jniMarketItem.Call<string>("getProductId");
			int managedOrdinal = jniMarketItem.Call<AndroidJavaObject>("getManaged").Call<int>("ordinal");
			if (managedOrdinal == 0) {
				this.consumable = Consumable.NONCONSUMABLE;
			} else if (managedOrdinal == 1){
				this.consumable = Consumable.CONSUMABLE;
			} else {
				this.consumable = Consumable.SUBSCRIPTION;
			}
		}
		
		public AndroidJavaObject toAndroidJavaObject(AndroidJavaObject jniUnityStoreAssets) {
			return jniUnityStoreAssets.CallStatic<AndroidJavaObject>("createGoogleMarketItem"
					, this.ProductId
					, (int)(this.consumable));
		}
#elif UNITY_IOS
		public MarketItem(JSONObject jsonMi) {
			ProductId = jsonMi[JSONConsts.MARKET_ITEM_PRODUCT_ID].str;
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
			obj.AddField(JSONConsts.MARKET_ITEM_PRODUCT_ID, ProductId);
			obj.AddField(JSONConsts.MARKET_ITEM_CONSUMABLE, (int)(consumable));
			
			return obj;
		}
#endif
	}
}
