using UnityEngine;
using System.Collections;

namespace com.soomla.unity{
	/// <summary>
	/// This class is a definition of a category. A single category can be associated with many virtual goods.
	/// The purposes of virtual category are:
	/// 1. You can use it to arrange virtual goods to their specific categories.
	/// 2. SOOMLA's storefront uses this to show the goods in their categories on the UI (for supported themes only).
	/// </summary>
	public class VirtualCategory {
		
		public sealed class EquippingModel {

    		private readonly string name;
    		private readonly int value;

		    public static readonly EquippingModel NONE = new EquippingModel (1, "none");
		    public static readonly EquippingModel SINGLE = new EquippingModel (2, "single");
		    public static readonly EquippingModel MULTIPLE = new EquippingModel (3, "multiple");        
		
		    private EquippingModel(int value, string name){
		        this.name = name;
		        this.value = value;
		    }
		
		    public override string ToString(){
		        return name;
		    }
			
			public int toInt() {
				return value;
			}
		
		}
		
		public string Name;
		public int Id;
		public EquippingModel Equipping;

		public VirtualCategory(string name, int id, EquippingModel equipping){
			this.Name = name;
			this.Id = id;
			this.Equipping = equipping;
		}
		
#if UNITY_ANDROID
		public VirtualCategory(AndroidJavaObject jniVirtualCategory) {
			this.Name = jniVirtualCategory.Call<string>("getName");
			this.Id = jniVirtualCategory.Call<int>("getId");
			int emOrdinal = jniVirtualCategory.Call<AndroidJavaObject>("getEquippingModel").Call<int>("ordinal");
			switch(emOrdinal){
				case 0:
					this.Equipping = EquippingModel.NONE;
					break;
				case 1:
					this.Equipping = EquippingModel.SINGLE;
					break;
				default:
					this.Equipping = EquippingModel.MULTIPLE;
					break;
			}
		}
		
		public AndroidJavaObject toAndroidJavaObject(AndroidJavaObject jniUnityStoreAssets) {
			return jniUnityStoreAssets.CallStatic<AndroidJavaObject>("createVirtualCategory"
					, this.Name
					, this.Id
					, this.Equipping.toInt());
		}
#elif UNITY_IOS
		public VirtualCategory(JSONObject jsonMi) {
			this.Name = jsonMi[JSONConsts.CATEGORY_NAME].str;
			this.Id = System.Convert.ToInt32(((JSONObject)jsonMi[JSONConsts.CATEGORY_ID]).n);
			int emOrdinal = System.Convert.ToInt32(((JSONObject)jsonMi[JSONConsts.CATEGORY_EQUIPPING]).n);
			this.Equipping = EquippingModel.MULTIPLE;
			switch(emOrdinal){
				case 0:
					this.Equipping = EquippingModel.NONE;
					break;
				case 1:
					this.Equipping = EquippingModel.SINGLE;
					break;
				default:
					this.Equipping = EquippingModel.MULTIPLE;
					break;
			}
		}
		
		public JSONObject toJSONObject() {
			JSONObject obj = new JSONObject(JSONObject.Type.OBJECT);
			obj.AddField(JSONConsts.CATEGORY_NAME, this.Name);
			obj.AddField(JSONConsts.CATEGORY_ID, this.Id);
			obj.AddField(JSONConsts.CATEGORY_EQUIPPING, this.Equipping.toInt());
			
			return obj;
		}
#endif
	}
}