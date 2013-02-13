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

@class PriceModel;
@class VirtualCategory;

/**
 * This is a representation of the application's virtual good.
 * Virtual goods are bought with one or more VirtualCurrencies. The price
 * is determined by the PriceModel.
 */
@interface VirtualGood : VirtualItem{
    @private
    PriceModel* priceModel;
    VirtualCategory* category;
}

@property (retain, nonatomic) PriceModel* priceModel;
@property (retain, nonatomic) VirtualCategory* category;

/**
 * oName is the name of the virtual good.
 * oDescription is the description of the virtual good. This will show up
 *                in the store in the description section.
 * oPriceModel is the way the price of the current virtual good is calculated.
 * oItemId is the id of the virtual good.
 * oCategory is the category this virtual good is associated with.
 */
- (id)initWithName:(NSString*)oName andDescription:(NSString*)oDescription
    andItemId:(NSString*)oItemId andPriceModel:(PriceModel*)oPriceModel
       andCategory:(VirtualCategory*)oCategory;

- (id)initWithDictionary:(NSDictionary*)dict;
- (NSDictionary*)toDictionary;
/**
 * The currency value is calculated in the price model so we return the current price of the
 * virtual good as defined in its price model.
 */
- (NSDictionary*)currencyValues;

@end
