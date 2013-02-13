#import "UnityStoreAssets.h"
#import "VirtualCategory.h"
#import "VirtualCurrency.h"
#import "VirtualGood.h"
#import "VirtualCurrencyPack.h"
#import "StaticPriceModel.h"
#import "StoreInventory.h"
#import "StoreController.h"
#import "JSONKit.h"
#import "VirtualItemNotFoundException.h"
#import "UnityCommons.h"
#import "StoreInfo.h"
#import "NonConsumableItem.h"

char* AutonomousStringCopy (const char* string)
{
    if (string == NULL)
       return NULL;

    char* res = (char*)malloc(strlen(string) + 1);
    strcpy(res, string);
    return res;
}

extern "C"{
	
	int storeInfo_GetCategoryById(int catId, char** json){
		@try {
			VirtualCategory* vc = [[StoreInfo getInstance] categoryWithId:catId];
			*json = AutonomousStringCopy([[[vc toDictionary] JSONString] UTF8String]);
		}
		
		@catch (VirtualItemNotFoundException* e) {
            NSLog(@"Couldn't find a VirtualCategory with id: %d.", catId);
			return EXCEPTION_ITEM_NOT_FOUND;
        }

		return NO_ERR;
	}
	
	int storeInfo_GetGoodByItemId(const char* itemId, char** json){
        NSString* itemIdS = [NSString stringWithUTF8String:itemId];
		@try {
			VirtualGood* vg = [[StoreInfo getInstance] goodWithItemId:itemIdS];
			*json = AutonomousStringCopy([[[vg toDictionary] JSONString] UTF8String]);
		}
		
		@catch (VirtualItemNotFoundException* e) {
            NSLog(@"Couldn't find a VirtualGood with itemId: %@.", itemIdS);
			return EXCEPTION_ITEM_NOT_FOUND;
        }

		return NO_ERR;
	}
	
	int storeInfo_GetCurrencyByItemId(const char* itemId, char** json){
        NSString* itemIdS = [NSString stringWithUTF8String:itemId];
		@try {
			VirtualCurrency* vc = [[StoreInfo getInstance] currencyWithItemId:itemIdS];
			*json = AutonomousStringCopy([[[vc toDictionary] JSONString] UTF8String]);
		}
		
		@catch (VirtualItemNotFoundException* e) {
            NSLog(@"Couldn't find a VirtualCurrency with itemId: %@.", itemIdS);
			return EXCEPTION_ITEM_NOT_FOUND;
        }

		return NO_ERR;
	}
	
	int storeInfo_GetCurrencyPackByItemId(const char* itemId, char** json){
        NSString* itemIdS = [NSString stringWithUTF8String:itemId];
		@try {
			VirtualCurrencyPack* vcp = [[StoreInfo getInstance] currencyPackWithItemId:itemIdS];
			*json = AutonomousStringCopy([[[vcp toDictionary] JSONString] UTF8String]);
		}
		
		@catch (VirtualItemNotFoundException* e) {
            NSLog(@"Couldn't find a VirtualCurrencyPack with itemId: %@.", itemIdS);
			return EXCEPTION_ITEM_NOT_FOUND;
        }

		return NO_ERR;
	}
	
	int storeInfo_GetCurrencyPackByProductId(const char* productId, char** json){
        NSString* productIdS = [NSString stringWithUTF8String:productId];
		@try {
			VirtualCurrencyPack* vcp = [[StoreInfo getInstance] currencyPackWithProductId:productIdS];
			*json = AutonomousStringCopy([[[vcp toDictionary] JSONString] UTF8String]);
		}
		
		@catch (VirtualItemNotFoundException* e) {
            NSLog(@"Couldn't find a VirtualCurrencyPack with productId: %@.", productIdS);
			return EXCEPTION_ITEM_NOT_FOUND;
        }

		return NO_ERR;
	}
	
	int storeInfo_GetNonConsumableByProductId(const char* productId, char** json){
        NSString* productIdS = [NSString stringWithUTF8String:productId];
		@try {
			NonConsumableItem* non = [[StoreInfo getInstance] nonConsumableItemWithProductId:productIdS];
			*json = AutonomousStringCopy([[[non toDictionary] JSONString] UTF8String]);
		}
		
		@catch (VirtualItemNotFoundException* e) {
            NSLog(@"Couldn't find a NonConsumableItem with productId: %@.", productIdS);
			return EXCEPTION_ITEM_NOT_FOUND;
        }

		return NO_ERR;
	}
	
	int storeInfo_GetVirtualCurrencies(char** json) {
		NSArray* virtualCurrencies = [[StoreInfo getInstance] virtualCurrencies];
		NSMutableString* retJson = [[[NSMutableString alloc] initWithString:@"["] autorelease];
		for(VirtualCurrency* vc in virtualCurrencies) {
			[retJson appendString:[NSString stringWithFormat:@"%@,", [[vc toDictionary] JSONString]]];
		}
		[retJson deleteCharactersInRange:NSMakeRange([retJson length]-1, 1)];
		[retJson appendString:@"]"];
		
		*json = AutonomousStringCopy([retJson UTF8String]);
		
		return NO_ERR;
	}
	
	int storeInfo_GetVirtualGoods(char** json) {
		NSArray* virtualGoods = [[StoreInfo getInstance] virtualGoods];
		NSMutableString* retJson = [[[NSMutableString alloc] initWithString:@"["] autorelease];
		for(VirtualGood* vg in virtualGoods) {
			[retJson appendString:[NSString stringWithFormat:@"%@,", [[vg toDictionary] JSONString]]];
		}
		[retJson deleteCharactersInRange:NSMakeRange([retJson length]-1, 1)];
		[retJson appendString:@"]"];
		
		*json = AutonomousStringCopy([retJson UTF8String]);
		
		return NO_ERR;
	}
	
	int storeInfo_GetVirtualCurrencyPacks(char** json) {
		NSArray* virtualCurrencyPacks = [[StoreInfo getInstance] virtualCurrencyPacks];
		NSMutableString* retJson = [[[NSMutableString alloc] initWithString:@"["] autorelease];
		for(VirtualCurrencyPack* vcp in virtualCurrencyPacks) {
			[retJson appendString:[NSString stringWithFormat:@"%@,", [[vcp toDictionary] JSONString]]];
		}
		[retJson deleteCharactersInRange:NSMakeRange([retJson length]-1, 1)];
		[retJson appendString:@"]"];
		
		*json = AutonomousStringCopy([retJson UTF8String]);
		
		return NO_ERR;
	}
	
	int storeInfo_GetNonConsumableItems(char** json) {
		NSArray* nonConsumables = [[StoreInfo getInstance] nonConsumableItems];
		NSMutableString* retJson = [[[NSMutableString alloc] initWithString:@"["] autorelease];
		for(NonConsumableItem* non in nonConsumables) {
			[retJson appendString:[NSString stringWithFormat:@"%@,", [[non toDictionary] JSONString]]];
		}
		[retJson deleteCharactersInRange:NSMakeRange([retJson length]-1, 1)];
		[retJson appendString:@"]"];
		
		*json = AutonomousStringCopy([retJson UTF8String]);
		
		return NO_ERR;
	}
	
	int storeInfo_GetVirtualCategories(char** json) {
		NSArray* virtualCategories = [[StoreInfo getInstance] virtualCategories];
		NSMutableString* retJson = [[[NSMutableString alloc] initWithString:@"["] autorelease];
		for(VirtualCategory* vc in virtualCategories) {
			[retJson appendString:[NSString stringWithFormat:@"%@,", [[vc toDictionary] JSONString]]];
		}
		[retJson deleteCharactersInRange:NSMakeRange([retJson length]-1, 1)];
		[retJson appendString:@"]"];
		
		*json = AutonomousStringCopy([retJson UTF8String]);
		
		return NO_ERR;
	}
	
}