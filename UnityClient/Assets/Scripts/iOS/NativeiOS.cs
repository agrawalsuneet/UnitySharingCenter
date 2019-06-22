using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Runtime.InteropServices;

public class NativeiOS : MonoBehaviour
{
    public Button shareButton;

    void Start()
    {
        shareButton.onClick.AddListener(OnShareButtonClick);
    }

    public void OnShareButtonClick()
    {
        #if UNITY_IOS
        _nativeiOS_showAlertDialog();
        #endif
    }

    #region Declare external C interface    
    #if UNITY_IOS && !UNITY_EDITOR

   [DllImport("__Internal")]
   private static extern void _nativeiOS_showAlertDialog();

    #endif
    #endregion

}
