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

public class CustomEventLogger : MonoBehaviour, IMixedRealitySourceStateHandler, IMixedRealityHandJointHandler
{
    private int frames = 0;
    //Lesson list containing 
    private int lessonID = 0;
    private string starttime;
    public EyeTracking eyeTracking;
    //Start time, used to create Output folder on the hololens
    private Vector3 position;

    int getUnixTime()
    {
        System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
        int cur_time = (int)(System.DateTime.UtcNow - epochStart).TotalSeconds;
        return cur_time;
    }
    // Start is called before the first frame update
    void Start()
    {
        AppendDataToFile("StartUpLogs", "Start up successfull at," + getUnixTime().ToString());
        AppendDataToFile("gazeData", "timestamp, Target Name, Target Location, Head Direction, Head Location");
        AppendDataToFile("handTracking", "timestamp, message");
    }
    private void OnEnable()
    {
        // Instruct Input System that we would like to receive all input events of type
        // IMixedRealitySourceStateHandler and IMixedRealityHandJointHandler
        CoreServices.InputSystem?.RegisterHandler<IMixedRealitySourceStateHandler>(this);
        CoreServices.InputSystem?.RegisterHandler<IMixedRealityHandJointHandler>(this);
    }

    private void OnDisable()
    {
        // This component is being destroyed
        // Instruct the Input System to disregard us for input event handling
        CoreServices.InputSystem?.UnregisterHandler<IMixedRealitySourceStateHandler>(this);
        CoreServices.InputSystem?.UnregisterHandler<IMixedRealityHandJointHandler>(this);
    }

    // IMixedRealitySourceStateHandler interface
    public void OnSourceDetected(SourceStateEventData eventData)
    {
        var hand = eventData.Controller as IMixedRealityHand;

        // Only react to articulated hand input sources
        if (hand != null)
        {
            Debug.Log("Source detected: " + hand.ControllerHandedness);
        }
    }

    public void OnSourceLost(SourceStateEventData eventData)
    {
        var hand = eventData.Controller as IMixedRealityHand;

        // Only react to articulated hand input sources
        if (hand != null)
        {
            Debug.Log("Source lost: " + hand.ControllerHandedness);
        }
    }

    // Update is called once per frame
    void Update()
    {
        AppendDataToFile("gazeData", GetLogGazeTargets());
        // try
        // {
        //     frames++;
        //     Debug.Log(getUnixTime());
        //     if (frames % 10 == 0)
        //     {
        //         Stack<EyeTracking.GazeTarget> gazeStack = eyeTracking.gazedObjects;
        //         int n = 3; // Amount of previous gaze targets to be logged
        //         n = Mathf.Min(n, gazeStack.Count);
        //         // Reads n most recent gaze targets from stack, and logs them
        //         for (int i = 0; i < n; i++)
        //         {
        //             EyeTracking.GazeTarget g = gazeStack.Pop();
        //             try
        //             {
        //                 string gText = g.gameObject.gameObject.GetComponent<ButtonConfigHelper>().MainLabelText;
        //                 string gazeTarget = string.Format("\tGaze Start:{0} Gaze Target:{1} Gaze Duration(In Frames): {2}", g.startTime, gText, g.duration);
        //                 AppendDataToFile("EyeGaze", gazeTarget);
        //             }
        //             catch (Exception e)
        //             {
        //                 Debug.Log("Could not find Gaze Target In Stack");
        //             }

        //         }
        //         //Clear stack, so gaze tracking can start again for next question
        //         eyeTracking.gazedObjects = new Stack<EyeTracking.GazeTarget>();
        //     }
        // }
        // catch (NullReferenceException e)
        // {
        //     Debug.Log(e);
        // }

    }
    public void AppendDataToFile(string filename, string data)
    {
        Debug.Log(string.Format("Appending: {0} to {1}", data, filename));
        string path = string.Format("{0}/TrackedData/{1}", Application.persistentDataPath, getUnixTime().ToString());
        //If file path does not already exists, create it
        if (!Directory.Exists(path))
        {

            Directory.CreateDirectory(path);
        }

        path = Path.Combine(path, filename + ".txt");
        //If File does not already exist, create it
        if (!File.Exists(path))
        {
            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine(filename + " At time " + starttime);
            }
        }

        //Write to file
        using (StreamWriter sw = File.AppendText(path))
        {
            sw.WriteLine(data);
        }
    }
    public void OnHandJointsUpdated(InputEventData<IDictionary<TrackedHandJoint, MixedRealityPose>> eventData)
    {
        MixedRealityPose fingerPose;
        if (eventData.InputData.TryGetValue(TrackedHandJoint.IndexTip, out fingerPose))

            if (eventData.Handedness == Handedness.Right)
            {
                Vector3 dist = fingerPose.Position - position;
                string message = Math.Round(dist.magnitude, 3) + "\n" + dist.ToString();
                AppendDataToFile("handTracking", getUnixTime().ToString() + "," + message);

            }

    }
    private string GetLogGazeTargets()
    {
        string payload = "";
        payload += getUnixTime().ToString();
        if (CoreServices.InputSystem.GazeProvider.GazeTarget)
        {
            payload += "," + CoreServices.InputSystem.GazeProvider.GazeTarget;
            payload += "," + CoreServices.InputSystem.GazeProvider.GazeTarget.transform.position;

        }
        else
        {
            payload += ",,";
        }
        payload += "," + CoreServices.InputSystem.GazeProvider.GazeDirection;
        payload += "," + CoreServices.InputSystem.GazeProvider.GazeOrigin;
        return payload;
    }
}