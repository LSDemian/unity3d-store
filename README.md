*This project is a part of [The SOOMLA Project](http://project.soom.la) which is a series of open source initiatives with a joint goal to help mobile game developers get better stores and more in-app purchases.*

Haven't you ever wanted an in-app purchase one liner that looks like this ?!

```cs
    StoreController.BuyCurrencyPack("[Product id here]");
```

unity3d-store
---
The unity3d-store is the Unity3D flavour of The SOOMLA Project. This project uses [android-store](https://github.com/soomla/android-store) and [ios-store](https://github.com/soomla/ios-store) in order to provide game developers with in-app billing for their **Unity3D** projects.
    
**Before you start**, we suggest that you go over the information in ios-store and android-store so you get acquainted with the SOOMLA framework:
- ios-store [project](https://github.com/soomla/ios-store) [wiki](https://github.com/soomla/ios-store/wiki)
- android-store [project](https://github.com/soomla/android-store) [wiki](https://github.com/soomla/android-store/wiki)

>Soon, SOOMLA is going to provide developers with storefront capabilities through [Store Designer](http://designer.soom.la).

Download
---

We've created 2 unitypackages and one example project:

####unity3d-store v0.1 debug & release

- The **debug** version can be used for debugging (well... duh!). It'll print out various log messages that can help you understand how to fix things. If you want to ask a question, we'll need you to run with this unitypackage and show us the log.  
- On Android, there's another difference between **debug** and **release** versions. The difference is that when you run the **debug** you'll be able to test actual purchases (without even providing a valid public key) while with the **release** version you'll actually have to provide a valid publick key and upload the app binary to the dev console and do all the rest of Google's requirements.

[unity3d-store v0.1 debug](http://bit.ly/10Be6tF)  
[unity3d-store v0.1 release](http://bit.ly/12QW3iz)

####unity3d-store v0.1 example

- The example project is mostly what you have in this Github repo. You can either download it or clone unity3d-store.

[unity3d-store v0.1 example](http://bit.ly/TzHQl1)

Getting Started (with debug & release)
---

1. Download the unity3d-store unityproject file you want and double-click it. It'll import all the necessary files into your project.
2. Drag the "Soomla" Prefab into your scene. You should see it listed in the "Hierarchy" panel.
3. Click on the "Soomla" Prefab you just added and in the "Inspector" panel change the values for "Custom Secret", "Public Key" and "Soom Sec":
    - "Custom Secret" - is an encryption secret you provide that will be used to secure your data.
    - "Public Key" - is the public key given to you from Google. (iOS doesn't have a public key).
    - "Soom Sec" - is a special secret SOOMLA uses to increase your data protection.  
    **Choose both secrets wisely. You can't change them after you launch your game!**
4. Create your own implementation of _IStoreAssets_ in order to describe your specific game's assets ([example](https://github.com/soomla/unity3d-store/blob/master/src/Assets/Soomla/Code/MuffinRushAssets.cs)). Initialize _StoreController_ with the class you just created:

    ```cs
       StoreController.Initialize(new YourStoreAssetsImplementation());
    ```
    
    > Initialize _StoreController_ ONLY ONCE when your application loads.
    
    > Initialize _StoreController_ in the "Start()" function of a 'MonoBehaviour' and **NOT** in the "Awake()" function. SOOMLA has its own 'MonoBehaviour' and it needs to be "Awakened" before you initialize.

5. Now, that you have _StoreController_ loaded, just decide when you want to show/hide your store's UI to the user and let _StoreController_ know about it:

  When you show the store call:

    ```cs
    StoreController.storeOpening();
    ```

  When you hide the store call:

    ```cs
    StoreController.storeClosing();
    ```
    
    > Don't forget to make these calls. _StoreController_ has to know that you opened/closed your in-app purchase store. Just to make it clear: the in-app purchase store is where you sell virtual goods (and not Google Play or App Store).

6. You'll need an event handler in order to be notified about in-app purchasing related events. refer to the [Event Handling](https://github.com/soomla/unity3d-store#event-handling) section for more information.

And that's it ! You have storage and in-app purchasing capabilities... ALL-IN-ONE.

What's next? In App Purchasing.
---

unity3d-store provides you with VirtualCurrencyPacks. VirtualCurrencyPack is a representation of a "bag" of currency units that you want to let your users purchase in Google Play or App Store. You define VirtualCurrencyPacks in your game specific assets file which is your implementation of `IStoreAssets` ([example](https://github.com/soomla/unity3d-store/blob/master/src/Assets/Soomla/Code/MuffinRushAssets.cs)). After you do that you can call _StoreController_ to make actual purchases and unity3d-store will take care of the rest.

Example:

Lets say you have a VirtualCurrencyPack you call `TEN_COINS_PACK` and a VirtualCurrency you call `COIN_CURRENCY`:


```cs
VirtualCurrencyPack TEN_COINS_PACK = new VirtualCurrencyPack(
        "10 Coins",                // name
        "A pack of 10 coins",      // description
        "10_coins",                // item id
        "com.soomla.ten_coin_pack",// product id in Google Market AND App Store !
        1.99,                      // actual price in $$
        10,                        // number of currency units in the pack
        COIN_CURRENCY);            // the associated currency
```
     
Now you can use _StoreController_ to call Google Play or the App Store's in-app purchasing mechanism:

```cs
StoreController.BuyCurrencyPack(TEN_COINS_PACK.MarketItem.ProductId);
```
    
And that's it! unity3d-store knows how to contact Google Play or the App Store for you and redirect the user to the purchasing mechanism.
Don't forget to define your _IStoreEventHandler_ in order to get the events of successful or failed purchases (see [Event Handling](https://github.com/soomla/unity3d-store#event-handling)).

Storage & Meta-Data
---

When you initialize _StoreController_, it automatically initializes two other classes: _StoreInventory_ and _StoreInfo_.
- _StoreInventory_ is a convenience class to let you perform operations on VirtualCurrencies and VirtualGoods. Use it to fetch/change the balances of VirtualItems in your game (using their ItemIds!)
- _StoreInfo_ is where all meta data information about your specific game can be retrieved. It is initialized with your implementation of `IStoreAssets` and you can use it to retrieve information about your specific game.
**ATTENTION: because we're using JNI (Android) and DllImport (iOS) you should make as little calls as possible to _StoreInfo_. Look in the example project for the way we created a sort of a cache to hold your game's information in order to not make too many calls to _StoreInfo_. We update this cache using an event handler. (see [ExampleLocalStoreInfo](https://github.com/soomla/unity3d-store/blob/master/src/Assets/Soomla/Code/ExampleLocalStoreInfo.cs) and [ExampleEventHandler](https://github.com/soomla/unity3d-store/blob/master/src/Assets/Soomla/Code/ExampleEventHandler.cs)).**

The on-device storage is encrypted and kept in a SQLite database. SOOMLA is preparing a cloud-based storage service that will allow this SQLite to be synced to a cloud-based repository that you'll define.

**Example Usages**

* Get VirtualCurrency with itemId "currency_coin":

    ```cs
    VirtualCurrency coin = StoreInfo.GetVirtualCurrencyByItemId("currency_coin");
    ``` 

* Add 10 coins to the virtual currency with itemId "currency_coin":

    ```cs
    StoreInventory.AddCurrencyAmount("currency_coin", 10);
    ```
    
* Remove 10 virtual goods with itemId "green_hat":

    ```cs
    StoreInventory.RemoveGoodAmount("green_hat", 10);
    ```
    
* Get the current balance of green hats (virtual goods with itemId "green_hat"):

    ```cs
    int greenHatsBalance = StoreInventory.GetGoodBalance("green_hat");
    ```

Event Handling
---

SOOMLA lets you create your own event handler and add it to _StoreEventHandlers_. That way you'll be able to get notifications on various events and implement your own application specific behaviour to those events.

> Your behaviour is an addition to the default behaviour implemented by SOOMLA. You don't replace SOOMLA's behaviour.

In order to create your event handler:

1. Create a class that implements _IStoreEventHandler_ (see [ExampleEventHandler](https://github.com/soomla/unity3d-store/blob/master/src/Assets/Soomla/Code/ExampleEventHandler.cs)).
2. Add the created class to _StoreEventHandlers_:
 `StoreEventHandlers.AddEventHandler(new YourEventHandler());`

Contribution
---

We want you!

Fork -> Clone -> Implement -> Test -> Pull-Request. We have great RESPECT for contributors.

SOOMLA, Elsewhere ...
---

+ [Website](http://project.soom.la/)
+ [On Facebook](https://www.facebook.com/pages/The-SOOMLA-Project/389643294427376).
+ [On AngelList](https://angel.co/the-soomla-project)

License
---
MIT License. Copyright (c) 2012 SOOMLA. http://project.soom.la
+ http://www.opensource.org/licenses/MIT


