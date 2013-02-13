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

@class VirtualCurrency;

/**
 * This class provide basic storage operations on VirtualCurrencies.
 */
@interface VirtualCurrencyStorage : NSObject

/**
 * Fetch the balance of the given virtual currency.
 * virtualCurrency is the required virtual currency.
 */
- (int)getBalanceForCurrency:(VirtualCurrency*)virtualCurrency;
/**
 * Adds the given amount of currency to the storage.
 * virtualCurrency is the required virtual currency.
 * amount is the amount of currency to add.
 */
- (int)addAmount:(int)amount toCurrency:(VirtualCurrency*)virtualCurrency;
/**
 * Removes the given amount of currency from the storage.
 * virtualCurrency is the required virtual currency.
 * amount is the amount of currency to remove.
 */
- (int)removeAmount:(int)amount fromCurrency:(VirtualCurrency*)virtualCurrency;

@end
