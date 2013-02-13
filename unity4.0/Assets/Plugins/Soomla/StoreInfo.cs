using UnityEngine;
using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;

namespace com.soomla.unity
{
	/// <summary>
	/// This class holds the store's meta data including:
	/// - Virtual Currencies definitions
	/// - Virtual Currency Packs definitions
	/// - Virtual Goods definitions
	/// - Virtual Categories definitions
	/// - Virtual Non-Consumable items definitions
	/// </summary>
	public static class StoreInfo
	{
#if UNITY_IOS
		[DllImport ("__Internal")]
		private static extern int storeInfo_GetCategoryById(int catId, out IntPtr json);
		[DllImport ("__Internal")]
		private static extern int storeInfo_GetGoodByItemId(string itemId, out IntPtr json);
		[DllImport ("__Internal")]
		private static extern int storeInfo_GetCurrencyByItemId(string itemId, out IntPtr json);
		[DllImport ("__Internal")]
		private static extern int storeInfo_GetCurrencyPackByItemId(string itemId, out IntPtr json);
		[DllImport ("__Internal")]
		private static extern int storeInfo_GetCurrencyPackByProductId(string productId, out IntPtr json);
		[DllImport ("__Internal")]
		private static extern int storeInfo_GetNonConsumableByProductId(string productId, out IntPtr json);
		[DllImport ("__Internal")]
		private static extern int storeInfo_GetVirtualCurrencies(out IntPtr json);
		[DllImport ("__Internal")]
		private static extern int storeInfo_GetVirtualGoods(out IntPtr json);
		[DllImport ("__Internal")]
		private static extern int storeInfo_GetVirtualCurrencyPacks(out IntPtr json);
		[DllImport ("__Internal")]
		private static extern int storeInfo_GetNonConsumableItems(out IntPtr json);
		[DllImport ("__Internal")]
		private static extern int storeInfo_GetVirtualCategories(out IntPtr json);
		[DllImport ("__Internal")]
		private static extern void storeAssets_SetVersion(int version);
		[DllImport ("__Internal")]
		private static extern void storeAssets_Init();
		[DllImport ("__Internal")]
		private static extern void storeAssets_AddCategory(string name, int id, string equipping);
		[DllImport ("__Internal")]
		private static extern void storeAssets_AddCurrency(string name, string description, string itemID);
		[DllImport ("__Internal")]
		private static extern void storeAssets_AddCurrencyPack(string name, string description, string itemID, double price, string productID, int currencyAmount, string currencyItemId);
		[DllImport ("__Internal")]
		private static extern void storeAssets_AddVirtualGood(string name, string description, string itemID, int categoryIndex, string priceModelJSON);
		[DllImport ("__Internal")]
		private static extern void storeAssets_AddNonConsumable(string name, string description, string itemID, double price, string productID);
#endif
		
#if UNITY_ANDROID
//		private static AndroidJavaClass jniStoreInfo = new AndroidJavaClass("com.soomla.unity.StoreInfo");
#endif
			
		public static void Initialize(IStoreAssets storeAssets) {
#if UNITY_ANDROID
			Debug.Log("pushing data to StoreAssets on java side");
			
			AndroidJNI.PushLocalFrame(100);
			
			using(AndroidJavaClass jniStoreAssets = new AndroidJavaClass("com.soomla.unity.StoreAssets")) {
			
				//storeAssets version
				jniStoreAssets.CallStatic("setVersion", storeAssets.GetVersion());
				
				Dictionary<int, AndroidJavaObject> jniCategories = new Dictionary<int, AndroidJavaObject>();
				//virtual categories
				using(AndroidJavaObject jniVirtualCategories = jniStoreAssets.GetStatic<AndroidJavaObject>("virtualCategories")) {
					jniVirtualCategories.Call("clear");
					foreach(VirtualCategory vc in storeAssets.GetVirtualCategories()){
						jniCategories[vc.Id] = vc.toAndroidJavaObject(jniStoreAssets);
						jniVirtualCategories.Call<bool>("add", jniCategories[vc.Id]);
					}
				}

				//non consumable items
				using(AndroidJavaObject jniNonConsumableItems = jniStoreAssets.GetStatic<AndroidJavaObject>("nonConsumableItems")) {
					jniNonConsumableItems.Call("clear");
					foreach(NonConsumableItem non in storeAssets.GetNonConsumableItems()){
						using(AndroidJavaObject obj = non.toAndroidJavaObject()) {
							jniNonConsumableItems.Call<bool>("add", obj);
						}
					}
				}
				
				Dictionary<string, AndroidJavaObject> jniCurrencies = new Dictionary<string, AndroidJavaObject>();
				//Virtual currencies
				using(AndroidJavaObject jniVirtualCurrencies = jniStoreAssets.GetStatic<AndroidJavaObject>("virtualCurrencies")) {
					jniVirtualCurrencies.Call("clear");
					foreach(VirtualCurrency vc in storeAssets.GetVirtualCurrencies()){
						jniCurrencies[vc.ItemId] = vc.toAndroidJavaObject();
						jniVirtualCurrencies.Call<bool>("add", jniCurrencies[vc.ItemId]);
					}
				}
				
				//Virtual currency packs
				using(AndroidJavaObject jniVirtualCurrencyPacks = jniStoreAssets.GetStatic<AndroidJavaObject>("virtualCurrencyPacks")) {
					jniVirtualCurrencyPacks.Call("clear");
					foreach(VirtualCurrencyPack vcp in storeAssets.GetVirtualCurrencyPacks()){
						using(AndroidJavaObject obj = vcp.toAndroidJavaObject(jniCurrencies[vcp.Currency.ItemId])) {
							jniVirtualCurrencyPacks.Call<bool>("add", obj);
						}
					}
				}

				//Virtual goods
				using(AndroidJavaObject jniVirtualGoods = jniStoreAssets.GetStatic<AndroidJavaObject>("virtualGoods")) {
					jniVirtualGoods.Call("clear");
					foreach(VirtualGood vg in storeAssets.GetVirtualGoods()){
						using(AndroidJavaObject obj = vg.toAndroidJavaObject(jniStoreAssets, jniCategories[vg.Category.Id])) {
							jniVirtualGoods.Call<bool>("add", obj);
						}
					}
				}
				
				foreach(KeyValuePair<int, AndroidJavaObject> kvp in jniCategories) {
					kvp.Value.Dispose();
				}
				
				foreach(KeyValuePair<string, AndroidJavaObject> kvp in jniCurrencies) {
					kvp.Value.Dispose();
				}
			}
			
			AndroidJNI.PopLocalFrame(IntPtr.Zero);
			Debug.Log("done! (pushing data to StoreAssets on java side)");
#elif UNITY_IOS
			Debug.Log("pushing data to StoreAssets on ios side");
			
			storeAssets_Init();
			
			// storeAssets version
			storeAssets_SetVersion(storeAssets.GetVersion());
			//virtual categories
			foreach(VirtualCategory vc in storeAssets.GetVirtualCategories()){
				storeAssets_AddCategory(vc.Name, vc.Id, vc.Equipping.ToString());
			}
			//ios market items
			foreach(NonConsumableItem non in storeAssets.GetNonConsumableItems()){
				storeAssets_AddNonConsumable(non.Name, non.Description, non.ItemId, non.MarketItem.Price, non.MarketItem.ProductId);
			}
			//Virtual currencies
			foreach(VirtualCurrency vc in storeAssets.GetVirtualCurrencies()){
				storeAssets_AddCurrency(vc.Name, vc.Description, vc.ItemId);
			}
			//Virtual currency packs
			foreach(VirtualCurrencyPack vcp in storeAssets.GetVirtualCurrencyPacks()){
				storeAssets_AddCurrencyPack(vcp.Name, vcp.Description, vcp.ItemId, vcp.MarketItem.Price, vcp.MarketItem.ProductId, vcp.CurrencyAmount, vcp.Currency.ItemId);
			}
			//Virtual goods
			foreach(VirtualGood vg in storeAssets.GetVirtualGoods()){
				string json = vg.PriceModel.toJSONObject().print();
				storeAssets_AddVirtualGood(vg.Name, vg.Description, vg.ItemId, vg.Category.Id, json);
			}
			
			Debug.Log("done! (pushing data to StoreAssets on ios side)");
#endif
		}
		
		public static List<NonConsumableItem> GetNonConsumableItems() {
			List<NonConsumableItem> nonConsumableItems = new List<NonConsumableItem>();
#if UNITY_ANDROID
			AndroidJNI.PushLocalFrame(100);
			using(AndroidJavaObject jniNonConsumableItems = new AndroidJavaClass("com.soomla.unity.StoreInfo").CallStatic<AndroidJavaObject>("getNonConsumableItems")) {
				for(int i=0; i<jniNonConsumableItems.Call<int>("size"); i++) {
					using(AndroidJavaObject jniNon = jniNonConsumableItems.Call<AndroidJavaObject>("get", i)) {
						nonConsumableItems.Add(new NonConsumableItem(jniNon));
					}
				}
			}
			AndroidJNI.PopLocalFrame(IntPtr.Zero);
#elif UNITY_IOS
			IntPtr p = IntPtr.Zero;
			int err = storeInfo_GetNonConsumableItems(out p);
				
			IOS_ErrorCodes.CheckAndThrowException(err);
			
			string nonConsumableJson = Marshal.PtrToStringAnsi(p);
			Marshal.FreeHGlobal(p);
			
			JSONObject nonConsArr = new JSONObject(nonConsumableJson);
			foreach(JSONObject obj in nonConsArr.list) {
				nonConsumableItems.Add(new NonConsumableItem(obj));
			}
#endif
			return nonConsumableItems;
		}
		
		public static List<VirtualCategory> GetVirtualCategories() {
			List<VirtualCategory> virtualCategories = new List<VirtualCategory>();
#if UNITY_ANDROID
			AndroidJNI.PushLocalFrame(100);
			using(AndroidJavaObject jniVirtualCategories = new AndroidJavaClass("com.soomla.unity.StoreInfo").CallStatic<AndroidJavaObject>("getVirtualCategories")) {
				for(int i=0; i<jniVirtualCategories.Call<int>("size"); i++) {
					using(AndroidJavaObject jniCat = jniVirtualCategories.Call<AndroidJavaObject>("get", i)) {
						virtualCategories.Add(new VirtualCategory(jniCat));
					}
				}
			}
			AndroidJNI.PopLocalFrame(IntPtr.Zero);
#elif UNITY_IOS
			IntPtr p = IntPtr.Zero;
			int err = storeInfo_GetVirtualCategories(out p);
				
			IOS_ErrorCodes.CheckAndThrowException(err);
			
			string categoriesJson = Marshal.PtrToStringAnsi(p);
			Marshal.FreeHGlobal(p);
			
			JSONObject categoriesArr = new JSONObject(categoriesJson);
			foreach(JSONObject obj in categoriesArr.list) {
				virtualCategories.Add(new VirtualCategory(obj));
			}
#endif
			return virtualCategories;
		}
		
		public static List<VirtualGood> GetVirtualGoods() {
			List<VirtualGood> virtualGoods = new List<VirtualGood>();
#if UNITY_ANDROID
			AndroidJNI.PushLocalFrame(100);
			using(AndroidJavaObject jniVirtualGoods = new AndroidJavaClass("com.soomla.unity.StoreInfo").CallStatic<AndroidJavaObject>("getVirtualGoods")) {
				for(int i=0; i<jniVirtualGoods.Call<int>("size"); i++) {
					using(AndroidJavaObject jniGood = jniVirtualGoods.Call<AndroidJavaObject>("get", i)) {
						virtualGoods.Add(new VirtualGood(jniGood));
					}
				}
			}
			AndroidJNI.PopLocalFrame(IntPtr.Zero);
#elif UNITY_IOS
			IntPtr p = IntPtr.Zero;
			int err = storeInfo_GetVirtualGoods(out p);
				
			IOS_ErrorCodes.CheckAndThrowException(err);
			
			string goodsJson = Marshal.PtrToStringAnsi(p);
			Marshal.FreeHGlobal(p);
			
			JSONObject goodsArr = new JSONObject(goodsJson);
			foreach(JSONObject obj in goodsArr.list) {
				virtualGoods.Add(new VirtualGood(obj));
			}
#endif
			return virtualGoods;
		}
		
		public static List<VirtualCurrencyPack> GetVirtualCurrencyPacks() {
			List<VirtualCurrencyPack> vcps = new List<VirtualCurrencyPack>();
#if UNITY_ANDROID
			AndroidJNI.PushLocalFrame(100);
			using(AndroidJavaObject jniVirtualCurrencyPacks = new AndroidJavaClass("com.soomla.unity.StoreInfo").CallStatic<AndroidJavaObject>("getCurrencyPacks")) {
				for(int i=0; i<jniVirtualCurrencyPacks.Call<int>("size"); i++) {
					using(AndroidJavaObject jnivcp = jniVirtualCurrencyPacks.Call<AndroidJavaObject>("get", i)) {
						vcps.Add(new VirtualCurrencyPack(jnivcp));
					}
				}
			}
			AndroidJNI.PopLocalFrame(IntPtr.Zero);
#elif UNITY_IOS
			IntPtr p = IntPtr.Zero;
			int err = storeInfo_GetVirtualCurrencyPacks(out p);
				
			IOS_ErrorCodes.CheckAndThrowException(err);
			
			string packsJson = Marshal.PtrToStringAnsi(p);
			Marshal.FreeHGlobal(p);
			
			JSONObject packsArr = new JSONObject(packsJson);
			foreach(JSONObject obj in packsArr.list) {
				vcps.Add(new VirtualCurrencyPack(obj));
			}
#endif
			return vcps;
		}
		
		public static List<VirtualCurrency> GetVirtualCurrencies() {
			List<VirtualCurrency> vcs = new List<VirtualCurrency>();
#if UNITY_ANDROID
			AndroidJNI.PushLocalFrame(100);
			using(AndroidJavaObject jniVirtualCurrencies = new AndroidJavaClass("com.soomla.unity.StoreInfo").CallStatic<AndroidJavaObject>("getVirtualCurrencies")) {
				for(int i=0; i<jniVirtualCurrencies.Call<int>("size"); i++) {
					using(AndroidJavaObject jnivc = jniVirtualCurrencies.Call<AndroidJavaObject>("get", i)) {
						vcs.Add(new VirtualCurrency(jnivc));
					}
				}
			}
			AndroidJNI.PopLocalFrame(IntPtr.Zero);
#elif UNITY_IOS
			IntPtr p = IntPtr.Zero;
			int err = storeInfo_GetVirtualCurrencies(out p);
				
			IOS_ErrorCodes.CheckAndThrowException(err);
			
			string currenciesJson = Marshal.PtrToStringAnsi(p);
			Marshal.FreeHGlobal(p);
			
			JSONObject currenciesArr = new JSONObject(currenciesJson);
			foreach(JSONObject obj in currenciesArr.list) {
				vcs.Add(new VirtualCurrency(obj));
			}
#endif
			return vcs;
		}

		public static NonConsumableItem GetNonConsumableItemByProductId(string productId) {
#if UNITY_ANDROID
			NonConsumableItem non = null;
			AndroidJNI.PushLocalFrame(100);
			using(AndroidJavaObject jniNonConsumableItem = AndroidJNIHandler.CallStatic<AndroidJavaObject>(
				new AndroidJavaClass("com.soomla.unity.StoreInfo"), "getNonConsumableByProductId", productId)) {
				non = new NonConsumableItem(jniNonConsumableItem);
			}
			AndroidJNI.PopLocalFrame(IntPtr.Zero);
			return non;
#elif UNITY_IOS
			IntPtr p = IntPtr.Zero;
			int err = storeInfo_GetNonConsumableByProductId(productId, out p);
				
			IOS_ErrorCodes.CheckAndThrowException(err);
			
			string nonConsJson = Marshal.PtrToStringAnsi(p);
			Marshal.FreeHGlobal(p);
			
			JSONObject obj = new JSONObject(nonConsJson);
			return new NonConsumableItem(obj);
#else
			return null;
#endif
		}
		
		public static VirtualCurrency GetVirtualCurrencyByItemId(string itemId) {
#if UNITY_ANDROID
			VirtualCurrency vc = null;
			AndroidJNI.PushLocalFrame(100);
			using(AndroidJavaObject jniVirtualCurrency = AndroidJNIHandler.CallStatic<AndroidJavaObject>(
				new AndroidJavaClass("com.soomla.unity.StoreInfo"),"getVirtualCurrencyByItemId", itemId)) {
				vc = new VirtualCurrency(jniVirtualCurrency);
			}
			AndroidJNI.PopLocalFrame(IntPtr.Zero);
			return vc;
#elif UNITY_IOS
			IntPtr p = IntPtr.Zero;
			int err = storeInfo_GetCurrencyByItemId(itemId, out p);
				
			IOS_ErrorCodes.CheckAndThrowException(err);
			
			string json = Marshal.PtrToStringAnsi(p);
			Marshal.FreeHGlobal(p);
			
			JSONObject obj = new JSONObject(json);
			return new VirtualCurrency(obj);
#else
			return null;
#endif
		}
		
		public static VirtualGood GetVirtualGoodByItemId(string itemId) {
#if UNITY_ANDROID
			VirtualGood vg = null;
			AndroidJNI.PushLocalFrame(100);
			using(AndroidJavaObject jniVirtualGood = AndroidJNIHandler.CallStatic<AndroidJavaObject>(
				new AndroidJavaClass("com.soomla.unity.StoreInfo"),"getVirtualGoodByItemId", itemId)) {
				vg = new VirtualGood(jniVirtualGood);	
			}
			AndroidJNI.PopLocalFrame(IntPtr.Zero);
			return vg;
#elif UNITY_IOS
			IntPtr p = IntPtr.Zero;
			int err = storeInfo_GetGoodByItemId(itemId, out p);
				
			IOS_ErrorCodes.CheckAndThrowException(err);
			
			string json = Marshal.PtrToStringAnsi(p);
			Marshal.FreeHGlobal(p);
			
			JSONObject obj = new JSONObject(json);
			return new VirtualGood(obj);
#else
			return null;
#endif
		}
		
		public static VirtualCurrencyPack GetPackByItemId(string itemId) {
#if UNITY_ANDROID
			VirtualCurrencyPack vcp = null;
			AndroidJNI.PushLocalFrame(100);
			using(AndroidJavaObject jniVirtualCurrencyPack = AndroidJNIHandler.CallStatic<AndroidJavaObject>(
				new AndroidJavaClass("com.soomla.unity.StoreInfo"),"getPackByItemId", itemId)) {
			
				vcp = new VirtualCurrencyPack(jniVirtualCurrencyPack);
			}
			AndroidJNI.PopLocalFrame(IntPtr.Zero);
			return vcp;
#elif UNITY_IOS
			IntPtr p = IntPtr.Zero;
			int err = storeInfo_GetCurrencyPackByItemId(itemId, out p);
				
			IOS_ErrorCodes.CheckAndThrowException(err);
			
			string json = Marshal.PtrToStringAnsi(p);
			Marshal.FreeHGlobal(p);
			
			JSONObject obj = new JSONObject(json);
			return new VirtualCurrencyPack(obj);
#else
			return null;
#endif
		}
		
		public static VirtualCurrencyPack GetPackByProductId(string productId) {
#if UNITY_ANDROID
			VirtualCurrencyPack vcp = null;
			AndroidJNI.PushLocalFrame(100);
			using(AndroidJavaObject jniVirtualCurrencyPack = AndroidJNIHandler.CallStatic<AndroidJavaObject>(
				new AndroidJavaClass("com.soomla.unity.StoreInfo"),"getPackByGoogleProductId", productId)) {
				vcp = new VirtualCurrencyPack(jniVirtualCurrencyPack);
			}
			return vcp;
#elif UNITY_IOS
			IntPtr p = IntPtr.Zero;
			int err = storeInfo_GetCurrencyPackByProductId(productId, out p);
				
			IOS_ErrorCodes.CheckAndThrowException(err);
			
			string json = Marshal.PtrToStringAnsi(p);
			Marshal.FreeHGlobal(p);
			
			JSONObject obj = new JSONObject(json);
			return new VirtualCurrencyPack(obj);
#else
			return null;
#endif
		}
		
		public static VirtualCategory GetVirtualCategoryById(int id) {
#if UNITY_ANDROID
			VirtualCategory vc = null;
			AndroidJNI.PushLocalFrame(100);
			using(AndroidJavaObject jniVirtualCategory = AndroidJNIHandler.CallStatic<AndroidJavaObject>(
				new AndroidJavaClass("com.soomla.unity.StoreInfo"),"getVirtualCategoryById", id)) {
				vc = new VirtualCategory(jniVirtualCategory);
			}
			AndroidJNI.PopLocalFrame(IntPtr.Zero);
			return vc;
#elif UNITY_IOS
			IntPtr p = IntPtr.Zero;
			int err = storeInfo_GetCategoryById(id, out p);
				
			IOS_ErrorCodes.CheckAndThrowException(err);
			
			string json = Marshal.PtrToStringAnsi(p);
			Marshal.FreeHGlobal(p);
			
			JSONObject obj = new JSONObject(json);
			return new VirtualCategory(obj);
#else
			return null;
#endif
		}
	}
}

