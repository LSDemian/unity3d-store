using UnityEngine;
using System.Collections;
using System;

namespace com.soomla.unity.example
{
	public class ExampleWindow : MonoBehaviour
	{
		private static ExampleWindow instance = null;
		
		private GUIState guiState = GUIState.WELCOME;
		private Vector2 goodsScrollPosition = Vector2.zero;
		private Vector2 productScrollPosition = Vector2.zero;
		private bool isDragging = false;
		private Vector2 startTouch = Vector2.zero;
		private static IStoreEventHandler handler;
		
		public string fontSuffix = "";
	
		private enum GUIState{
			WELCOME,
			PRODUCTS,
			GOODS
		}
		
		private static bool isVisible = false;
		
		void Awake(){
			if(instance == null){ 	//making sure we only initialize one instance.
				instance = this;
				GameObject.DontDestroyOnLoad(this.gameObject);
			} else {					//Destroying unused instances.
				GameObject.Destroy(this);
			}
			
			//FONT
			if(Mathf.Max(Screen.width, Screen.height) > 640){ //using max to be certain we have the longest side of the screen, even if we are in portrait.
				fontSuffix = "_2X"; //a nice suffix to show the fonts are twice as big as the original
			}
		}
		
		// Use this for initialization
		void Start () {
			StoreController.Initialize(new MuffinRushAssets());
			handler = new ExampleEventHandler();
			
			// some examples
			Debug.Log("start currency: " + StoreInventory.GetCurrencyBalance("currency_muffin"));
			Debug.Log("remove currency: " + StoreInventory.RemoveCurrencyAmount("currency_muffin",50));
			Debug.Log("middle currency: " + StoreInventory.GetCurrencyBalance("currency_muffin"));
			Debug.Log("add currency: " + StoreInventory.AddCurrencyAmount("currency_muffin",4000));
			Debug.Log("end currency:" + StoreInventory.GetCurrencyBalance("currency_muffin"));
		}
		
		public static void StoreOpening() {
			ExampleLocalStoreInfo.Init();
			StoreEventHandlers.AddEventHandler(handler);	
		}
		
		public static void StoreClosing() {
			StoreEventHandlers.RemoveEventHandler(handler);
		}
		
		public static void OpenWindow(){
			instance.guiState = GUIState.WELCOME;
			isVisible = true;
		}
		
		public static void CloseWindow(){
			isVisible = false;
		}
		// Update is called once per frame
		void Update () {
			if(isVisible){
				//code to be able to scroll without the scrollbars.
				if(Input.GetMouseButtonDown(0)){
					startTouch = Input.mousePosition;
				}else if(Input.GetMouseButtonUp(0)){
					isDragging = false;
				}else if(Input.GetMouseButton(0)){
					if(!isDragging){
						if( Mathf.Abs(startTouch.y-Input.mousePosition.y) > 10f){
							isDragging = true;
						}
					}else{
						if(guiState == GUIState.GOODS){
							goodsScrollPosition.y -= startTouch.y - Input.mousePosition.y;
							startTouch = Input.mousePosition;
						}else if(guiState == GUIState.PRODUCTS){
							productScrollPosition.y -= startTouch.y - Input.mousePosition.y;
							startTouch = Input.mousePosition;
						}
					}
				}
			}
		}
		
		void OnGUI(){
			if(!isVisible){
				return;
			}
			
			//GUI.skin.verticalScrollbar.fixedWidth = 0;
			//GUI.skin.verticalScrollbar.fixedHeight = 0;
			//GUI.skin.horizontalScrollbar.fixedWidth = 0;
			//GUI.skin.horizontalScrollbar.fixedHeight = 0;
			GUI.skin.horizontalScrollbar = GUIStyle.none;
			GUI.skin.verticalScrollbar = GUIStyle.none;
			
			//disabling warnings because we use GUIStyle.none which result in warnings
			if(guiState == GUIState.WELCOME){
				welcomeScreen();
			}else if(guiState == GUIState.GOODS){
				goodsScreen();
			}else if(guiState == GUIState.PRODUCTS){
				currencyScreen();
			}	
		}
	
		void welcomeScreen()
		{
			//drawing background, just using a white pixel here
			GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),(Texture2D)Resources.Load("SoomlaStore/images/img_direct"));
			//changing the font and alignment the label, and making a backup so we can put it back.
			Font backupFont = GUI.skin.label.font;
			TextAnchor backupAlignment = GUI.skin.label.alignment;
			GUI.skin.label.font = (Font)Resources.Load("SoomlaStore/GoodDog" + fontSuffix);
			GUI.skin.label.alignment = TextAnchor.MiddleCenter;
			//writing the text.
			GUI.Label(new Rect(Screen.width/8,Screen.height/8f,Screen.width*6f/8f,Screen.height*0.3f),"Soomla Store\nExample");
			//select the small font
			GUI.skin.label.font = (Font)Resources.Load("SoomlaStore/GoodDog_small" + fontSuffix);
			GUI.Label(new Rect(Screen.width/8,Screen.height*7f/8f,Screen.width*6f/8f,Screen.height/8f),"Press the SOOMLA-bot to open store");
			//set font back to original
			GUI.skin.label.font = backupFont;
			GUI.Label(new Rect(Screen.width*0.25f,Screen.height/2-50,Screen.width*0.5f,100),"[ Your game here ]");
			//drawing button and testing if it has been clicked
			if(GUI.Button(new Rect(Screen.width*2/6,Screen.height*5f/8f,Screen.width*2/6,Screen.width*2/6),(Texture2D)Resources.Load("SoomlaStore/images/soomla_logo_new"))){
				guiState = GUIState.GOODS;
				StoreController.StoreOpening();
				StoreOpening();
			}
			//set alignment to backup
			GUI.skin.label.alignment = backupAlignment;
		}
	
		void goodsScreen()
		{
			//white background
			GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),(Texture2D)Resources.Load("SoomlaStore/images/white_pixel"));
			Color backupColor = GUI.color;
			TextAnchor backupAlignment = GUI.skin.label.alignment;
			Font backupFont = GUI.skin.label.font;
			
			GUI.color = Color.red;
			GUI.skin.label.alignment = TextAnchor.UpperLeft;
			GUI.Label(new Rect(10,10,Screen.width-10,Screen.height-10),"SOOMLA Example Store");
			GUI.color = Color.black;
			GUI.skin.label.alignment = TextAnchor.UpperRight;
			GUI.Label(new Rect(10,10,Screen.width-40,Screen.height),""+ ExampleLocalStoreInfo.CurrencyBalance);
			GUI.skin.label.alignment = TextAnchor.MiddleCenter;
			GUI.skin.label.font = (Font)Resources.Load("SoomlaStore/Title" + fontSuffix);
			GUI.Label(new Rect(0,Screen.height/8f,Screen.width,Screen.height/8f),"Virtual Goods");
			
			GUI.color = backupColor;
			GUI.DrawTexture(new Rect(Screen.width-30,10,30,30),(Texture2D)Resources.Load("SoomlaStore/images/Muffins"));
			float productSize = Screen.width*0.30f;
			float totalHeight = ExampleLocalStoreInfo.VirtualGoods.Count*productSize;
			//Here we start a scrollView, the first rectangle is the position of the scrollView on the screen,
			//the second rectangle is the size of the panel inside the scrollView.
			//All rectangles after this point are relative to the position of the scrollView.
			goodsScrollPosition = GUI.BeginScrollView(new Rect(0,Screen.height*2f/8f,Screen.width,Screen.height*5f/8f),goodsScrollPosition,new Rect(0,0,Screen.width,totalHeight));
			float y = 0;
			foreach(VirtualGood vg in ExampleLocalStoreInfo.VirtualGoods){
				GUI.color = backupColor;
				if(GUI.Button(new Rect(0,y,Screen.width,productSize),"") && !isDragging){
					Debug.Log("SOOMLA/UNITY wants to buy: " + vg.Name);
					try {
						StoreController.BuyVirtualGood(vg.ItemId);
					} catch (Exception e) {
						Debug.Log ("SOOMLA/UNITY " + e.Message);
					}
				}
				GUI.DrawTexture(new Rect(0,y,Screen.width,productSize),(Texture2D)Resources.Load("SoomlaStore/images/white_pixel"));
				//We draw a button so we can detect a touch and then draw an image on top of it.
				//TODO
				//Resources.Load(path) The path is the relative path starting from the Resources folder.
				//Make sure the images used for UI, have the textureType GUI. You can change this in the Unity editor.
				GUI.color = backupColor;
				GUI.DrawTexture(new Rect(0+productSize/8f, y+productSize/8f,productSize*6f/8f,productSize*6f/8f),(Texture2D)Resources.Load("SoomlaStore/images/" + vg.Name));
				GUI.color = Color.black;
				GUI.skin.label.font = (Font)Resources.Load("SoomlaStore/Name" + fontSuffix);
				GUI.skin.label.alignment = TextAnchor.UpperLeft;
				GUI.Label(new Rect(productSize,y,Screen.width,productSize/3f),vg.Name);
				GUI.skin.label.font = (Font)Resources.Load("SoomlaStore/Description" + fontSuffix);
				GUI.Label(new Rect(productSize + 10f,y+productSize/3f,Screen.width-productSize-15f,productSize/3f),vg.Description);
				GUI.Label(new Rect(Screen.width/2f,y+productSize*2/3f,Screen.width,productSize/3f),"price:" + vg.PriceModel.GetCurrenctPrice(vg)[ExampleLocalStoreInfo.VirtualCurrencies[0].ItemId]);
				GUI.Label(new Rect(Screen.width*3/4f,y+productSize*2/3f,Screen.width,productSize/3f), "Balance:" + ExampleLocalStoreInfo.GoodsBalances[vg.ItemId]);
				GUI.skin.label.alignment = TextAnchor.UpperRight;
				GUI.skin.label.font = (Font)Resources.Load("SoomlaStore/Buy" + fontSuffix);
				GUI.Label(new Rect(0,y,Screen.width-10,productSize),"Click to buy");
				GUI.color = Color.grey;
				GUI.DrawTexture(new Rect(0,y+productSize-1,Screen.width,1),(Texture2D)Resources.Load("SoomlaStore/images/white_pixel"));
				y+= productSize;
			}
			GUI.EndScrollView();
			//We have just ended the scroll view this means that all the positions are relative top-left corner again.
			GUI.skin.label.alignment = backupAlignment;
			GUI.color = backupColor;
			GUI.skin.label.font = backupFont;
			
			float height = Screen.height/8f;
			float borderSize = height/8f;
			float buttonHeight = height-2*borderSize;
			float width = buttonHeight*180/95;
			if(GUI.Button(new Rect(Screen.width*2f/7f-width/2f,Screen.height*7f/8f+borderSize,width,buttonHeight), "back")){
				guiState = GUIState.WELCOME;
				StoreController.StoreClosing();
				StoreClosing();
			}
			GUI.DrawTexture(new Rect(Screen.width*2f/7f-width/2f,Screen.height*7f/8f+borderSize,width,buttonHeight),(Texture2D)Resources.Load("SoomlaStore/images/back"));
			width = buttonHeight*227/94;
			if(GUI.Button(new Rect(Screen.width*5f/7f-width/2f,Screen.height*7f/8f+borderSize,width,buttonHeight), "back")){
				guiState = GUIState.PRODUCTS;
			}
			GUI.DrawTexture(new Rect(Screen.width*5f/7f-width/2f,Screen.height*7f/8f+borderSize,width,buttonHeight),(Texture2D)Resources.Load("SoomlaStore/images/GetMore"));
		}
	
		void currencyScreen()
		{
			//white background
			GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),(Texture2D)Resources.Load("SoomlaStore/images/white_pixel"));
			Color backupColor = GUI.color;
			TextAnchor backupAlignment = GUI.skin.label.alignment;
			Font backupFont = GUI.skin.label.font;
			
			GUI.color = Color.red;
			GUI.skin.label.alignment = TextAnchor.UpperLeft;
			GUI.Label(new Rect(10,10,Screen.width-10,Screen.height-10),"SOOMLA Example Store");
			GUI.color = Color.black;
			GUI.skin.label.alignment = TextAnchor.UpperRight;
			GUI.Label(new Rect(10,10,Screen.width-40,Screen.height),""+ExampleLocalStoreInfo.CurrencyBalance);
			GUI.skin.label.alignment = TextAnchor.MiddleCenter;
			GUI.skin.label.font = (Font)Resources.Load("SoomlaStore/Title" + fontSuffix);
			GUI.Label(new Rect(0,Screen.height/8f,Screen.width,Screen.height/8f),"Virtual Currency Packs");
			
			GUI.color = backupColor;
			GUI.DrawTexture(new Rect(Screen.width-30,10,30,30),(Texture2D)Resources.Load("SoomlaStore/images/" + ExampleLocalStoreInfo.VirtualCurrencies[0].Name));
			float productSize = Screen.width*0.30f;
			float totalHeight = ExampleLocalStoreInfo.VirtualGoods.Count*productSize;
			//Here we start a scrollView, the first rectangle is the position of the scrollView on the screen,
			//the second rectangle is the size of the panel inside the scrollView.
			//All rectangles after this point are relative to the position of the scrollView.
			productScrollPosition = GUI.BeginScrollView(new Rect(0,Screen.height*2f/8f,Screen.width,Screen.height*5f/8f),productScrollPosition,new Rect(0,0,Screen.width,totalHeight));
			float y = 0;
			foreach(VirtualCurrencyPack cp in ExampleLocalStoreInfo.VirtualCurrencyPacks){
				GUI.color = backupColor;
				//We draw a button so we can detect a touch and then draw an image on top of it.
				if(GUI.Button(new Rect(0,y,Screen.width,productSize),"") && !isDragging){
					Debug.Log("SOOMLA/UNITY Wants to buy: " + cp.Name + " productId: " + cp.MarketItem.ProductId);
					try {
						StoreController.BuyCurrencyPack(cp.MarketItem.ProductId);
					} catch (Exception e) {
						Debug.Log ("SOOMLA/UNITY " + e.Message);
					}
				}
				GUI.DrawTexture(new Rect(0,y,Screen.width,productSize),(Texture2D)Resources.Load("SoomlaStore/images/white_pixel"));
				//Resources.Load(path) The path is the relative path starting from the Resources folder.
				//Make sure the images used for UI, have the textureType GUI. You can change this in the Unity editor.
				GUI.DrawTexture(new Rect(0+productSize/8f, y+productSize/8f,productSize*6f/8f,productSize*6f/8f),(Texture2D)Resources.Load("SoomlaStore/images/" + cp.Name));
				GUI.color = Color.black;
				GUI.skin.label.font = (Font)Resources.Load("SoomlaStore/Name" + fontSuffix);
				GUI.skin.label.alignment = TextAnchor.UpperLeft;
				GUI.Label(new Rect(productSize,y,Screen.width,productSize/3f),cp.Name);
				GUI.skin.label.font = (Font)Resources.Load("SoomlaStore/Description" + fontSuffix);
				GUI.Label(new Rect(productSize + 10f,y+productSize/3f,Screen.width-productSize-15f,productSize/3f),cp.Description);
				GUI.Label(new Rect(Screen.width*3/4f,y+productSize*2/3f,Screen.width,productSize/3f),"price:" + cp.Price);
				GUI.skin.label.alignment = TextAnchor.UpperRight;
				GUI.skin.label.font = (Font)Resources.Load("SoomlaStore/Buy" + fontSuffix);
				GUI.Label(new Rect(0,y,Screen.width-10,productSize),"Click to buy");
				GUI.color = Color.grey;
				GUI.DrawTexture(new Rect(0,y+productSize-1,Screen.width,1),(Texture2D)Resources.Load("SoomlaStore/images/white_pixel"));
				y+= productSize;
			}
			GUI.EndScrollView();
			//We have just ended the scroll view this means that all the positions are relative top-left corner again.
			GUI.skin.label.alignment = backupAlignment;
			GUI.color = backupColor;
			GUI.skin.label.font = backupFont;
			
			float height = Screen.height/8f;
			float borderSize = height/8f;
			float buttonHeight = height-2*borderSize;
			float width = buttonHeight*180/95;
			if(GUI.Button(new Rect(Screen.width/2f-width/2f,Screen.height*7f/8f+borderSize,width,buttonHeight), "back")){
				guiState = GUIState.GOODS;
			}
			GUI.DrawTexture(new Rect(Screen.width/2f-width/2f,Screen.height*7f/8f+borderSize,width,buttonHeight),(Texture2D)Resources.Load("SoomlaStore/images/back"));
		}
	
	}
}

