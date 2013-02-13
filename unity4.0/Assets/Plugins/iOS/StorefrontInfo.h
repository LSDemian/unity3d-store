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

/**
 * This class is used to retrieve the storefront JSON when it's needed.
 */
@interface StorefrontInfo : NSObject{
    @private
    BOOL orientationLandscape;
    @public
    NSString* storefrontJson;
}

@property (nonatomic, retain) NSString* storefrontJson;
@property BOOL orientationLandscape;

+ (StorefrontInfo*)getInstance;

/**
 * This function initializes StorefrontInfo. On first initialization, when the
 * database doesn't have any previous version of the store metadata (JSON), StorefrontInfo
 * saves the given JSON to the DB. After the first initialization,
 * StorefrontInfo will load the JSON metadata when needed.
 * NOTE: If you want to override the current StorefrontInfo metadata JSON file, you'll have to bump the
 * database version (the old database will be destroyed but balances will be saved!!).
 */
- (void)initializeWithJSON:(NSString*)sfJSON;

- (BOOL)initializeFromDB;
- (NSDictionary*)toDictionary;

@end
