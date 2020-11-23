using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NativeAndroidWhatsappContactDirectShare : MonoBehaviour {

	public  Button shareButton;

    private string packageName = "com.whatsapp";

	private bool isFocus = false;
	private bool isProcessing = false;

	void  Start () {
		shareButton.onClick.AddListener (ShareText);
	}

	void OnApplicationFocus (bool focus) {
		isFocus = focus;
	}

	private void ShareText () {

		#if UNITY_ANDROID
		if (!isProcessing) {

			//check if app installed
			if (CheckIfAppInstalled ()) {
				StartCoroutine (ShareTextToWhatsContact());
			} else {
				//fallback plan
				//can either disable the whatsapp share button
				//or can a normal share trigger
			}
		}
		#else
		Debug.Log("No sharing set up for this platform.");
		#endif
	}

	private bool CheckIfAppInstalled () {

    #if UNITY_ANDROID

		//create a class reference of unity player activity
		AndroidJavaClass unityActivity = 
			new AndroidJavaClass ("com.unity3d.player.UnityPlayer");

		//get the context of current activity
		AndroidJavaObject context = unityActivity.GetStatic<AndroidJavaObject> ("currentActivity");

		//get package manager reference
		AndroidJavaObject packageManager = context.Call<AndroidJavaObject> ("getPackageManager");

		//get the list of all the apps installed on the device
		AndroidJavaObject appsList = packageManager.Call<AndroidJavaObject> ("getInstalledPackages", 1);

		//get the size of the list for app installed apps
		int size = appsList.Call<int> ("size");

		for (int i = 0; i < size; i++) {
			AndroidJavaObject appInfo = appsList.Call<AndroidJavaObject> ("get", i);
			string packageNew = appInfo.Get<string> ("packageName");

			if (packageNew.CompareTo (packageName) == 0) {
				return true;
			}
		}

		return false;

    #endif
		return false;
	}

	#if UNITY_ANDROID
	public IEnumerator ShareTextToWhatsContact () {

		isProcessing = true;

		if (!Application.isEditor) {

			//var url = "https://api.whatsapp.com/send?phone=${mobileNumber}&text=You%20can%20now%20send%20me%20audio%20and%20video%20messages%20on%20the%20app%20-%20Chirp.%20%0A%0Ahttps%3A//bit.ly/chirp_android";
			var url = "https://api.whatsapp.com/send?phone=+919876543210&text=You%20can%20now%20send%20me%20audio%20and%20video%20messages%20on%20the%20app%20-%20Chirp.%20%0A%0Ahttps%3A//bit.ly/chirp_android";

			//Create intent for action send
			AndroidJavaClass intentClass = 
				new AndroidJavaClass ("android.content.Intent");
			AndroidJavaObject intentObject = 
				new AndroidJavaObject ("android.content.Intent");
			intentObject.Call<AndroidJavaObject> 
			("setAction", intentClass.GetStatic<string> ("ACTION_VIEW"));

			//uri class
			AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");

			//set data
			intentObject.Call<AndroidJavaObject>("setData", 
				uriClass.CallStatic<AndroidJavaObject>("parse", url));

			//set the package to whatsapp package
			intentObject.Call<AndroidJavaObject> ("setPackage", packageName);

			//call start activity method
			AndroidJavaClass unity = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
			AndroidJavaObject currentActivity = 
				unity.GetStatic<AndroidJavaObject> ("currentActivity");
			currentActivity.Call ("startActivity", intentObject);
		}

		yield return new WaitUntil (() => isFocus);
		isProcessing = false;
	}
		
	#endif
}