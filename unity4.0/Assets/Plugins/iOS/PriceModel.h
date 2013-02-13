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

@class VirtualGood;

/** ABSTRACT
 * This abstract class represents a price-model used in every VirtualGood.
 */
@interface PriceModel : NSObject{
    @protected
    NSString* type;
}

@property (retain, nonatomic) NSString* type;

- (id)init;

/** ABSTRACT
 *
 * Fetch the price of the given VirtualGood (Usually, the given virtual good will be the same as the
 * virtual good that holds this price-model).
 *
 * The price of a VirtualGood is a hash that contains multiple currencies' itemIds and their needed
 * values. If the user doesn't own at least one of the needed currency balances,
 * he can't purchase the required VirtualGood.
 *
 * virtualGood is the virtual good to fetch the price for.
 */
- (NSDictionary*)getCurrentPriceForVirtualGood:(VirtualGood*)virtualGood;
/**
 * NOTE: This function is not abstract but surely needs to be overriden by classes that extends PriceModel.
 * 
 * Creates and returns a NSDictionary representation of the PriceModel.
 */
- (NSDictionary*)toDictionary;

/**
 * Creates the appropriate PriceModel with the given NSDictionary.
 * The appropriate PriceModel is determined by the "type" element inside the NSDictionary.
 *
 * dict is a NSDictionary representation of the required PriceModel.
 */
+ (PriceModel*)priceModelWithNSDictionary:(NSDictionary*)dict;
/**
 * Generates a NSDictionary out of a given price model.
 * priceModel is the required price model.
 */
+ (NSDictionary*)dictionaryWithPriceModel:(PriceModel*)priceModel;

@end
