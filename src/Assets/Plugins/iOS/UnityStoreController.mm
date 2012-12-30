#import "UnityStoreAssets.h"
#import "UnityStoreEventDispatcher.h"
#import "VirtualCategory.h"
#import "VirtualCurrency.h"
#import "VirtualGood.h"
#import "AppStoreItem.h"
#import "VirtualCurrencyPack.h"
#import "StoreController.h"
#import "JSONKit.h"
#import "VirtualItemNotFoundException.h"
#import "NotEnoughGoodsException.h"
#import "InsufficientFundsException.h"
#import "UnityCommons.h"
#import "StoreConfig.h"

UnityStoreAssets * storeAssets;
UnityStoreEventDispatcher * storeEventDispatcher;
extern UIViewController* UnityGetGLViewController();

extern "C"{
	    
	void storeAssets_Init(){
		storeAssets = [[UnityStoreAssets alloc] init];
	}
	
	void storeAssets_AddCategory(const char* name, int id){
		NSMutableArray * virtualCategories = (NSMutableArray*)[storeAssets virtualCategories];
		[virtualCategories addObject:[[VirtualCategory alloc] initWithName:[NSString stringWithUTF8String:name] andId:id]];
	}
	
	void storeAssets_AddCurrency(const char* name, const char* description, const char* itemID){
		NSMutableArray * virtualCurrencies = (NSMutableArray*)[storeAssets virtualCurrencies];
		[virtualCurrencies addObject:[[VirtualCurrency alloc] initWithName:[NSString stringWithUTF8String:name] andDescription:[NSString stringWithUTF8String:description] andItemId:[NSString stringWithUTF8String:itemID]]];
	}
	
	void storeAssets_AddCurrencyPack(const char* name, const char* description, const char* itemID, double price, const char* productID, int currencyAmount, const char* currencyItemId){
		NSMutableArray * virtualCurrencies = (NSMutableArray*)[storeAssets virtualCurrencies];
		VirtualCurrency * virtualCurrency = NULL;
		NSString* currencyItemIdS = [NSString stringWithUTF8String:currencyItemId];
		for(VirtualCurrency* vc in virtualCurrencies) {
			if ([vc.itemId isEqualToString:currencyItemIdS]) {
				virtualCurrency = vc;
				break;
			}
		}
		NSMutableArray * virtualCurrencyPacks = (NSMutableArray*)[storeAssets virtualCurrencyPacks];
		[virtualCurrencyPacks addObject:[[VirtualCurrencyPack alloc] initWithName:[NSString stringWithUTF8String:name] andDescription:[NSString stringWithUTF8String:description] andItemId:[NSString stringWithUTF8String:itemID] andPrice:price andProductId:[NSString stringWithUTF8String:productID] andCurrencyAmount:currencyAmount andCurrency:virtualCurrency]];
	}
	
	void storeAssets_AddVirtualGood(const char* name, const char* description, const char* itemID,int categoryIndex, bool equipStatus, const char* priceModelJSON){
		NSDictionary* dict = [[NSString stringWithUTF8String:priceModelJSON] objectFromJSONString];
		PriceModel * priceModel = [PriceModel priceModelWithNSDictionary:dict];
		NSMutableArray * virtualCategories = (NSMutableArray*)[storeAssets virtualCategories];
		VirtualCategory * virtualCategory = NULL;
		for(VirtualCategory* vc in virtualCategories) {
			if (vc.Id == categoryIndex) {
				virtualCategory = vc;
				break;
			}
		}
		NSMutableArray * virtualGoods = (NSMutableArray*)[storeAssets virtualGoods];
		[virtualGoods addObject:[[VirtualGood alloc] initWithName:[NSString stringWithUTF8String:name] andDescription:[NSString stringWithUTF8String:description] andItemId:[NSString stringWithUTF8String:itemID] andPriceModel:priceModel andCategory:virtualCategory andEquipStatus:equipStatus]];
	}
	
	void storeAssets_AddNonConsumable(const char* productId) {
		NSMutableArray * appStoreNonConsumableItems = (NSMutableArray*)[storeAssets appStoreNonConsumableItems];
		[appStoreNonConsumableItems addObject:[[AppStoreItem alloc] initWithProductId:[NSString stringWithUTF8String:productId] andConsumable:kNonConsumable]];
	}
    
    void storeController_SetSoomSec(const char* soomSec) {
        if (SOOM_SEC) {
            [SOOM_SEC release];
        }
        SOOM_SEC = [[NSString stringWithUTF8String:soomSec] retain];
    }
	
	void storeController_Init(const char* secret){
		[[StoreController getInstance] initializeWithStoreAssets:storeAssets andCustomSecret:[NSString stringWithUTF8String:secret]];
        storeEventDispatcher = [[UnityStoreEventDispatcher alloc] init];
	}
	
	int storeController_BuyCurrencyPack(const char* productId) {
		@try {
			[[StoreController getInstance] buyCurrencyPackWithProcuctId:[NSString stringWithUTF8String:productId]];
		} 
		
        @catch (VirtualItemNotFoundException *e) {
            NSLog(@"Couldn't find a VirtualCurrencyPack with productId: %@. Purchase is cancelled.", [NSString stringWithUTF8String:productId]);
			return EXCEPTION_ITEM_NOT_FOUND;
        }
		
		return NO_ERR;
	}
	
	int storeController_BuyVirtualGood(const char* itemId) {
		@try {
			[[StoreController getInstance] buyVirtualGood:[NSString stringWithUTF8String:itemId]];
		}
		
        @catch (InsufficientFundsException *e) {
            NSLog(@"%@", e.reason);
            return EXCEPTION_INSUFFICIENT_FUNDS;
        }
		
        @catch (VirtualItemNotFoundException *e) {
            NSLog(@"Couldn't find a VirtualCurrencyPack with itemId: %@. Purchase is cancelled.", [NSString stringWithUTF8String:itemId]);
			return EXCEPTION_ITEM_NOT_FOUND;
        }
		
		return NO_ERR;
	}
	
	int storeController_BuyNonConsumableItem(const char* productId) {
		@try {
			[[StoreController getInstance] buyNonConsumableItem:[NSString stringWithUTF8String:productId]];
		} 
		
        @catch (VirtualItemNotFoundException *e) {
            NSLog(@"Couldn't find a AppStoreItem with productId: %@. Purchase is cancelled.", [NSString stringWithUTF8String:productId]);
			return EXCEPTION_ITEM_NOT_FOUND;
        }
		
		return NO_ERR;
	}
	
	int storeController_EquipVirtualGood(const char* itemId) {
        NSString* itemIdS = [NSString stringWithUTF8String:itemId];
        @try {
            [[StoreController getInstance] equipVirtualGood:itemIdS];
        }
        
        @catch (NotEnoughGoodsException *e) {
            NSLog(@"%@", e.reason);
            return EXCEPTION_NOT_ENOUGH_GOODS;
        }
            
        @catch (VirtualItemNotFoundException *e) {
            NSLog(@"Couldn't find a VirtualGood with itemId: %@. Equipping is cancelled.", itemIdS);
			return EXCEPTION_ITEM_NOT_FOUND;
        }

		return NO_ERR;
	}
	
	int storeController_UnEquipVirtualGood(const char* itemId) {
        NSString* itemIdS = [NSString stringWithUTF8String:itemId];
        @try {
            [[StoreController getInstance] unequipVirtualGood:itemIdS];
        }
            
        @catch (VirtualItemNotFoundException *e) {
            NSLog(@"Couldn't find a VirtualGood with itemId: %@. UnEquipping is cancelled.", itemIdS);
			return EXCEPTION_ITEM_NOT_FOUND;
        }

		return NO_ERR;
	}
	
	void storeController_StoreOpening() {
		[[StoreController getInstance] storeOpening];
	}
	
	void storeController_StoreClosing() {
		[[StoreController getInstance] storeClosing];
	}
	
}