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


/**
 * This protocol represents a single game's metadata.
 * Use this protocol to create your assets class that will be transferred to StoreInfo
 * upon initialization.
 */
@protocol IStoreAsssets <NSObject>

/**
 * A version for your specific game's store assets
 * 
 * This value will determine if the saved data in the database will be deleted or not.
 * Bump the version every time you want to delete the old data in the DB.
 * If you don't bump this value, you won't be able to see changes you've made to the objects in this file.
 *
 * For example: If you previously created a VirtualGood with name "Hat" and you published your application,
 * the name "Hat will be saved in any of your users' databases. If you want to change the name to "Green Hat"
 * than you'll also have to bump the version (from 0 to 1). Now the new "Green Hat" name will replace the old one.
 */
- (int)getVersion;

/**
 * A representation of your game's virtual currency.
 */
- (NSArray*)virtualCurrencies;

/**
 * An array of all virtual goods served by your store.
 * NOTE: The order of the items in the array will be their order when shown to the user.
 */
- (NSArray*)virtualGoods;

/**
 * An array of all virtual currency packs served by your store.
 * NOTE: The order of the items in the array will be their order when shown to the user.
 */
- (NSArray*)virtualCurrencyPacks;

/**
 * An array of all virtual categories served by your store.
 */
- (NSArray*)virtualCategories;

/**
 * You can define NON-CONSUMABLE items that you'd like to use for your needs.
 * CONSUMABLE items are usually just currency packs. If you use SOOMLA's storefront, it'll take care of
 * the UNMANAGED for you in the UI.
 * NON-CONSUMABLE items are usually used to let users purchase a "no-ads" token.
 * Make sure you set the type of the items you add here as Consumable.kNonConsumable.
 *
 * An array of all managed served in your game.
 */
- (NSArray*)nonConsumableItems;

@end
