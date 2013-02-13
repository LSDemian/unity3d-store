//
//  NotEnoughGoodsException.h
//  SoomlaiOSStore
//
//  Created by Refael Dakar on 10/2/12.
//  Copyright (c) 2012 SOOMLA. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface NotEnoughGoodsException : NSException

- (id)initWithItemId:(NSString*)itemId;

@end
