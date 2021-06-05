using UnityEngine;

public class CastToJavaObject : MonoBehaviour
{
    //C# Code
    public AndroidJavaObject castToJavaObject(AndroidJavaObject source, string className)
    {
        var clazz = new AndroidJavaClass("java.lang.Class");
        var destClass = clazz.CallStatic<AndroidJavaObject>("forName", className);
        return destClass.Call<AndroidJavaObject>("cast", source);
    }
}