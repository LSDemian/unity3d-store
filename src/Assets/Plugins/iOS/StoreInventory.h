//
//  StoreInventory.h
//  SoomlaiOSStore
//
//  Created by Refael Dakar on 10/27/12.
//  Copyright (c) 2012 SOOMLA. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface StoreInventory : NSObject

/** Virtual Currencies **/
+ (int)getCurrencyBalance:(NSString*)currencyItemId;
+ (int)addAmount:(int)amount toCurrency:(NSString*)currencyItemId;
+ (int)removeAmount:(int)amount fromCurrency:(NSString*)currencyItemId;

/** Virtual Goods **/
+ (int)getGoodBalance:(NSString*)goodItemId;
+ (int)addAmount:(int)amount toGood:(NSString*)goodItemId;
+ (int)removeAmount:(int)amount fromGood:(NSString*)goodItemId;

@end
