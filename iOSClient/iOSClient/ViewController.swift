//
//  ViewController.swift
//  iOSClient
//
//  Created by Suneet on 11/06/19.
//  Copyright © 2019 Suneet. All rights reserved.
//

import UIKit

class ViewController: UIViewController {

    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view.
        
        
    }
    
    override func viewDidAppear(_ animated: Bool) {
        super.viewDidAppear(animated)
        
        //NativeiOS.shared.showAlertDialog(title: "Hello", message: "How are you", actionMessage: "Dismiss")
        NativeiOS.shared.showAlertDialog()
    }


}

