using UnityEngine;
using System;

namespace com.soomla.unity
{
	public static class AndroidJNIHandler
	{
#if UNITY_ANDROID
		public static void CallVoid(AndroidJavaObject jniObject, string method, string arg0) {
			if(!Application.isEditor){
				jniObject.Call(method, arg0);
				
				checkExceptions();
			}
		}
		
		public static T CallStatic<T>(AndroidJavaObject jniObject, string method, string arg0) {
			if (!Application.isEditor) {
				T retVal = jniObject.CallStatic<T>(method, arg0);

				checkExceptions();
				
				if (retVal is AndroidJavaObject) {
					if ((retVal as AndroidJavaObject).GetRawObject() == IntPtr.Zero) {
						throw new VirtualItemNotFoundException();
					}
				}

				return retVal;
			}
			
			return default(T);
		}
		
		public static T CallStatic<T>(AndroidJavaObject jniObject, string method, int arg0) {
			if (!Application.isEditor) {
				T retVal = jniObject.CallStatic<T>(method, arg0);
				
				checkExceptions();
				
				if (retVal is AndroidJavaObject) {
					if ((retVal as AndroidJavaObject).GetRawObject() == IntPtr.Zero) {
						throw new VirtualItemNotFoundException();
					}
				}
				
				return retVal;
			}
			
			return default(T);
		}

		public static void checkExceptions ()
		{
			IntPtr jException = AndroidJNI.ExceptionOccurred();
			if (jException != IntPtr.Zero) {				
				AndroidJavaObject exception = new AndroidJavaObject(jException);
				AndroidJNI.ExceptionClear();
				
				AndroidJavaClass jniExceptionClass = new AndroidJavaClass("com.soomla.store.exceptions.InsufficientFundsException");
				if (AndroidJNI.IsInstanceOf(exception.GetRawObject(), jniExceptionClass.GetRawClass())) {
					Debug.Log("SOOMLA/UNITY Cought InsufficientFundsException!");
					
					throw new InsufficientFundsException();
				}
				
				jniExceptionClass = new AndroidJavaClass("com.soomla.store.exceptions.VirtualItemNotFoundException");
				if (AndroidJNI.IsInstanceOf(exception.GetRawObject(), jniExceptionClass.GetRawClass())) {
					Debug.Log("SOOMLA/UNITY Cought VirtualItemNotFoundException!");
					
					throw new VirtualItemNotFoundException();
				}
				
				jniExceptionClass = new AndroidJavaClass("com.soomla.store.exceptions.NotEnoughGoodsException");
				if (AndroidJNI.IsInstanceOf(exception.GetRawObject(), jniExceptionClass.GetRawClass())) {
					Debug.Log("SOOMLA/UNITY Cought NotEnoughGoodsException!");
					
					throw new NotEnoughGoodsException();
				}
				
				Debug.Log("SOOMLA/UNITY Got an exception but can't identify it!");
			}
		}
#endif
	}
}

