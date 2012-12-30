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


#import <UIKit/UIKit.h>

extern NSString * EVENT_APPSTORE_PURCHASED;
extern NSString * EVENT_VIRTUAL_GOOD_PURCHASED;
extern NSString * EVENT_VIRTUAL_GOOD_EQUIPPED;
extern NSString * EVENT_VIRTUAL_GOOD_UNEQUIPPED;
extern NSString * EVENT_BILLING_SUPPORTED;
extern NSString * EVENT_BILLING_NOT_SUPPORTED;
extern NSString * EVENT_MARKET_PURCHASE_STARTED;
extern NSString * EVENT_GOODS_PURCHASE_STARTED;
extern NSString * EVENT_CLOSING_STORE;
extern NSString * EVENT_OPENING_STORE;
extern NSString * EVENT_UNEXPECTED_ERROR_IN_STORE;
extern NSString * EVENT_TRANSACTION_RESTORED;

@class AppStoreItem;
@class VirtualGood;

/**
 * This class is used register and post all the supported events.
 * Use this class to invoke events on handlers when they occur.
 *
 * We use iOS's NSNotificationCenter to handle events across the SDK.
 */
@interface EventHandling : NSObject

+ (void)observeAllEventsWithObserver:(id)observer withSelector:(SEL)selector;

+ (void)postAppStorePurchase:(AppStoreItem*)appStoreItem;
+ (void)postVirtualGoodPurchased:(VirtualGood*)good;
+ (void)postVirtualGoodEquipped:(VirtualGood*)good;
+ (void)postVirtualGoodUnEquipped:(VirtualGood*)good;
+ (void)postBillingSupported;
+ (void)postBillingNotSupported;
+ (void)postGoodsPurchaseStarted;
+ (void)postMarketPurchaseStarted:(AppStoreItem*)appStoreItem;
+ (void)postClosingStore;
+ (void)postOpeningStore;
+ (void)postUnexpectedError;
+ (void)postTransactionRestored:(NSString*)productId;

@end
