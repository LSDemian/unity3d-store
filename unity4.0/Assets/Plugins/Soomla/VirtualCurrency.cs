using UnityEngine;
using System.Collections;

namespace com.soomla.unity{
	/// <summary>
	/// This is a representation of the game's virtual currency.
	/// Each game can have multiple instances of a virtual currency, all kept in StoreInfo;
	/// </summary>
	public class VirtualCurrency : AbstractVirtualItem{

#if UNITY_ANDROID
		public VirtualCurrency(AndroidJavaObject jniVirtualCurrency) 
			: base(jniVirtualCurrency)
		{
		}
		
		public AndroidJavaObject toAndroidJavaObject() {
			return new AndroidJavaObject("com.soomla.store.domain.data.VirtualCurrency", this.Name, this.Description, this.ItemId);
		}
#elif UNITY_IOS
		public VirtualCurrency(JSONObject jsonVc)
			: base(jsonVc)
		{
		}
		
		public override JSONObject toJSONObject() {
			return base.toJSONObject();
		}
#endif
		
		public VirtualCurrency(string name, string description, string itemId)
			: base(name, description, itemId)
		{
		}
	}
}