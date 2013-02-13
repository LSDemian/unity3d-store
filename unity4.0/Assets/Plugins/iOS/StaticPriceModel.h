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

#import "PriceModel.h"

/**
 * This price model is a model that gives the associated virtual good a static price that's not affected by anything.
 */
@interface StaticPriceModel : PriceModel{
    NSDictionary* currencyValue;
}

@property (retain, nonatomic) NSDictionary* currencyValue;

- (id)initWithCurrencyValue:(NSDictionary*)oCurrencyValue;

// docs in parent
- (NSDictionary*)getCurrentPriceForVirtualGood:(VirtualGood*)virtualGood;
// docs in parent
- (NSDictionary*)toDictionary;

+ (StaticPriceModel*)modelWithNSDictionary:(NSDictionary*)dict;

@end
