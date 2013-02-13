using UnityEngine;
using System.Collections.Generic;

namespace com.soomla.unity
{
	/// <summary>
	/// This price model is a model that gives the associated virtual good a static price that's not affected by anything.
	/// </summary>
	public class StaticPriceModel : AbstractPriceModel
	{
		public StaticPriceModel (Dictionary<string, int> currencyValue)
			: base()
		{
			priceModelType = AbstractPriceModel.PriceModelType.STATIC_PRICE_MODEL;
			this.currencyValue = currencyValue;
		}
		
		public override Dictionary<string,int> GetCurrenctPrice(VirtualGood vg) {
			return currencyValue;
		}

#if UNITY_ANDROID
		public override AndroidJavaObject toAndroidJavaObject(AndroidJavaObject jniUnityStoreAssets) {
			AndroidJavaObject jniHashMap = jniUnityStoreAssets.CallStatic<AndroidJavaObject>("createStringIntegerHashMap");
			foreach(KeyValuePair<string, int> kvp in currencyValue)
			{
				jniUnityStoreAssets.CallStatic("voidPutIntoStringIntegerHashMap", jniHashMap, kvp.Key, kvp.Value);
			}
			return new AndroidJavaObject("com.soomla.store.domain.data.StaticPriceModel", jniHashMap);
		}
#elif UNITY_IOS
		public override JSONObject toJSONObject() {
			JSONObject obj = new JSONObject(JSONObject.Type.OBJECT);
			JSONObject curValObj = new JSONObject(JSONObject.Type.OBJECT);
			foreach(KeyValuePair<string, int> kvp in currencyValue) {
				curValObj.AddField(kvp.Key, kvp.Value);
			}
			
			obj.AddField(JSONConsts.GOOD_PRICE_MODEL_VALUES, curValObj);
			obj.AddField(JSONConsts.GOOD_PRICE_MODEL_TYPE, "static");
			
			return obj;
		}
#endif
		
		private Dictionary<string, int> currencyValue;
	}
}

