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

#import "VirtualItem.h"

@class VirtualCurrency;
@class VirtualCategory;
@class AppStoreItem;

/**
 * This class represents a pack of the game's virtual currency.
 * For example: If you have a "Coin" as a virtual currency, you might
 * want to sell packs of "Coins". e.g. "10 Coins Set" or "Super Saver Pack".
 * The currency pack usually has an AppStoreItem related to it. As a developer,
 * you'll define the app store's item in Itunes Connect.
 */
@interface VirtualCurrencyPack : VirtualItem{
    int     currencyAmount;
    VirtualCurrency* currency;
    AppStoreItem* appstoreItem;
}


@property int     currencyAmount;
@property (retain, nonatomic) VirtualCurrency* currency;
@property (retain, nonatomic) AppStoreItem* appstoreItem;

/**
* oName is the name of the virtual currency pack.
* oDescription is the description of the virtual currency pack. This will show up
*                in the store in the description section.
* oItemId is the id of the virtual currency pack.
* productId is the product id on Google Market..
* oPrice is the actual $$ cost of the virtual currency pack.
* oCurrencyAmout is the amount of currency in the pack.
* oCurrency is the currency associated with this pack.
* oCategory is the category this currency pack is associated with.
*/
- (id)initWithName:(NSString*)oName andDescription:(NSString*)oDescription
    andItemId:(NSString*)oItemId andPrice:(double)oPrice
    andProductId:(NSString*)productId andCurrencyAmount:(int)oCurrencyAmount andCurrency:(VirtualCurrency*)oCurrency;

- (id)initWithDictionary:(NSDictionary*)dict;
- (NSDictionary*)toDictionary;

@end
