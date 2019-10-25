using UnityEngine.UI;
using UnityEngine;

public class NativeAndroidInUnity : MonoBehaviour
{
    public Button showToastButton;
    public string toastText = "This is a Toast";

    void Start()
    {
        showToastButton.onClick.AddListener(onShowToastClicked);
    }


    public void onShowToastClicked()
    {
#if UNITY_ANDROID
        showAndroidToast();
#else
		Debug.Log("No Toast setup for this platform.");
#endif
    }


    private void showAndroidToast()
    {
        //create a Toast class object
        AndroidJavaClass toastClass =
                    new AndroidJavaClass("android.widget.Toast");

        //create an array and add params to be passed
        object[] toastParams = new object[3];
        AndroidJavaClass unityActivity =
          new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        toastParams[0] =
                     unityActivity.GetStatic<AndroidJavaObject>
                               ("currentActivity");
        toastParams[1] = toastText;
        toastParams[2] = toastClass.GetStatic<int>
                               ("LENGTH_LONG");

        //call static function of Toast class, makeText
        AndroidJavaObject toastObject =
                        toastClass.CallStatic<AndroidJavaObject>
                                      ("makeText", toastParams);

        //show toast
        toastObject.Call("show");

    }
}

