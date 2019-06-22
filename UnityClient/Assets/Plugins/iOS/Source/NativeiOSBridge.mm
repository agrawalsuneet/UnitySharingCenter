#include "iOSClient-Swift.h"

#pragma mark - C interface

extern "C" {
    
    void _nativeiOS_showAlertDialog(String title, String message, String actionMessage) {
        [[NativeiOS shared] showAlertDialog:title message:message actionMessage:actionMessage];
    }
    
    void _nativeiOS_showAlertDialog(){
        [[NativeiOS shared] showAlertDialog];
    }
}
#include "iOSClient-Swift.h"
