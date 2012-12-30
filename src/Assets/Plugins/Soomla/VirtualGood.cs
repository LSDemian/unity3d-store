using UnityEngine;
using System.Collections;


namespace com.soomla.unity{	

	/// <summary>
	/// This is a representation of the application's virtual good.
	/// Virtual goods are bought with one or more VirtualCurrency. The price is determined by the PriceModel.
	/// </summary>
	public class VirtualGood : AbstractVirtualItem{
		public AbstractPriceModel PriceModel;
		public VirtualCategory Category;
		public bool Equiped;
		
		public VirtualGood(string name, string description, AbstractPriceModel priceModel, string itemId, VirtualCategory category, bool equiped)
			: base(name, description, itemId)
		{
			this.PriceModel = priceModel;
			this.Category = category;
			this.Equiped = equiped;
		}
		
#if UNITY_ANDROID
		public VirtualGood(AndroidJavaObject jniVirtualGood) 
			: base(jniVirtualGood)
		{
			this.Equiped = jniVirtualGood.Call<bool>("isEquipped");
			// Virtual Category
			AndroidJavaObject jniVirtualCategory = jniVirtualGood.Call<AndroidJavaObject>("getCategory");
			this.Category = new VirtualCategory(jniVirtualCategory);
			// Price Model
			AndroidJavaObject jniPriceModel = jniVirtualGood.Call<AndroidJavaObject>("getPriceModel");
			this.PriceModel = AbstractPriceModel.CreatePriceModel(jniPriceModel);
		}
		
		public AndroidJavaObject toAndroidJavaObject(AndroidJavaObject jniUnityStoreAssets, AndroidJavaObject jniVirtualCategory) {
			return new AndroidJavaObject("com.soomla.store.domain.data.VirtualGood", this.Name, this.Description, 
				this.PriceModel.toAndroidJavaObject(jniUnityStoreAssets), this.ItemId, jniVirtualCategory, this.Equiped);
		}
#elif UNITY_IOS
		public VirtualGood(JSONObject jsonVg)
			: base(jsonVg)
		{
			this.Equiped = ((JSONObject)jsonVg[JSONConsts.GOOD_EQUIPPED]).b;
			this.PriceModel = AbstractPriceModel.CreatePriceModel((JSONObject)jsonVg[JSONConsts.GOOD_PRICE_MODEL]);
			int categoryId = System.Convert.ToInt32(((JSONObject)jsonVg[JSONConsts.GOOD_CATEGORY_ID]).n);
			try {
				if (categoryId > -1) {
					this.Category = StoreInfo.GetVirtualCategoryById(categoryId);
				}
			} catch (VirtualItemNotFoundException e) {
				Debug.Log("Couldn't find category with id: " + categoryId);
			}
		}
		
		public override JSONObject toJSONObject() {
			JSONObject obj = base.toJSONObject();
			obj.AddField(JSONConsts.GOOD_EQUIPPED, this.Equiped);
			obj.AddField(JSONConsts.GOOD_PRICE_MODEL, this.PriceModel.toJSONObject());
			obj.AddField(JSONConsts.GOOD_CATEGORY_ID, this.Category.Id);
			
			return obj;
		}
#endif
	}
}
