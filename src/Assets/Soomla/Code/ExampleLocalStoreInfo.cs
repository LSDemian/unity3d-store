using System;
using System.Collections.Generic;
using UnityEngine;
using com.soomla.unity;

namespace com.soomla.unity.example
{
	public static class ExampleLocalStoreInfo
	{
		/** Currency and Goods balances ! **/
		/** we keep these balances so we won't have to make too many calls to the native (Java/iOS) code **/
		
		public static int CurrencyBalance = 0;
		public static Dictionary<string, int> GoodsBalances = new Dictionary<string, int>();
		public static List<VirtualCurrency> VirtualCurrencies = null;
		public static List<VirtualGood> VirtualGoods = null;
		public static List<VirtualCurrencyPack> VirtualCurrencyPacks = null;
		
		public static void UpdateBalances() {
			CurrencyBalance = StoreInventory.GetCurrencyBalance(VirtualCurrencies[0].ItemId);
			foreach(VirtualGood vg in VirtualGoods){
				GoodsBalances[vg.ItemId] = StoreInventory.GetGoodBalance(vg.ItemId);
			}
		}
		
		public static void Init() {
			VirtualCurrencies = StoreInfo.GetVirtualCurrencies();
			VirtualGoods = StoreInfo.GetVirtualGoods();
			VirtualCurrencyPacks = StoreInfo.GetVirtualCurrencyPacks();	
			UpdateBalances();
		}
	}
}

