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
		public string Name;
		public int Id;

		public VirtualCategory(string name, int id){
			this.Name = name;
			this.Id = id;
		}
		
#if UNITY_ANDROID
		public VirtualCategory(AndroidJavaObject jniVirtualCategory) {
			this.Name = jniVirtualCategory.Call<string>("getName");
			this.Id = jniVirtualCategory.Call<int>("getId");
		}
		
		public AndroidJavaObject toAndroidJavaObject() {
			return new AndroidJavaObject("com.soomla.store.domain.data.VirtualCategory", this.Name,this.Id);
		}
#elif UNITY_IOS
		public VirtualCategory(JSONObject jsonMi) {
			this.Name = jsonMi[JSONConsts.CATEGORY_NAME].str;
			this.Id = System.Convert.ToInt32(((JSONObject)jsonMi[JSONConsts.CATEGORY_ID]).n);
		}
		
		public JSONObject toJSONObject() {
			JSONObject obj = new JSONObject(JSONObject.Type.OBJECT);
			obj.AddField(JSONConsts.CATEGORY_NAME, this.Name);
			obj.AddField(JSONConsts.CATEGORY_ID, this.Id);
			
			return obj;
		}
#endif
	}
}