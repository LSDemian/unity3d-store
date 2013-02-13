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

/**
 * This class provide basic storage operations on VirtualGoods.
 */
@interface VirtualGoodStorage : NSObject

/**
 * Fetch the balance of the given virtual good.
 * virtualGood is the required virtual good.
 */
- (int)getBalanceForGood:(VirtualGood*)virtualGood;
/**
 * Adds the given amount of goods to the storage.
 * virtualGood is the required virtual good.
 * amount is the amount of goods to add.
 */
- (int)addAmount:(int)amount toGood:(VirtualGood*)virtualGood;
/**
 * Removes the given amount from the given virtual good's balance.
 * virtualGood is the virtual good to remove the given amount from.
 * amount is the amount to remove.
 */
- (int)removeAmount:(int)amount fromGood:(VirtualGood*)virtualGood;
/**
 * Fetch the equip status of the given VirtualGood.
 */
- (BOOL)isGoodEquipped:(VirtualGood*)virtualGood;
/**
 * Sets the equip status of the given VirtualGood.
 * virtualGood is the required VirtualGood
 * equip is the boolean equip status of the VirtualGood
 */
- (void)equipGood:(VirtualGood*)virtualGood withEquipValue:(BOOL)equip;

@end
