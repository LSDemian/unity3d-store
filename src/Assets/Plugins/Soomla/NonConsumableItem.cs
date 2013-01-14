using System;
using UnityEngine;

namespace com.soomla.unity
{
	public class NonConsumableItem : AbstractVirtualItem
	{
		public MarketItem MarketItem;
		
		public NonConsumableItem (string name, string description, string itemId, string productId, double price)
			: base(name, description, itemId)
		{
			this.MarketItem = new MarketItem(productId, MarketItem.Consumable.NONCONSUMABLE, price);
		}
		
#if UNITY_ANDROID
		public NonConsumableItem(AndroidJavaObject jniNonConsumableItem) 
			: base(jniNonConsumableItem)
		{
			// Google Market Item
			using(AndroidJavaObject jniGoogleMarketItem = jniNonConsumableItem.Call<AndroidJavaObject>("getGoogleItem")) {
				this.MarketItem = new MarketItem(jniGoogleMarketItem);
			}
		}
		
		public AndroidJavaObject toAndroidJavaObject() {
			return new AndroidJavaObject("com.soomla.store.domain.data.NonConsumableItem", this.Name, this.Description, 
				this.ItemId, this.MarketItem.ProductId, this.MarketItem.Price);
		}
#elif UNITY_IOS
		public NonConsumableItem(JSONObject jsonNon)
			: base(jsonNon)
		{
			this.MarketItem = new MarketItem(jsonNon);
		}
		
		public override JSONObject toJSONObject() {
			JSONObject obj = base.toJSONObject();
			JSONObject miJson = this.MarketItem.toJSONObject();
			for(int i=0; i<miJson.list.Count; i++) {
				string key = (string)miJson.keys[i];
				obj.AddField(key, miJson[key]);
			}
			
			return obj;
		}
#endif
	}
}

