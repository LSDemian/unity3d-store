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

@class StoreDatabase;
@class VirtualGoodStorage;
@class VirtualCurrencyStorage;
@class NonConsumableStorage;


/**
 * This is the place where all the relevant storage classes are created.
 * This is a singleton class and you can call it from your application in order
 * to get the instances of the Virtual goods/currency storages.
 *
 * You will usually need the storage in order to get/set the amounts of virtual goods/currency.
 */
@interface StorageManager : NSObject{
    StoreDatabase* database;
    VirtualGoodStorage* virtualGoodStorage;
    VirtualCurrencyStorage* virtualCurrenctStorage;
    NonConsumableStorage* nonConsumableStorage;
}

@property (nonatomic, retain)StoreDatabase* database;
@property (nonatomic, retain)VirtualGoodStorage* virtualGoodStorage;
@property (nonatomic, retain)VirtualCurrencyStorage* virtualCurrencyStorage;
@property (nonatomic, retain)NonConsumableStorage* nonConsumableStorage;

+ (StorageManager*)getInstance;

- (id)init;

@end
