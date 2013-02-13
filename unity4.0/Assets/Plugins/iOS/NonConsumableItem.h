//
//  NonConsumableItem.h
//  SoomlaiOSStoreExample
//
//  Created by Refael Dakar on 08/01/13.
//  Copyright (c) 2013 SOOMLA. All rights reserved.
//

#import "VirtualItem.h"

@class AppStoreItem;

@interface NonConsumableItem : VirtualItem {
    AppStoreItem* appStoreItem;
}

@property (nonatomic, retain) AppStoreItem* appStoreItem;

- (id)initWithName:(NSString*)oName andDescription:(NSString*)oDescription
         andItemId:(NSString*)oItemId andPrice:(double)oPrice
      andProductId:(NSString*)productId;

- (id)initWithDictionary:(NSDictionary*)dict;
- (NSDictionary*)toDictionary;

@end
