/*
 * Copyright (C) 2012 Soomla Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

#import <Foundation/Foundation.h>
#import "IStoreAsssets.h"

@class VirtualCategory;
@class VirtualCurrency;
@class VirtualGood;
@class VirtualCurrencyPack;
@class AppStoreItem;

/**
 * This class holds the store's meta data including:
 * - Virtual Currencies definitions
 * - Virtual Currency Packs definitions
 * - Virtual Goods definitions
 * - Virtual Categories definitions
 */
@interface StoreInfo : NSObject{
    NSArray* virtualCurrencies;
    NSArray* virtualGoods;
    NSArray* virtualCurrencyPacks;
    NSArray* appStoreNonConsumableItems;
    NSArray* virtualCategories;
}

@property (nonatomic, retain) NSArray* virtualCurrencies;
@property (nonatomic, retain) NSArray* virtualGoods;
@property (nonatomic, retain) NSArray* virtualCurrencyPacks;
@property (nonatomic, retain) NSArray* appStoreNonConsumableItems;
@property (nonatomic, retain) NSArray* virtualCategories;

+ (StoreInfo*)getInstance;

/**
 * This function initializes StoreInfo. On first initialization, when the
 * database doesn't have any previous version of the store metadata, StoreInfo
 * is being loaded from the given IStoreAssets. After the first initialization,
 * StoreInfo will be initialized from the database.
 * NOTE: If you want to override the current StoreInfo, you'll have to bump the
 * database version (the old database will be destroyed).
 */
- (void)initializeWithIStoreAsssets:(id <IStoreAsssets>)storeAssets;
- (BOOL)initializeFromDB;
- (NSDictionary*)toDictionary;

/**
 * Use this function if you need to know the definition of a specific virtual category.
 * id is the requested category's id.
 * throws VirtualItemNotFoundException
 */
- (VirtualCategory*)categoryWithId:(int)Id;
/**
 * Use this function if you need to know the definition of a specific virtual good.
 * itemId is the requested good's item id.
 * throws VirtualItemNotFoundException
 */
- (VirtualGood*)goodWithItemId:(NSString*)itemId;
/**
 * Use this function if you need to know the definition of a specific virtual currency.
 * itemId is the requested currency's item id.
 * throws VirtualItemNotFoundException
 */
- (VirtualCurrency*)currencyWithItemId:(NSString*)itemId;
/**
 * Use this function if you need to know the definition of a specific virtual currency pack.
 * productId is the requested pack's product id.
 * throws VirtualItemNotFoundException
 */
- (VirtualCurrencyPack*)currencyPackWithProductId:(NSString*)productId;
/**
 * Use this function if you need to know the definition of a specific virtual currency pack.
 * itemId is the requested currency pack's item id.
 * throws VirtualItemNotFoundException
 */
- (VirtualCurrencyPack*)currencyPackWithItemId:(NSString*)itemId;
/**
 * Use this function if you need to know the definition of a specific App Store NON-CONSUMABLE item.
 * productId is the requested NON-CONSUMABLE item's product id.
 * throws VirtualItemNotFoundException
 */
- (AppStoreItem*)appStoreNonConsumableItemWithProductId:(NSString*)productId;

@end
