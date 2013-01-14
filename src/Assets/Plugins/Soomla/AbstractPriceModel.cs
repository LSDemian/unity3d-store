using UnityEngine;
using System.Collections.Generic;

namespace com.soomla.unity{
	
	/// <summary>
	/// his abstract class represents a price-model used in every VirtualGood.
	/// </summary>
	public abstract class AbstractPriceModel {		
		public enum PriceModelType{
			STATIC_PRICE_MODEL,
			BALANCE_DRIVEN_PRICE_MODEL
		};
		
		protected PriceModelType priceModelType;
		
		protected AbstractPriceModel(){
		}
		
		public abstract Dictionary<string,int> GetCurrenctPrice(VirtualGood vg);
#if UNITY_ANDROID
		public abstract AndroidJavaObject toAndroidJavaObject(AndroidJavaObject jniUnityStoreAssets);
		
		public static AbstractPriceModel CreatePriceModel(AndroidJavaObject jniPriceModel) {
			string pmType = jniPriceModel.Call<string>("getType");
			AbstractPriceModel pm = null;
			if (pmType == "balance") {
				List<Dictionary<string, int>> currencyValuePerBalance = new List<Dictionary<string, int>>();
				using(AndroidJavaObject jniCurrencyValuePerBalance = jniPriceModel.Call<AndroidJavaObject>("getCurrencyValuePerBalance")) {
					for (int i=0; i<jniCurrencyValuePerBalance.Call<int>("size"); i++) {
						using(AndroidJavaObject jniCurrencyValue = jniCurrencyValuePerBalance.Call<AndroidJavaObject>("get", i)) {
							using(AndroidJavaObject jniCurrencyValueKeys = jniCurrencyValue.Call<AndroidJavaObject>("keySet")) {
								Dictionary<string, int> currencyValue = new Dictionary<string, int>();
								using(AndroidJavaObject jniCurrencyValueKeysIter = jniCurrencyValueKeys.Call<AndroidJavaObject>("iterator")) {
									while(jniCurrencyValueKeysIter.Call<bool>("hasNext")) {
										string key = jniCurrencyValueKeysIter.Call<string>("next");
										int price = jniCurrencyValue.Call<int>("get", key);
										currencyValue[key] = price;
									}
								}
								currencyValuePerBalance.Add(currencyValue);
							}
						}
					}
				}
				pm = new BalanceDrivenPriceModel(currencyValuePerBalance);
			} else {
				Dictionary<string, int> currencyValue = new Dictionary<string, int>();
				using(AndroidJavaObject jniCurrencyValue = jniPriceModel.Call<AndroidJavaObject>("getCurrencyValue")) {
					using(AndroidJavaObject jniCurrencyValueKeys = jniCurrencyValue.Call<AndroidJavaObject>("keySet")) {
						using(AndroidJavaObject jniCurrencyValueVals = jniCurrencyValue.Call<AndroidJavaObject>("values")) {
							using(AndroidJavaObject jniCurrencyValueKeysIter = jniCurrencyValueKeys.Call<AndroidJavaObject>("iterator")) {
								using(AndroidJavaObject jniCurrencyValueValsIter = jniCurrencyValueVals.Call<AndroidJavaObject>("iterator")) {
									while(jniCurrencyValueKeysIter.Call<bool>("hasNext")) {
										string key = jniCurrencyValueKeysIter.Call<string>("next");
										string priceStr = jniCurrencyValueValsIter.Call<AndroidJavaObject>("next").Call<string>("toString");
										int price = int.Parse(priceStr);
										currencyValue[key] = price;
									}
								}
							}
						}
					}
				}
				pm = new StaticPriceModel(currencyValue);
			}
			return pm;
		}
#elif UNITY_IOS
		public abstract JSONObject toJSONObject();
		
		public static AbstractPriceModel CreatePriceModel(JSONObject jsonPriceModel) {
			string pmType = jsonPriceModel[JSONConsts.GOOD_PRICE_MODEL_TYPE].str;
			AbstractPriceModel pm = null;
			if (pmType == "balance") {
				JSONObject values = jsonPriceModel[JSONConsts.GOOD_PRICE_MODEL_VALUES];
				List<Dictionary<string, int>> currencyValuePerBalance = new List<Dictionary<string, int>>();
				for(int i=0; i<values.list.Count; i++) {
					JSONObject val = values[i];
					Dictionary<string, int> currencyValue = new Dictionary<string, int>();
					for(int j=0; j<val.list.Count; j++) {
						string itemId = (string)val.keys[j];
						int price = System.Convert.ToInt32(((JSONObject)val[itemId]).n);
						currencyValue[itemId] = price;
					}
					currencyValuePerBalance.Add(currencyValue);
				}
				
				pm = new BalanceDrivenPriceModel(currencyValuePerBalance);
			} else {
				JSONObject values = jsonPriceModel[JSONConsts.GOOD_PRICE_MODEL_VALUES];
				Dictionary<string, int> currencyValue = new Dictionary<string, int>();
				for(int i=0; i<values.list.Count; i++) {
					string itemId = (string)values.keys[i];
					int price = System.Convert.ToInt32(((JSONObject)values[itemId]).n);
					currencyValue[itemId] = price;
				}
				pm = new StaticPriceModel(currencyValue);
			}
			
			return pm;
		}
#endif
	}
}