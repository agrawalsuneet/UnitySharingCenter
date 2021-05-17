using UnityEngine.UI;
using UnityEngine;
using System;

public class AcceptIncomingImage : MonoBehaviour
{

    public Button acceptIncomingImageBtn;
    public Image image;

    // Start is called before the first frame update
    void Start()
    {
        acceptIncomingImageBtn.onClick.AddListener(onAcceptImageClicked);
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
        AndroidJavaClass uriAndroid = 
			new AndroidJavaClass ("android.net.Uri");

		AndroidJavaClass unityActivity = 
			new AndroidJavaClass ("com.unity3d.player.UnityPlayer");

        AndroidJavaClass intentClass = new AndroidJavaClass ("android.content.Intent");

		//get the context of current activity
		AndroidJavaObject context = unityActivity.GetStatic<AndroidJavaObject> ("currentActivity");
        
        AndroidJavaObject intent = context.Call<AndroidJavaObject>("getIntent");

        string type = intent.Call<string>("getType");

        if (type == null){
            Debug.Log("Ballu type is null");
        } else {
            Debug.Log("Ballu type: " + type);
        }

        AndroidJavaObject parcelable = intent.Call<AndroidJavaObject>("getParcelableExtra", "android.intent.extra.STREAM");
        AndroidJavaObject uri = Cast(parcelable, "android.net.Uri");
        

        String uriString = uri.Call<String>("toString");
        if (uriString == null){
            Debug.Log("Ballu uriString is null");
        } else {
            Debug.Log("Ballu uriString: " + uriString);
        }

        var bytes = getBytes(context, uri);
        if (bytes == null){
            Debug.Log("Ballu bytes is null");
        } else {
            Debug.Log("Ballu bytes: " + bytes);
        }

        Texture2D texture = new Texture2D(1024, 1024);
        texture.LoadImage(bytes);
        //image.
    }

    public AndroidJavaObject ClassForName(string className)
    {
        var clazz = new AndroidJavaClass("java.lang.Class");
        return clazz.CallStatic<AndroidJavaObject>("forName", className);
    }

    public AndroidJavaObject Cast(AndroidJavaObject source, string destClass)
    {
        var destClassAJC = ClassForName(destClass);
        return destClassAJC.Call<AndroidJavaObject>("cast", source);
    }

    public byte[] getBytes(AndroidJavaObject context, AndroidJavaObject uri) {
        AndroidJavaObject contentResolver = context.Call<AndroidJavaObject>("getContentResolver");
        AndroidJavaObject inputStream = contentResolver.Call<AndroidJavaObject>("openInputStream", uri);
        AndroidJavaObject byteBuffer = new AndroidJavaObject("java.io.ByteArrayOutputStream");

        byte[] buffer = new byte[1024];

        int len = 0;
        while((len = inputStream.Call<int>("read", buffer)) != -1){
            byteBuffer.Call("write", buffer, 0, len);
        }

        byte[] output = byteBuffer.Call<byte[]>("toByteArray");
        return output;
    }
}