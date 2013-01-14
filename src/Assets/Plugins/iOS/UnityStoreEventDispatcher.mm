
#import "UnityStoreEventDispatcher.h"
#import "EventHandling.h"
#import "AppStoreItem.h"
#import "VirtualGood.h"
#import "VirtualCurrency.h"

@implementation UnityStoreEventDispatcher

- (id) init {
    if (self = [super init]) {
        [EventHandling observeAllEventsWithObserver:self withSelector:@selector(handleEvent:)];
    }
    
    return self;
}

- (void)handleEvent:(NSNotification*)notification{
    if ([notification.name isEqualToString:EVENT_APPSTORE_PURCHASED]) {
        AppStoreItem* asi = [notification.userInfo objectForKey:@"AppStoreItem"];
        UnitySendMessage("Soomla", "onMarketPurchase", [asi.productId UTF8String]);
    } else if ([notification.name isEqualToString:EVENT_VIRTUAL_GOOD_PURCHASED]) {
        VirtualGood* vg = [notification.userInfo objectForKey:@"VirtualGood"];
        UnitySendMessage("Soomla", "onVirtualGoodPurchased", [vg.itemId UTF8String]);
    } else if ([notification.name isEqualToString:EVENT_VIRTUAL_GOOD_EQUIPPED]) {
        VirtualGood* vg = [notification.userInfo objectForKey:@"VirtualGood"];
        UnitySendMessage("Soomla", "onVirtualGoodEquiped", [vg.itemId UTF8String]);
    } else if ([notification.name isEqualToString:EVENT_VIRTUAL_GOOD_UNEQUIPPED]) {
        VirtualGood* vg = [notification.userInfo objectForKey:@"VirtualGood"];
        UnitySendMessage("Soomla", "onVirtualGoodUnequiped", [vg.itemId UTF8String]);
    } else if ([notification.name isEqualToString:EVENT_BILLING_SUPPORTED]) {
        UnitySendMessage("Soomla", "onBillingSupported", "");
    } else if ([notification.name isEqualToString:EVENT_BILLING_NOT_SUPPORTED]) {
        UnitySendMessage("Soomla", "onBillingNotSupported", "");
    } else if ([notification.name isEqualToString:EVENT_MARKET_PURCHASE_STARTED]) {
        AppStoreItem* asi = [notification.userInfo objectForKey:@"AppStoreItem"];
        UnitySendMessage("Soomla", "onBillingNotSupported", [asi.productId UTF8String]);
    }  else if ([notification.name isEqualToString:EVENT_CLOSING_STORE]) {
        UnitySendMessage("Soomla", "onClosingStore", "");
    }  else if ([notification.name isEqualToString:EVENT_UNEXPECTED_ERROR_IN_STORE]) {
        UnitySendMessage("Soomla", "onUnexpectedErrorInStore", "");
    }  else if ([notification.name isEqualToString:EVENT_OPENING_STORE]) {
        UnitySendMessage("Soomla", "onOpeningStore", "");
    }  else if ([notification.name isEqualToString:EVENT_GOODS_PURCHASE_STARTED]) {
        UnitySendMessage("Soomla", "onGoodsPurchaseProcessStarted", "");
    }  else if ([notification.name isEqualToString:EVENT_CHANGED_CURRENCY_BALANCE]) {
	    NSDictionary* userInfo = [notification userInfo];
	    VirtualCurrency* vc = [userInfo objectForKey:@"VirtualCurrency"];
		int balance = [(NSNumber*)[userInfo objectForKey:@"balance"] intValue];
        UnitySendMessage("Soomla", "onCurrencyBalanceChanged", [[NSString stringWithFormat:@"%@#SOOM#%d", vc.itemId, balance] UTF8String]);
    }  else if ([notification.name isEqualToString:EVENT_CHANGED_GOOD_BALANCE]) {
	    NSDictionary* userInfo = [notification userInfo];
	    VirtualGood* vg = [userInfo objectForKey:@"VirtualGood"];
		int balance = [(NSNumber*)[userInfo objectForKey:@"balance"] intValue];
        UnitySendMessage("Soomla", "onGoodBalanceChanged", [[NSString stringWithFormat:@"%@#SOOM#%d", vg.itemId, balance] UTF8String]);
    }
}

@end
