using UnityEngine;

namespace com.soomla.unity
{
	/// <summary>
	/// This class is the parent of all virtual items in the application.
	/// </summary>
	public abstract class AbstractVirtualItem
	{
		public string Name;
		public string Description;
		public string ItemId;
		
		protected AbstractVirtualItem (string name, string description, string itemId)
		{
			this.Name = name;
			this.Description = description;
			this.ItemId = itemId;
		}
		
#if UNITY_ANDROID
		protected AbstractVirtualItem(AndroidJavaObject jniVirtualItem) {
			this.Name = jniVirtualItem.Call<string>("getName");
			this.Description = jniVirtualItem.Call<string>("getDescription");
			this.ItemId = jniVirtualItem.Call<string>("getItemId");
		}
#elif UNITY_IOS
		protected AbstractVirtualItem(JSONObject jsonItem) {
			this.Name = jsonItem[JSONConsts.ITEM_NAME].str;
			this.Description = jsonItem[JSONConsts.ITEM_DESCRIPTION].str;
			this.ItemId = jsonItem[JSONConsts.ITEM_ITEMID].str;
		}
		
		public virtual JSONObject toJSONObject() {
			JSONObject obj = new JSONObject(JSONObject.Type.OBJECT);
			obj.AddField(JSONConsts.ITEM_NAME, this.Name);
			obj.AddField(JSONConsts.ITEM_DESCRIPTION, this.Description);
			obj.AddField(JSONConsts.ITEM_ITEMID, this.ItemId);
			
			return obj;
		}
#endif
	}
}

