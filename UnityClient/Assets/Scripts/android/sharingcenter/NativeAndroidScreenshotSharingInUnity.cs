using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NativeAndroidScreenshotSharingInUnity : MonoBehaviour {

	public  Button shareButton;

    public string shareSubject = "I challenge you to beat my high score in Fire Block";
	public string shareMessage = "I challenge you to beat my high score in Fire Block. " +
		". Get the Fire Block app from the link below. \nCheers\n" +
		"\nhttp://onelink.to/fireblock";

	private bool isFocus = false;
    
	private bool isProcessing = false;
	private string screenshotName;

	void  Start () {
		shareButton.onClick.AddListener (OnShareButtonClick);
	}


	void OnApplicationFocus (bool focus) {
		isFocus = focus;
	}

	public void OnShareButtonClick () {

		screenshotName = "fireblock_highscore.png";
		

		ShareScreenshot ();
	}


	private void ShareScreenshot () {

		#if UNITY_ANDROID
		if (!isProcessing) {
			StartCoroutine (ShareScreenshotInAnroid ());
		}

		#else
		Debug.Log("No sharing set up for this platform.");
		#endif
	}



	#if UNITY_ANDROID
	public IEnumerator ShareScreenshotInAnroid () {

		isProcessing = true;
		// wait for graphics to render
		yield return new WaitForEndOfFrame ();

		string screenShotPath = Application.persistentDataPath + "/" + screenshotName;
		ScreenCapture.CaptureScreenshot (screenshotName, 1);
		yield return new WaitForSeconds (0.5f);

		if (!Application.isEditor) {
			//Create intent for action send
			AndroidJavaClass intentClass = new AndroidJavaClass ("android.content.Intent");
			AndroidJavaObject intentObject = new AndroidJavaObject ("android.content.Intent");
			intentObject.Call<AndroidJavaObject> ("setAction", intentClass.GetStatic<string> ("ACTION_SEND"));

			//create image URI to add it to the intent
			AndroidJavaClass uriClass = new AndroidJavaClass ("android.net.Uri");
			AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject> ("parse", "file://" + screenShotPath);

			//put image and string extra
			intentObject.Call<AndroidJavaObject> ("putExtra", intentClass.GetStatic<string> ("EXTRA_STREAM"), uriObject);
			intentObject.Call<AndroidJavaObject> ("setType", "image/png");
			intentObject.Call<AndroidJavaObject> ("putExtra", intentClass.GetStatic<string> ("EXTRA_SUBJECT"), shareSubject);
			intentObject.Call<AndroidJavaObject> ("putExtra", intentClass.GetStatic<string> ("EXTRA_TEXT"), shareMessage);

			AndroidJavaClass unity = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
			AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject> ("currentActivity");
			AndroidJavaObject chooser = intentClass.CallStatic<AndroidJavaObject> ("createChooser", intentObject, "Share your high score");
			currentActivity.Call ("startActivity", chooser);
		}

		yield return new WaitUntil (() => isFocus);
		isProcessing = false;
	}
	#endif
}