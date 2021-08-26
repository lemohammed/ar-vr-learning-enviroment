using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Microsoft.MixedReality.Toolkit.UI;
using TMPro;
using System.Text;
using System.IO;
using System;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;

public class RouteCheckbox : MonoBehaviour
{
    public GameObject Button1;
    public GameObject Button2;
    public GameObject Button3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TaskOnClick(){
        bool toggle1 = Button1.GetComponent<Interactable>().IsToggled;
        bool toggle2 = Button1.GetComponent<Interactable>().IsToggled;
        bool toggle3 = Button1.GetComponent<Interactable>().IsToggled;
    
    }
 
}
