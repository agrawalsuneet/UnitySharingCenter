using UnityEngine;
using UnityEngine.UI;

public class AcceptIncomingText : MonoBehaviour
{
    public Button acceptIncomingTextBtn;
    void Start()
    {
        acceptIncomingTextBtn.onClick.AddListener(onAcceptImageClicked);
    }

    public void onAcceptImageClicked()
    {
    #if UNITY_ANDROID
            acceptIncomingImage();
    #else
            Debug.Log("No Toast setup for this platform.");
    #endif
    }


    void acceptIncomingImage()
    {
        //create a class reference of unity player activity
		AndroidJavaClass unityActivity = 
			new AndroidJavaClass ("com.unity3d.player.UnityPlayer");

		//get the context of current activity
		AndroidJavaObject context = unityActivity.GetStatic<AndroidJavaObject> ("currentActivity");
        
        //get the current intent object
        AndroidJavaObject intent = context.Call<AndroidJavaObject>("getIntent");

        //below line is for debug purpose
        Debug.Log("UnityClientDebugging GetExtra type: " + intent.Call<string>("getType"));
        //it will print
        //UnityClientDebugging GetExtra type: text/plain

        //get the extra text from intent
        string incomingText = intent.Call<string>("getStringExtra", "android.intent.extra.TEXT");

        //set it as button text
        acceptIncomingTextBtn.GetComponentInChildren<Text>().text = incomingText;
    }
}
