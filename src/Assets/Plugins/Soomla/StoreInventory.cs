using UnityEngine;
using System.Runtime.InteropServices;

namespace com.soomla.unity
{
	/// <summary>
	/// This class allows some convinience operations on Virtual Goods and Virtual Currencies.
	/// </summary>
	public class StoreInventory
	{
		
#if UNITY_IOS
		[DllImport ("__Internal")]
		private static extern int storeInventory_GetCurrencyBalance(string itemId, out int outBalance);
		[DllImport ("__Internal")]
		private static extern int storeInventory_AddCurrencyAmount(string itemId, int amount, out int outBalance);
		[DllImport ("__Internal")]
		private static extern int storeInventory_RemoveCurrencyAmount(string itemId, int amount, out int outBalance);
		[DllImport ("__Internal")]
		private static extern int storeInventory_GetGoodBalance(string itemId, out int outBalance);
		[DllImport ("__Internal")]
		private static extern int storeInventory_AddGoodAmount(string itemId, int amount, out int outBalance);
		[DllImport ("__Internal")]
		private static extern int storeInventory_RemoveGoodAmount(string itemId, int amount, out int outBalance);
#endif
		
#if UNITY_ANDROID
		private static AndroidJavaClass jniStoreInventory = null;
#endif
		
		public static void Init() {
#if UNITY_ANDROID
			jniStoreInventory = new AndroidJavaClass("com.soomla.store.StoreInventory");
#endif
		}
		
		public static int GetCurrencyBalance(string currencyItemId) {
			if(!Application.isEditor){
#if UNITY_ANDROID
				return jniStoreInventory.CallStatic<int>("getCurrencyBalance", currencyItemId);
#elif UNITY_IOS
				Debug.Log("SOOMLA/UNITY Calling GetCurrencyBalance with: " + currencyItemId);
				int balance = 0;
				int err = storeInventory_GetCurrencyBalance(currencyItemId, out balance);
				
				IOS_ErrorCodes.CheckAndThrowException(err);
				
				return balance;
#endif
			}
			return 0;
		}
		
		public static int AddCurrencyAmount(string currencyItemId, int amount) {
			if(!Application.isEditor){
#if UNITY_ANDROID
				return jniStoreInventory.CallStatic<int>("addCurrencyAmount", currencyItemId, amount);
#elif UNITY_IOS
				Debug.Log("SOOMLA/UNITY Calling AddCurrencyAmount with: " + currencyItemId);
				int balance = 0;
				int err = storeInventory_AddCurrencyAmount(currencyItemId, amount, out balance);
				
				IOS_ErrorCodes.CheckAndThrowException(err);
				
				return balance;
#endif
			}
			return 0;
		}
		
		public static int RemoveCurrencyAmount(string currencyItemId, int amount) {
			if(!Application.isEditor){
#if UNITY_ANDROID
				return jniStoreInventory.CallStatic<int>("removeCurrencyAmount", currencyItemId, amount);
#elif UNITY_IOS
				Debug.Log("SOOMLA/UNITY Calling RemoveCurrencyAmount with: " + currencyItemId);
				int balance = 0;
				int err = storeInventory_RemoveCurrencyAmount(currencyItemId, amount, out balance);
				
				IOS_ErrorCodes.CheckAndThrowException(err);
				
				return balance;
				
#endif
			}
			return 0;
		}
		
		public static int GetGoodBalance(string goodItemId) {
			if(!Application.isEditor){
#if UNITY_ANDROID
				return jniStoreInventory.CallStatic<int>("getGoodBalance", goodItemId);
#elif UNITY_IOS
				Debug.Log("SOOMLA/UNITY Calling GetGoodBalance with: " + goodItemId);
				int balance = 0;
				int err = storeInventory_GetGoodBalance(goodItemId, out balance);
				
				IOS_ErrorCodes.CheckAndThrowException(err);
				
				return balance;
#endif
			}
			return 0;
		}
		
		public static int AddGoodAmount(string goodItemId, int amount) {
			if(!Application.isEditor){
#if UNITY_ANDROID
				return jniStoreInventory.CallStatic<int>("addGoodAmount", goodItemId, amount);
#elif UNITY_IOS
				Debug.Log("SOOMLA/UNITY Calling AddGoodAmount with: " + goodItemId);
				int balance = 0;
				int err = storeInventory_AddGoodAmount(goodItemId, amount, out balance);
				
				IOS_ErrorCodes.CheckAndThrowException(err);
				
				return balance;
#endif
			}
			return 0;
		}
		
		public static int RemoveGoodAmount(string goodItemId, int amount) {
			if(!Application.isEditor){
#if UNITY_ANDROID
				return jniStoreInventory.CallStatic<int>("removeGoodAmount", goodItemId, amount);
#elif UNITY_IOS
				Debug.Log("SOOMLA/UNITY Calling RemoveGoodAmount with: " + goodItemId);
				int balance = 0;
				int err = storeInventory_RemoveGoodAmount(goodItemId, amount, out balance);
				
				IOS_ErrorCodes.CheckAndThrowException(err);
				
				return balance;
#endif
			}
			return 0;
		}
	}
}

