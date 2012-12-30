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
		private static extern void storeAssets_Init();
		[DllImport ("__Internal")]
		private static extern void storeAssets_AddCategory(string name, int id);
		[DllImport ("__Internal")]
		private static extern void storeAssets_AddCurrency(string name, string description, string itemID);
		[DllImport ("__Internal")]
		private static extern void storeAssets_AddCurrencyPack(string name, string description, string itemID, double price, string productID, int currencyAmount, string currencyItemId);
		[DllImport ("__Internal")]
		private static extern void storeAssets_AddVirtualGood(string name, string description, string itemID, int categoryIndex, bool equipStatus, string priceModelJSON);
		[DllImport ("__Internal")]
		private static extern void storeAssets_AddNonConsumable(string productId);
#endif
		
#if UNITY_ANDROID
		private static AndroidJavaClass jniStoreInfo = new AndroidJavaClass("com.soomla.unity.StoreInfo");
#endif
			
		public static void Initialize(IStoreAssets storeAssets) {
#if UNITY_ANDROID
			Debug.Log("pushing data to StoreAssets on java side");
			
			AndroidJavaClass jniStoreAssets = new AndroidJavaClass("com.soomla.unity.StoreAssets");
			
			//virtual categories
			AndroidJavaObject jniVirtualCategories = jniStoreAssets.GetStatic<AndroidJavaObject>("virtualCategories");
			jniVirtualCategories.Call("clear");
			Dictionary<int, AndroidJavaObject> jniCategories = new Dictionary<int, AndroidJavaObject>();
			foreach(VirtualCategory vc in storeAssets.GetVirtualCategories()){
				jniCategories[vc.Id] = vc.toAndroidJavaObject();
				jniVirtualCategories.Call<bool>("add", jniCategories[vc.Id]);
			}
			//google market items
			AndroidJavaObject jniGoogleManagedItems = jniStoreAssets.GetStatic<AndroidJavaObject>("googleManagedItems");
			jniGoogleManagedItems.Call("clear");
			foreach(MarketItem mi in storeAssets.GetNonConsumableItems()){
				jniGoogleManagedItems.Call<bool>("add", mi.toAndroidJavaObject(jniStoreAssets));
			}
			//Virtual currencies
			AndroidJavaObject jniVirtualCurrencies = jniStoreAssets.GetStatic<AndroidJavaObject>("virtualCurrencies");
			Dictionary<string, AndroidJavaObject> jniCurrencies = new Dictionary<string, AndroidJavaObject>();
			foreach(VirtualCurrency vc in storeAssets.GetVirtualCurrencies()){
				jniCurrencies[vc.ItemId] = vc.toAndroidJavaObject();
				jniVirtualCurrencies.Call<bool>("add", jniCurrencies[vc.ItemId]);
			}
			//Virtual currency packs
			AndroidJavaObject jniVirtualCurrencyPacks = jniStoreAssets.GetStatic<AndroidJavaObject>("virtualCurrencyPacks");
			foreach(VirtualCurrencyPack vcp in storeAssets.GetVirtualCurrencyPacks()){
				jniVirtualCurrencyPacks.Call<bool>("add", vcp.toAndroidJavaObject(jniCurrencies[vcp.Currency.ItemId]));
			}
			//Virtual goods
			AndroidJavaObject jniVirtualGoods = jniStoreAssets.GetStatic<AndroidJavaObject>("virtualGoods");
			foreach(VirtualGood vg in storeAssets.GetVirtualGoods()){
				jniVirtualGoods.Call<bool>("add", vg.toAndroidJavaObject(jniStoreAssets, jniCategories[vg.Category.Id]));
			}
			
			Debug.Log("done! (pushing data to StoreAssets on java side)");
#elif UNITY_IOS
			Debug.Log("pushing data to StoreAssets on ios side");
			
			storeAssets_Init();
			
			//virtual categories
			foreach(VirtualCategory vc in storeAssets.GetVirtualCategories()){
				storeAssets_AddCategory(vc.Name, vc.Id);
			}
			//ios market items
			foreach(MarketItem mi in storeAssets.GetNonConsumableItems()){
				storeAssets_AddNonConsumable(mi.ProductId);
			}
			//Virtual currencies
			foreach(VirtualCurrency vc in storeAssets.GetVirtualCurrencies()){
				storeAssets_AddCurrency(vc.Name, vc.Description, vc.ItemId);
			}
			//Virtual currency packs
			foreach(VirtualCurrencyPack vcp in storeAssets.GetVirtualCurrencyPacks()){
				storeAssets_AddCurrencyPack(vcp.Name, vcp.Description, vcp.ItemId, vcp.Price, vcp.MarketItem.ProductId, vcp.CurrencyAmount, vcp.Currency.ItemId);
			}
			//Virtual goods
			foreach(VirtualGood vg in storeAssets.GetVirtualGoods()){
				string json = vg.PriceModel.toJSONObject().print();
				storeAssets_AddVirtualGood(vg.Name, vg.Description, vg.ItemId, vg.Category.Id, vg.Equiped, json);
			}
			
			Debug.Log("done! (pushing data to StoreAssets on ios side)");
#endif
		}
		
		public static List<MarketItem> GetNonConsumableItems() {
			List<MarketItem> nonConsumableItems = new List<MarketItem>();
#if UNITY_ANDROID
			AndroidJavaObject jniGoogleManagedItems = jniStoreInfo.CallStatic<AndroidJavaObject>("getGoogleManagedItems");
			for(int i=0; i<jniGoogleManagedItems.Call<int>("size"); i++) {
				AndroidJavaObject jniGmi = jniGoogleManagedItems.Call<AndroidJavaObject>("get", i);
				nonConsumableItems.Add(new MarketItem(jniGmi));
			}
#elif UNITY_IOS
			IntPtr p = IntPtr.Zero;
			int err = storeInfo_GetNonConsumableItems(out p);
				
			IOS_ErrorCodes.CheckAndThrowException(err);
			
			string nonConsumableJson = Marshal.PtrToStringAnsi(p);
			Marshal.FreeHGlobal(p);
			
			JSONObject nonConsArr = new JSONObject(nonConsumableJson);
			foreach(JSONObject obj in nonConsArr.list) {
				nonConsumableItems.Add(new MarketItem(obj));
			}
#endif
			return nonConsumableItems;
		}
		
		public static List<VirtualCategory> GetVirtualCategories() {
			List<VirtualCategory> virtualCategories = new List<VirtualCategory>();
#if UNITY_ANDROID
			AndroidJavaObject jniVirtualCategories = jniStoreInfo.CallStatic<AndroidJavaObject>("getVirtualCategories");
			for(int i=0; i<jniVirtualCategories.Call<int>("size"); i++) {
				AndroidJavaObject jniCat = jniVirtualCategories.Call<AndroidJavaObject>("get", i);
				virtualCategories.Add(new VirtualCategory(jniCat));
			}
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
			AndroidJavaObject jniVirtualGoods = jniStoreInfo.CallStatic<AndroidJavaObject>("getVirtualGoods");
			for(int i=0; i<jniVirtualGoods.Call<int>("size"); i++) {
				AndroidJavaObject jniGood = jniVirtualGoods.Call<AndroidJavaObject>("get", i);
				virtualGoods.Add(new VirtualGood(jniGood));
			}
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
			AndroidJavaObject jniVirtualCurrencyPacks = jniStoreInfo.CallStatic<AndroidJavaObject>("getCurrencyPacks");
			for(int i=0; i<jniVirtualCurrencyPacks.Call<int>("size"); i++) {
				AndroidJavaObject jnivcp = jniVirtualCurrencyPacks.Call<AndroidJavaObject>("get", i);
				vcps.Add(new VirtualCurrencyPack(jnivcp));
			}
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
			AndroidJavaObject jniVirtualCurrencies = jniStoreInfo.CallStatic<AndroidJavaObject>("getVirtualCurrencies");
			for(int i=0; i<jniVirtualCurrencies.Call<int>("size"); i++) {
				AndroidJavaObject jnivc = jniVirtualCurrencies.Call<AndroidJavaObject>("get", i);
				vcs.Add(new VirtualCurrency(jnivc));
			}
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

		public static MarketItem GetNonConsumableItemByProductId(string productId) {
#if UNITY_ANDROID
			AndroidJavaObject jniGoogleManagedItem = AndroidJNIHandler.CallStatic<AndroidJavaObject>(
				jniStoreInfo, "getGoogleManagedItemByProductId", productId);
			return new MarketItem(jniGoogleManagedItem);
#elif UNITY_IOS
			IntPtr p = IntPtr.Zero;
			int err = storeInfo_GetNonConsumableByProductId(productId, out p);
				
			IOS_ErrorCodes.CheckAndThrowException(err);
			
			string nonConsJson = Marshal.PtrToStringAnsi(p);
			Marshal.FreeHGlobal(p);
			
			JSONObject obj = new JSONObject(nonConsJson);
			return new MarketItem(obj);
#endif
		}
		
		public static VirtualCurrency GetVirtualCurrencyByItemId(string itemId) {
#if UNITY_ANDROID
			AndroidJavaObject jniVirtualCurrency = AndroidJNIHandler.CallStatic<AndroidJavaObject>(
				jniStoreInfo,"getVirtualCurrencyByItemId", itemId);
			return new VirtualCurrency(jniVirtualCurrency);
#elif UNITY_IOS
			IntPtr p = IntPtr.Zero;
			int err = storeInfo_GetCurrencyByItemId(itemId, out p);
				
			IOS_ErrorCodes.CheckAndThrowException(err);
			
			string json = Marshal.PtrToStringAnsi(p);
			Marshal.FreeHGlobal(p);
			
			JSONObject obj = new JSONObject(json);
			return new VirtualCurrency(obj);
#endif
		}
		
		public static VirtualGood GetVirtualGoodByItemId(string itemId) {
#if UNITY_ANDROID
			AndroidJavaObject jniVirtualGood = AndroidJNIHandler.CallStatic<AndroidJavaObject>(
				jniStoreInfo,"getVirtualGoodByItemId", itemId);
			return new VirtualGood(jniVirtualGood);	
#elif UNITY_IOS
			IntPtr p = IntPtr.Zero;
			int err = storeInfo_GetGoodByItemId(itemId, out p);
				
			IOS_ErrorCodes.CheckAndThrowException(err);
			
			string json = Marshal.PtrToStringAnsi(p);
			Marshal.FreeHGlobal(p);
			
			JSONObject obj = new JSONObject(json);
			return new VirtualGood(obj);
#endif
		}
		
		public static VirtualCurrencyPack GetPackByItemId(string itemId) {
#if UNITY_ANDROID
			AndroidJavaObject jniVirtualCurrencyPack = AndroidJNIHandler.CallStatic<AndroidJavaObject>(
				jniStoreInfo,"getPackByItemId", itemId);
			
			return new VirtualCurrencyPack(jniVirtualCurrencyPack);
#elif UNITY_IOS
			IntPtr p = IntPtr.Zero;
			int err = storeInfo_GetCurrencyPackByItemId(itemId, out p);
				
			IOS_ErrorCodes.CheckAndThrowException(err);
			
			string json = Marshal.PtrToStringAnsi(p);
			Marshal.FreeHGlobal(p);
			
			JSONObject obj = new JSONObject(json);
			return new VirtualCurrencyPack(obj);
#endif
		}
		
		public static VirtualCurrencyPack GetPackByProductId(string productId) {
#if UNITY_ANDROID
			AndroidJavaObject jniVirtualCurrencyPack = AndroidJNIHandler.CallStatic<AndroidJavaObject>(
				jniStoreInfo,"getPackByGoogleProductId", productId);
			return new VirtualCurrencyPack(jniVirtualCurrencyPack);
#elif UNITY_IOS
			IntPtr p = IntPtr.Zero;
			int err = storeInfo_GetCurrencyPackByProductId(productId, out p);
				
			IOS_ErrorCodes.CheckAndThrowException(err);
			
			string json = Marshal.PtrToStringAnsi(p);
			Marshal.FreeHGlobal(p);
			
			JSONObject obj = new JSONObject(json);
			return new VirtualCurrencyPack(obj);
#endif
		}
		
		public static VirtualCategory GetVirtualCategoryById(int id) {
#if UNITY_ANDROID
			AndroidJavaObject jniVirtualCategory = AndroidJNIHandler.CallStatic<AndroidJavaObject>(
				jniStoreInfo,"getVirtualCategoryById", id);
			return new VirtualCategory(jniVirtualCategory);
#elif UNITY_IOS
			IntPtr p = IntPtr.Zero;
			int err = storeInfo_GetCategoryById(id, out p);
				
			IOS_ErrorCodes.CheckAndThrowException(err);
			
			string json = Marshal.PtrToStringAnsi(p);
			Marshal.FreeHGlobal(p);
			
			JSONObject obj = new JSONObject(json);
			return new VirtualCategory(obj);
#endif
		}
	}
}

