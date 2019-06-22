//
//  NativeiOS.swift
//  iOSClient
//
//  Created by Suneet on 11/06/19.
//  Copyright © 2019 Suneet. All rights reserved.
//

import Foundation
import UIKit

@objc public class NativeiOS : NSObject {
    @objc static let shared = NativeiOS()
    
    @objc func showAlertDialog(title : String, message : String, actionMessage : String){
        let alert = UIAlertController.init(title: title, message: message, preferredStyle: .alert)
        alert.addAction(UIAlertAction(title: actionMessage, style: .default))
        
        UIApplication.shared.keyWindow?.rootViewController?.present(alert, animated: true, completion: nil)
    }
    
    @objc func showAlertDialog(){
        let alert = UIAlertController.init(title: "Custom title", message: "Custom message", preferredStyle: .alert)
        alert.addAction(UIAlertAction(title: "Done", style: .default))
        
        UIApplication.shared.keyWindow?.rootViewController?.present(alert, animated: true, completion: nil)
    }
}
