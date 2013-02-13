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

/** ABSTRACT
 * This class is the parent of all virtual items in the application.
 */
@interface VirtualItem : NSObject {
    NSString* name;
    NSString* description;
    NSString* itemId;
}

@property (retain, nonatomic) NSString* name;
@property (retain, nonatomic) NSString* description;
@property (retain, nonatomic) NSString* itemId;

- (id)init;
- (id)initWithName:(NSString*)name andDescription:(NSString*)description
    andItemId:(NSString*)itemId;
- (id)initWithDictionary:(NSDictionary*)dict;
- (NSDictionary*)toDictionary;

@end
