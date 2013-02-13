using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace com.soomla.unity.example {
	public class MuffinRushAssets : IStoreAssets{
		
		public int GetVersion() {
			return 0;
		}
		
		public VirtualCurrency[] GetVirtualCurrencies() {
			return new VirtualCurrency[]{MUFFIN_CURRENCY};
		}
		
	    public VirtualGood[] GetVirtualGoods() {
			return new VirtualGood[] {MUFFINCAKE_GOOD, PAVLOVA_GOOD,CHOCLATECAKE_GOOD, CREAMCUP_GOOD};
		}
		
	    public VirtualCurrencyPack[] GetVirtualCurrencyPacks() {
			return new VirtualCurrencyPack[] {TENMUFF_PACK, FIFTYMUFF_PACK, FORTYMUFF_PACK, THOUSANDMUFF_PACK};
		}
		
	    public VirtualCategory[] GetVirtualCategories() {
			return new VirtualCategory[]{GENERAL_CATEGORY};
		}
		
	    public NonConsumableItem[] GetNonConsumableItems() {
			return new NonConsumableItem[]{NO_ADDS_NONCONS};
		}
		
	    /** Static Final members **/
	
	    public static string MUFFIN_CURRENCY_ITEM_ID = "currency_muffin";
	    public static string TENMUFF_PACK_PRODUCT_ID = "android.test.refunded";
	    public static string FIFTYMUFF_PACK_PRODUCT_ID = "android.test.canceled";
	    public static string FORTYMUFF_PACK_PRODUCT_ID = "android.test.purchased";
	    public static string THOUSANDMUFF_PACK_PRODUCT_ID = "android.test.item_unavailable";
	    public static string NO_ADDS_NONCONS_PRODUCT_ID = "no_ads";
	
	    /** Virtual Categories **/
	    // The muffin rush theme doesn't support categories, so we just put everything under a general category.
	    public static VirtualCategory GENERAL_CATEGORY = new VirtualCategory(
	            "General", 0, VirtualCategory.EquippingModel.MULTIPLE
	    );
	
	    /** Virtual Currencies **/
	    public static VirtualCurrency MUFFIN_CURRENCY = new VirtualCurrency(
	            "Muffins",
	            "",
	            MUFFIN_CURRENCY_ITEM_ID
	    );
	
	    /** Virtual Goods **/
	    private static Dictionary<string, int> MUFFINCAKE_PRICE = new Dictionary<string, int>();
		
	    public static VirtualGood MUFFINCAKE_GOOD = new VirtualGood(
	            "Fruit Cake",                                   // name
	            "Customers buy a double portion on each purchase of this cake", // description
	            new StaticPriceModel(MUFFINCAKE_PRICE), // currency value
	            "fruit_cake",                                   // item id
	            GENERAL_CATEGORY);
	
	    private static Dictionary<string, int> PAVLOVA_PRICE =  new Dictionary<string, int>();
		
	    public static VirtualGood PAVLOVA_GOOD = new VirtualGood(
	    		"Pavlova",                                      // name
	            "Gives customers a sugar rush and they call their friends", // description
	            new StaticPriceModel(PAVLOVA_PRICE),  // currency value
	            "pavlova",                                      // item id
	            GENERAL_CATEGORY
		);
	
	    private static Dictionary<string, int> CHOCLATECAKE_PRICE = new Dictionary<string, int>();
	   
	    public static VirtualGood CHOCLATECAKE_GOOD = new VirtualGood(
	            "Chocolate Cake",                               // name
	            "A classic cake to maximize customer satisfaction",// description
	            new StaticPriceModel(CHOCLATECAKE_PRICE),       // currency value
	            "chocolate_cake",                               // item id
	            GENERAL_CATEGORY
		);
	
	    private static Dictionary<string, int> CREAMCUP_PRICE = new Dictionary<string, int>();

	    public static VirtualGood CREAMCUP_GOOD = new VirtualGood(
	            "Cream Cup",                                    // name
	            "Increase bakery reputation with this original pastry",   // description
	            new StaticPriceModel(CREAMCUP_PRICE),           // currency value
	            "cream_cup",                                    // item id
	            GENERAL_CATEGORY
		);
		
		/// <summary>
		/// Initializing prices.
		/// </summary>
		static MuffinRushAssets() {
			MUFFINCAKE_PRICE.Add(MUFFIN_CURRENCY_ITEM_ID, 225);
			PAVLOVA_PRICE.Add(MUFFIN_CURRENCY_ITEM_ID, 175);
			CHOCLATECAKE_PRICE.Add(MUFFIN_CURRENCY_ITEM_ID, 250);
			CREAMCUP_PRICE.Add(MUFFIN_CURRENCY_ITEM_ID, 50);
		}
	
	    /** Virtual Currency Packs **/
	
	    public static VirtualCurrencyPack TENMUFF_PACK = new VirtualCurrencyPack(
	            "10 Muffins",                                   // name
	            "Test refund of an item",                       // description
	            "muffins_10",                                   // item id
	            TENMUFF_PACK_PRODUCT_ID,                        // product id in Google Market AND App Store !
	            0.99,                                           // actual price in $$
	            10,                                             // number of currencies in the pack
	            MUFFIN_CURRENCY
		);
	
	    public static VirtualCurrencyPack FIFTYMUFF_PACK = new VirtualCurrencyPack(
	            "50 Muffins",                                   // name
	            "Test cancellation of an item",                 // description
	            "muffins_50",                                   // item id
	            FIFTYMUFF_PACK_PRODUCT_ID,                      // product id in Google Market AND App Store !
	            1.99,                                           // actual price in $$
	            50,                                             // number of currencies in the pack
	            MUFFIN_CURRENCY
		);
	
	    public static VirtualCurrencyPack FORTYMUFF_PACK = new VirtualCurrencyPack(
	            "400 Muffins",                                  // name
	            "Test purchase of an item",                     // description
	            "muffins_400",                                  // item id
	            FORTYMUFF_PACK_PRODUCT_ID,                      // product id in Google Market AND App Store !
	            4.99,                                           // actual price in $$
	            400,                                            // number of currencies in the pack
	            MUFFIN_CURRENCY
		);
	
	    public static VirtualCurrencyPack THOUSANDMUFF_PACK = new VirtualCurrencyPack(
	            "1000 Muffins",                                 // name
	            "Test item unavailable",                        // description
	            "muffins_1000",                                 // item id
	            THOUSANDMUFF_PACK_PRODUCT_ID,                   // product id in Google Market AND App Store !
	            8.99,                                           // actual price in $$
	            1000,                                           // number of currencies in the pack
	            MUFFIN_CURRENCY
		);
		
	    /** Google MANAGED Items **/
	
	    public static NonConsumableItem NO_ADDS_NONCONS  = new NonConsumableItem("No Ads", "", "no_ads",
	            NO_ADDS_NONCONS_PRODUCT_ID, 1.99
	    );
		
	}
	
}