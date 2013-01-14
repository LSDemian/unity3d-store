using UnityEngine;
using System;
using System.Runtime.InteropServices;


namespace com.soomla.unity
{
	/// <summary>
	/// You can use this class to purchase products from the native phone market, buy virtual goods, and do many other store related operations.
	/// </summary>
	public class StoreController
	{
#if UNITY_IOS
		[DllImport ("__Internal")]
		private static extern void storeController_Init(string customSecret);
		[DllImport ("__Internal")]
		private static extern int storeController_BuyMarketItem(string productId);
		[DllImport ("__Internal")]
		private static extern int storeController_BuyVirtualGood(string itemId);
		[DllImport ("__Internal")]
		private static extern int storeController_EquipVirtualGood(string itemId);
		[DllImport ("__Internal")]
		private static extern int storeController_UnEquipVirtualGood(string itemId);
		[DllImport ("__Internal")]
		private static extern void storeController_StoreOpening();
		[DllImport ("__Internal")]
		private static extern void storeController_StoreClosing();
		[DllImport ("__Internal")]
		private static extern void storeController_SetSoomSec(string soomSec);
#endif
		
#if UNITY_ANDROID
		private static AndroidJavaObject jniStoreController = null;
//		private static AndroidJavaObject jniUnityEventHandler = null;
#endif
		
		public static void Initialize(IStoreAssets storeAssets) {
			if (string.IsNullOrEmpty(Soomla.GetInstance().publicKey) || string.IsNullOrEmpty(Soomla.GetInstance().customSecret) || string.IsNullOrEmpty(Soomla.GetInstance().soomSec)) {
				Debug.Log("SOOMLA/UNITY MISSING publickKey or customSecret or soomSec !!! Stopping here !!");
				throw new ExitGUIException();
			}
			//init SOOM_SEC
#if UNITY_ANDROID
			AndroidJNI.PushLocalFrame(100);
			using(AndroidJavaClass jniStoreAssets = new AndroidJavaClass("com.soomla.unity.StoreAssets")) {
				jniStoreAssets.CallStatic("setSoomSec", Soomla.GetInstance().soomSec);
			}
			AndroidJNI.PopLocalFrame(IntPtr.Zero);
#elif UNITY_IOS
			storeController_SetSoomSec(Soomla.GetInstance().soomSec);
#endif
			
			StoreInfo.Initialize(storeAssets);
#if UNITY_ANDROID
			AndroidJNI.PushLocalFrame(100);
			using(AndroidJavaObject jniStoreAssetsInstance = new AndroidJavaObject("com.soomla.unity.StoreAssets")) {
				using(AndroidJavaClass jniStoreControllerClass = new AndroidJavaClass("com.soomla.store.StoreController")) {
					jniStoreController = jniStoreControllerClass.CallStatic<AndroidJavaObject>("getInstance");
					jniStoreController.Call("initialize", jniStoreAssetsInstance, Soomla.GetInstance().publicKey, Soomla.GetInstance().customSecret);
				}
			}
			//init EventHandler
			using(AndroidJavaClass jniEventHandler = new AndroidJavaClass("com.soomla.unity.EventHandler")) {
				jniEventHandler.CallStatic("initialize");
			}
			AndroidJNI.PopLocalFrame(IntPtr.Zero);
#elif UNITY_IOS
			storeController_Init(Soomla.GetInstance().customSecret);
#endif
		}
		
		
		public static void BuyMarketItem(string packProductId) {
#if UNITY_ANDROID
			AndroidJNI.PushLocalFrame(100);
			AndroidJNIHandler.CallVoid(jniStoreController, "buyGoogleMarketItem", packProductId);
			AndroidJNI.PopLocalFrame(IntPtr.Zero);
#elif UNITY_IOS
			storeController_BuyMarketItem(packProductId);
#endif
		}
		
		public static void BuyVirtualGood(string goodItemId) {
#if UNITY_ANDROID
			AndroidJNI.PushLocalFrame(100);
			AndroidJNIHandler.CallVoid(jniStoreController, "buyVirtualGood", goodItemId);
			AndroidJNI.PopLocalFrame(IntPtr.Zero);
#elif UNITY_IOS
			storeController_BuyVirtualGood(goodItemId);
#endif
		}
		
		public static void EquipVirtualGood(string goodItemId) {
#if UNITY_ANDROID
			AndroidJNI.PushLocalFrame(100);
			AndroidJNIHandler.CallVoid(jniStoreController, "equipVirtualGood", goodItemId);
			AndroidJNI.PopLocalFrame(IntPtr.Zero);
#elif UNITY_IOS
			storeController_EquipVirtualGood(goodItemId);
#endif
		}
		
		public static void UnEquipVirtualGood(string goodItemId) {
#if UNITY_ANDROID
			AndroidJNI.PushLocalFrame(100);
			AndroidJNIHandler.CallVoid(jniStoreController, "unequipVirtualGood", goodItemId);
			AndroidJNI.PopLocalFrame(IntPtr.Zero);
#elif UNITY_IOS
			storeController_UnEquipVirtualGood(goodItemId);
#endif
		}
		
		public static void StoreOpening() {
			if(!Application.isEditor){
#if UNITY_ANDROID
				AndroidJNI.PushLocalFrame(100);
				using(AndroidJavaClass jniUnityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer")){
					using(AndroidJavaObject jniCurrentActivity = jniUnityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity")) {
						using(AndroidJavaClass jniStoreAssets = new AndroidJavaClass("com.soomla.unity.StoreAssets")) {
							jniStoreAssets.CallStatic("createHandler", jniCurrentActivity);
							using(AndroidJavaObject jniHandler = jniStoreAssets.CallStatic<AndroidJavaObject>("getHandler")) {
								jniStoreController.Call("storeOpening", jniCurrentActivity, jniHandler);
							}
						}
					}
				}
				AndroidJNI.PopLocalFrame(IntPtr.Zero);
#elif UNITY_IOS
				storeController_StoreOpening();
#endif
			}
		}
		
		public static void StoreClosing() {
			if(!Application.isEditor){
#if UNITY_ANDROID
				AndroidJNI.PushLocalFrame(100);
				jniStoreController.Call("storeClosing");
				AndroidJNI.PopLocalFrame(IntPtr.Zero);
#elif UNITY_IOS
				storeController_StoreClosing();
#endif
			}
		}
		
	}
}

