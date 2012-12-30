#import <Foundation/Foundation.h>
#import "IStoreAsssets.h"
#import "VirtualCategory.h"
#import "VirtualCurrency.h"
#import "VirtualGood.h"
#import "VirtualCurrencyPack.h"
#import "StaticPriceModel.h"

@interface UnityStoreAssets : NSObject <IStoreAsssets>{
	NSMutableArray* virtualCurrenciesArray;
	NSMutableArray* virtualGoodsArray;
	NSMutableArray* virtualCurrencyPacksArray;
	NSMutableArray* virtualCategoriesArray;
	NSMutableArray* nonConsumablesArray;
}

@end