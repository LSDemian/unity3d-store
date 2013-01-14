using UnityEngine;
using System;
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
		
		public static int GetCurrencyBalance(string currencyItemId) {
			if(!Application.isEditor){
#if UNITY_ANDROID
				AndroidJNI.PushLocalFrame(100);
				int balance = 0;
				using(AndroidJavaClass jniStoreInventory = new AndroidJavaClass("com.soomla.store.StoreInventory")) {
					balance = jniStoreInventory.CallStatic<int>("getCurrencyBalance", currencyItemId);
				}
				AndroidJNI.PopLocalFrame(IntPtr.Zero);
				return balance;
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
				AndroidJNI.PushLocalFrame(100);
				int balance = 0;
				using(AndroidJavaClass jniStoreInventory = new AndroidJavaClass("com.soomla.store.StoreInventory")) {
					balance = jniStoreInventory.CallStatic<int>("addCurrencyAmount", currencyItemId, amount);
				}
				AndroidJNI.PopLocalFrame(IntPtr.Zero);
				return balance;
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
				int balance = 0;
				AndroidJNI.PushLocalFrame(100);
				using(AndroidJavaClass jniStoreInventory = new AndroidJavaClass("com.soomla.store.StoreInventory")) {
					balance = jniStoreInventory.CallStatic<int>("removeCurrencyAmount", currencyItemId, amount);
				}
				AndroidJNI.PopLocalFrame(IntPtr.Zero);
				return balance;
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
				int balance = 0;
				AndroidJNI.PushLocalFrame(100);
				using(AndroidJavaClass jniStoreInventory = new AndroidJavaClass("com.soomla.store.StoreInventory")) {
					balance = jniStoreInventory.CallStatic<int>("getGoodBalance", goodItemId);
				}
				AndroidJNI.PopLocalFrame(IntPtr.Zero);
				return balance;
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
				int balance = 0;
				AndroidJNI.PushLocalFrame(100);
				using(AndroidJavaClass jniStoreInventory = new AndroidJavaClass("com.soomla.store.StoreInventory")) {
					balance = jniStoreInventory.CallStatic<int>("addGoodAmount", goodItemId, amount);
				}
				AndroidJNI.PopLocalFrame(IntPtr.Zero);
				return balance;
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
				int balance = 0;
				AndroidJNI.PushLocalFrame(100);
				using(AndroidJavaClass jniStoreInventory = new AndroidJavaClass("com.soomla.store.StoreInventory")) {
					balance = jniStoreInventory.CallStatic<int>("removeGoodAmount", goodItemId, amount);
				}
				AndroidJNI.PopLocalFrame(IntPtr.Zero);
				return balance;
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

