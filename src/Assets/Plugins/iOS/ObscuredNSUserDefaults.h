//
//  ObscuredNSUserDefaults.h
//  SoomlaiOSStoreExample
//
//  Created by Refael Dakar on 26/12/12.
//  Copyright (c) 2012 SOOMLA. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface ObscuredNSUserDefaults : NSObject {

}

+ (BOOL)boolForKey:(NSString *)defaultName;
+ (NSString*)stringForKey:(NSString *)defaultName;
+ (void)setBool:(BOOL)value forKey:(NSString *)defaultName;
+ (void)setString:(NSString*)value forKey:(NSString *)defaultName;

@end
