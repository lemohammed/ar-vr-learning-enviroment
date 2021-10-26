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
using Logger = file.logger.Logger;
using CustomEventLogger = events.logger.CustomEventLogger;


public class Task1 : MonoBehaviour
{
    private int clickCount = 0;
    public GameObject Task1Object;
    public GameObject Task2Object;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TaskOnClick()
    {
        clickCount++;
        onTryTaskComplete();
    }
    public void onTryTaskComplete() {
        if (clickCount >= 2)
        {
            Debug.Log("Task Complete");
            Logger.AppendDataToFile(Logger.TASK_LOGS, string.Format("{0}{1}{2}{3}{4}", Logger.getUnixTime().ToString(),Logger.delimeter, CustomEventLogger.currentTimerTime,Logger.delimeter, '2'));
            CustomEventLogger.ResetTimer();
            Task1Object.SetActive(false);
            Task2Object.SetActive(true);
        }
     }
}
