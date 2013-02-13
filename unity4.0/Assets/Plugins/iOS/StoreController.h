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
#import <StoreKit/StoreKit.h>
#import "IStoreAsssets.h"

/**
 * This class is where all the important stuff happens. You can use it to purchase products from Google Play,
 * buy virtual goods, and get notifications on whatever happens.
 *
 * This is the only class you need to initialize in order to use the SOOMLA SDK. If you use the UI,
 * you'll need to also initialize StorefrontActivity.
 *
 * In addition to initializing this class, you'll also have to call storeOpening and
 * storeClosing when you open the store window or close it.
 * IMPORTANT: if you use the SOOMLA storefront (SOOMLA Storefront), than DON'T call these 2 functions. SOOMLA
 * storefront takes care of it for you.
 */
@interface StoreController : NSObject <SKProductsRequestDelegate, SKPaymentTransactionObserver>{
    SKProduct *proUpgradeProduct;
    SKProductsRequest *productsRequest;
}

+ (StoreController*)getInstance;

- (void)initializeWithStoreAssets:(id<IStoreAsssets>)storeAssets andCustomSecret:(NSString*)secret;
/**
 * Start an app-store item (CurrencyPack or NonConsumableItem) purchase process
 * productId is the product id of the required currency pack.
 */
- (void)buyAppStoreItemWithProcuctId:(NSString*)productId;
/**
 * Start a virtual goods purchase process.
 * itemId is the item id of the required virtual good.
 * throws InsufficientFundsException
 * throws VirtualItemNotFoundException
 */
- (void)buyVirtualGood:(NSString*)itemId;
/**
 * Call this function when you open the actual store window
 */
- (void)storeOpening;
/**
 * Call this function when you close the actual store window.
 */
- (void)storeClosing;
/**
* Make a VirtualGood equipped by the user.
* itemId is the item id of the required virtual good.
* throws NotEnoughGoodsException
* throws VirtualItemNotFoundException
*/
- (void) equipVirtualGood:(NSString*) itemId;
/**
* Make a VirtualGood unequipped by the user.
* itemId is the item id of the required virtual good.
* throws VirtualItemNotFoundException
*/
- (void) unequipVirtualGood:(NSString*) itemId;

@end
