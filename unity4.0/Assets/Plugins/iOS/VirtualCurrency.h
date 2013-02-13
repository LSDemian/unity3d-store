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

/**
 * This is a representation of the game's virtual currency.
 * Each game can have multiple instances of a virtual currency, all kept in StoreInfo;
 */
@interface VirtualCurrency : VirtualItem{
    
}

- (id)initWithName:(NSString*)name andDescription:(NSString*)description
    andItemId:(NSString*)itemId;
- (id)initWithDictionary:(NSDictionary*)dict;
- (NSDictionary*)toDictionary;

@end
