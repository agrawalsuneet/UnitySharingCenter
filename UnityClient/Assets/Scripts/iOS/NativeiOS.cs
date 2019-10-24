using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Runtime.InteropServices;

public class NativeiOS : MonoBehaviour
{

    #region Declare external C interface    
    #if UNITY_IOS && !UNITY_EDITOR

   [DllImport("__Internal")]
   private static extern void _nativeiOS_showAlertDialog();

    #endif
    #endregion

    #region Wrapped methods and properties
   public void showAlertDialog() {
       #if UNITY_IOS && !UNITY_EDITOR
       _nativeiOS_showAlertDialog();
       #endif
   }
   #endregion

    public Button shareButton;

    void Start()
    {
        shareButton.onClick.AddListener(OnShareButtonClick);
    }

    public void OnShareButtonClick()
    {
        #if UNITY_IOS
        showAlertDialog();
        #endif
    }

    

}
