using UnityEngine;
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
		private static extern int storeController_BuyCurrencyPack(string productId);
		[DllImport ("__Internal")]
		private static extern int storeController_BuyVirtualGood(string itemId);
		[DllImport ("__Internal")]
		private static extern int storeController_BuyNonConsumableItem(string productId);
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
		private static AndroidJavaObject jniCurrentActivity = null;
#endif
		
		public static void Initialize(IStoreAssets storeAssets) {
			if (string.IsNullOrEmpty(Soomla.GetInstance().publicKey) || string.IsNullOrEmpty(Soomla.GetInstance().customSecret) || string.IsNullOrEmpty(Soomla.GetInstance().soomSec)) {
				Debug.Log("SOOMLA/UNITY MISSING publickKey or customSecret or soomSec !!! Stopping here !!");
				throw new ExitGUIException();
			}
			//init SOOM_SEC
#if UNITY_ANDROID
			AndroidJavaClass jniStoreAssets = new AndroidJavaClass("com.soomla.unity.StoreAssets");
			jniStoreAssets.CallStatic("setSoomSec", Soomla.GetInstance().soomSec);
#elif UNITY_IOS
			storeController_SetSoomSec(Soomla.GetInstance().soomSec);
#endif
			
			StoreInventory.Init();
			StoreInfo.Initialize(storeAssets);
#if UNITY_ANDROID
			AndroidJavaClass jniUnityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"); 
			jniCurrentActivity = jniUnityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
			
			AndroidJavaObject jniStoreAssetsInstance = new AndroidJavaObject("com.soomla.unity.StoreAssets");
			AndroidJavaClass jniStoreControllerClass = new AndroidJavaClass("com.soomla.store.StoreController");
			jniStoreController = jniStoreControllerClass.CallStatic<AndroidJavaObject>("getInstance");
			jniStoreController.Call("initialize", jniStoreAssetsInstance, Soomla.GetInstance().publicKey, Soomla.GetInstance().customSecret);
			
			//init StoreEventHandlers
			AndroidJavaClass jniStoreEventHandlersClass = new AndroidJavaClass("com.soomla.store.StoreEventHandlers");
			AndroidJavaObject jniStoreEventHandlersInstance = jniStoreEventHandlersClass.CallStatic<AndroidJavaObject>("getInstance");
			AndroidJavaObject jniUnityEventHandler = new AndroidJavaObject("com.soomla.unity.EventHandler");
			jniStoreEventHandlersInstance.Call("addEventHandler",jniUnityEventHandler);
			
#elif UNITY_IOS
			storeController_Init(Soomla.GetInstance().customSecret);
#endif
		}
		
		
		public static void BuyCurrencyPack(string packProductId) {
#if UNITY_ANDROID
			AndroidJNIHandler.CallVoid(jniStoreController, "buyCurrencyPack", packProductId);
#elif UNITY_IOS
			storeController_BuyCurrencyPack(packProductId);
#endif
		}
		
		public static void BuyVirtualGood(string goodItemId) {
#if UNITY_ANDROID
			AndroidJNIHandler.CallVoid(jniStoreController, "buyVirtualGood", goodItemId);
#elif UNITY_IOS
			storeController_BuyVirtualGood(goodItemId);
#endif
		}
		
		public static void BuyNonConsumableItem(string nonConsProductId) {
#if UNITY_ANDROID
			AndroidJNIHandler.CallVoid(jniStoreController, "buyManagedItem", nonConsProductId);
#elif UNITY_IOS
			storeController_BuyNonConsumableItem(nonConsProductId);
#endif
		}
		
		public static void EquipVirtualGood(string goodItemId) {
#if UNITY_ANDROID
			AndroidJNIHandler.CallVoid(jniStoreController, "equipVirtualGood", goodItemId);
#elif UNITY_IOS
			storeController_EquipVirtualGood(goodItemId);
#endif
		}
		
		public static void UnEquipVirtualGood(string goodItemId) {
#if UNITY_ANDROID
			AndroidJNIHandler.CallVoid(jniStoreController, "unequipVirtualGood", goodItemId);
#elif UNITY_IOS
			storeController_UnEquipVirtualGood(goodItemId);
#endif
		}
		
		public static void StoreOpening() {
			if(!Application.isEditor){
#if UNITY_ANDROID
				AndroidJavaClass jniStoreAssets = new AndroidJavaClass("com.soomla.unity.StoreAssets");
				jniStoreAssets.CallStatic("createHandler", jniCurrentActivity);
				AndroidJavaObject jniHandler = jniStoreAssets.CallStatic<AndroidJavaObject>("getHandler");
				jniStoreController.Call("storeOpening", jniCurrentActivity, jniHandler);
#elif UNITY_IOS
				storeController_StoreOpening();
#endif
			}
		}
		
		public static void StoreClosing() {
			if(!Application.isEditor){
#if UNITY_ANDROID
				jniStoreController.Call("storeClosing");
#elif UNITY_IOS
				storeController_StoreClosing();
#endif
			}
		}
		
	}
}

