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
#import <sqlite3.h>

#define DICT_KEY_BALANCE @"balance"
#define DICT_KEY_EQUIP   @"equip"
#define DICT_KEY_ITEM_ID @"itemId"
#define DICT_KEY_PRODUCT_ID @"productId"

/**
 * The StoreDatabase provides basic SQLite database io functions for specific needs around the SDK.
 */
@interface StoreDatabase : NSObject{
    @private
    sqlite3 *database;
}

- (id)init;

/**
 * Fetch a single App Store NON-CONSUMABLE information with the given productId.
 * productId is the required item's product id.
 */
- (BOOL)getAppStoreNonConsumableExists:(NSString*)productId;
/**
 * Sets the status of the App Store NON-CONSUMABLE item with the given purchased boolean.
 * productId is the App Store NON-CONSUMABLE item.
 * purchased is the status of the App Store NON-CONSUMABLE item.
 */
- (void)setAppStoreNonConsumable:(NSString*)productId purchased:(BOOL)purchased;
/**
 * Updates the balance of the virtual currency with the given itemId.
 * itemId is the item id of the required virtual currency.
 * balance is the required virtual currency's new balance.
 */
- (void)updateCurrencyBalance:(NSString*)balance forItemId:(NSString*)itemId;
/**
 * Updates the balance of the virtual good with the given itemId.
 * itemId is the item id of the required virtual good.
 * balance is the required virtual good's new balance.
 */
- (void)updateGoodBalance:(NSString*)balance forItemId:(NSString*)itemId;
/**
 * Updates the equipe status of the virtual good with the given itemId.
 * itemId is the item id of the required virtual good.
 * equip is the required virtual good's new equip status.
 */
- (void)updateGoodEquipped:(NSString*)equip forItemId:(NSString*)itemId;
/**
 * Fetch a single virtual currency information with the given itemId.
 * itemId is the required currency's item id.
 */
- (NSDictionary*)getCurrencyWithItemId:(NSString*)itemId;
/**
 * Fetch a single virtual good information with the given itemId.
 * itemId is the required good's item id.
 */
- (NSDictionary*)getGoodWithItemId:(NSString*)itemId;
/**
 * Overwrites the current storeinfo information with a new one.
 * storeinfo is the new store information.
 */
- (void)setStoreInfo:(NSString*)storeInfoData;
/**
 * Overwrites the current storefrontinfo information with a new one.
 * storefrontinfo is the new storefront information.
 */
- (void)setStorefrontInfo:(NSString*)storefrontInfoData;
/**
 * Fetch the current storeInfo information with a new one.
 */
- (NSString*)getStoreInfo;
/**
 * Fetch the current storefrontInfo information with a new one.
 */
- (NSString*)getStorefrontInfo;


@end
