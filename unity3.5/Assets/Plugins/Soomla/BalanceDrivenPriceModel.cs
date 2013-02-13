using UnityEngine;
using System.Collections.Generic;

namespace com.soomla.unity
{
	/// <summary>
	/// This price model is a model that gives the associated virtual good a price according to its balance.
 	/// The BalanceDrivenPriceModel is provided with a List of prices which its indexes is the balances. for
 	/// example, the value at currencyValuePerBalance[0] is the price of a specific virtual good when its balance is 0.
	/// </summary>
	public class BalanceDrivenPriceModel : AbstractPriceModel
	{
		public BalanceDrivenPriceModel (List<Dictionary<string, int>> currencyValuePerBalance)
			: base()
		{
			priceModelType = AbstractPriceModel.PriceModelType.BALANCE_DRIVEN_PRICE_MODEL;
			this.currencyValuePerBalance = currencyValuePerBalance;
		}
		
		public override Dictionary<string,int> GetCurrenctPrice(VirtualGood vg) {
			int balance = StoreInventory.GetGoodBalance(vg.ItemId);
			
			if (balance >= currencyValuePerBalance.Count) {
				return currencyValuePerBalance[currencyValuePerBalance.Count - 1];
			}
			return currencyValuePerBalance[balance];
		}

#if UNITY_ANDROID
		public override AndroidJavaObject toAndroidJavaObject(AndroidJavaObject jniUnityStoreAssets) {
			AndroidJavaObject jniArrayListHashMap = jniUnityStoreAssets.CallStatic<AndroidJavaObject>("createStringIntegerHashMapArrayList");
			foreach(Dictionary<string, int> curval in currencyValuePerBalance) {
				AndroidJavaObject jniHashMap = jniUnityStoreAssets.CallStatic<AndroidJavaObject>("createStringIntegerHashMap");
				foreach(KeyValuePair<string, int> kvp in curval)
				{
					jniUnityStoreAssets.CallStatic("voidPutIntoStringIntegerHashMap", jniHashMap, kvp.Key, kvp.Value);
				}
				jniArrayListHashMap.Call<bool>("add", jniHashMap);
			}
			return new AndroidJavaObject("com.soomla.store.domain.data.BalanceDrivenPriceModel", jniArrayListHashMap);
		}
#elif UNITY_IOS
		public override JSONObject toJSONObject() {
			JSONObject obj = new JSONObject(JSONObject.Type.OBJECT);
			JSONObject curValArrObj = new JSONObject(JSONObject.Type.ARRAY);
			foreach(Dictionary<string, int> curval in currencyValuePerBalance) {
				JSONObject curValObj = new JSONObject(JSONObject.Type.OBJECT);
				foreach(KeyValuePair<string, int> kvp in curval) {
					curValObj.AddField(kvp.Key, kvp.Value);
				}
				curValArrObj.Add(curValObj);
			}
			
			obj.AddField(JSONConsts.GOOD_PRICE_MODEL_VALUES, curValArrObj);
			obj.AddField(JSONConsts.GOOD_PRICE_MODEL_TYPE, "balance");
			
			return obj;
		}
#endif
		
		private List<Dictionary<string, int>> currencyValuePerBalance;
	}
}

