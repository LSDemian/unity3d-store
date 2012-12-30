*This project is a part of [The SOOMLA Project](http://project.soom.la) which is a series of open source initiatives with a joint goal to help mobile game developers get better stores and more in-app purchases.*

Didn't you ever wanted an in-app purchase one liner that looks like this ?!

```cs
    StoreController.BuyCurrencyPack("[Product id here]");
```

unity3d-store
---
The unity3d-store is the Unity3D flavour of The SOOMLA Project. This project uses [android-store](https://github.com/soomla/android-store) and [ios-store](https://github.com/soomla/ios-store) in order to provide game developers with in-app billing for their **Unity3D** projects.
    
**Before you start**, we suggest that you go over the information in ios-store and android-store so you get acquainted with the SOOMLA framework:
- ios-store [project](https://github.com/soomla/ios-store) [wiki](https://github.com/soomla/ios-store/wiki)
- android-store [project](https://github.com/soomla/android-store) [wiki](https://github.com/soomla/android-store/wiki)

>Soon, SOOMLA is going to provide developers with storefront capabilities through [Store Designer](designer.soom.la).

Download
---

We've created 2 unitypackages and one example project:

####unity3d-store v0.1 debug & release

- The **debug** version can be used for debugging (well... duh!). It'll print out various log messages that can help you understand how to fix things. If you want to ask a question, we'll need you to run with this unitypackage and show us the log.  
- On Android, there's another difference between **debug** and **release** versions. The difference is that when you run the **debug** you'll be able to test actual purchases (without even providing a valid public key) while with the **release** version you'll actually have to provide a valid publick key and upload the app binary to the dev console and do all the rest of Google's requirements.

[unity3d-store v0.1 debug](http://dl.dropbox.com/u/88939562/unity3d/soomla-unity3d-store_debug-v0.1.unitypackage)  
[unity3d-store v0.1 release](http://dl.dropbox.com/u/88939562/unity3d/soomla-unity3d-store_release-v0.1.unitypackage)

####unity3d-store v0.1 example

- The example project is mostly what you have in this Github repo. You can either download it or clone unity3d-store.

[unity3d-store v0.1 example](http://dl.dropbox.com/u/88939562/unity3d/soomla-unity3d-store_example-v0.1.zip)

Getting Started (with debug & release)
---

1. Download the unity3d-store unityproject file you want and double-click it. It'll import all the necessary files into your project.
2. Drag the "Soomla" Prefab into your scene. You should see it listed in the "Hierarchy" panel.
3. Click on the "Soomla" Prefab you just added and in the "Inspector" panel change the values for "Custom Secret", "Public Key" and "Soom Sec":
    - "Custom Secret" - is an encryption secret you provide that'll be used to secure your data.
    - "Public Key" - is the public key given to you from Google. (iOS doesn't have a public key).
    - "Soom Sec" - is a special secret SOOMLA uses to increase your data protection.  
    **Choose both secrets wisely. You can't change them after you launch your game!**
4. Create your own implementation of _IStoreAssets_ in order to describe your specific game's assets ([example](https://github.com/soomla/unity3d-store/blob/master/src/Assets/Soomla/Code/MuffinRushAssets.cs)). Initialize _StoreController_ with the class you just created:

    ```cs
       StoreController.Initialize(new YourStoreAssetsImplementation());
    ```
    
    > Initialize StoreController ONLY ONCE when your application loads.
    
    > Initialize StoreController in the "Start()" function of a 'MonoBehaviour' and **NOT** in the "Awake()" function. SOOMLA has its own 'MonoBehaviour' and it needs to be "Awaked" before you initialize.

5. Now, that you have StoreController loaded, just decide when you want to show/hide your store's UI to the user and let StoreController know about it:

  When you show the store call:

    ```cs
    StoreController.storeOpening();
    ```

  When you hide the store call:

    ```cs
    StoreController.storeClosing();
    ```
    
> Don't forget to make these calls. StoreController has to know that you opened/closed your in-app purchase store. Just to make it clear: the in-app purchase store is where you sell virtual goods (and not Google Play or App Store).

6. You'll need an event handler in order to be notified about in-app purchasing related events. refer to the [Event Handling]() section for more information.

And that's it ! You have storage and in-app purchasing capabilities... ALL-IN-ONE.


