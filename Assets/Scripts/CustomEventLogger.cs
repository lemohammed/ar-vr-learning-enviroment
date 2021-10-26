using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Debug = UnityEngine.Debug;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine.UI;
using TMPro;
using System;
using System.Diagnostics;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using static file.logger.Logger;



namespace events.logger
{
    
public class CustomEventLogger : MonoBehaviour, IMixedRealitySourceStateHandler, IMixedRealityHandJointHandler
{
    //Lesson list containing
    private static System.Random rnd = new System.Random();

    public int lessonID = rnd.Next(10000000);
    public GameObject Button1;
    public GameObject Button2;
    static private string startTime = getUnixTime().ToString();
    static public int startTimeInt=getUnixTime();
    static public int currentTimerTime;
    //Start time, used to create Output folder on the hololens
    private Vector3 position;
    [SerializeField] TextMeshPro m_Object;
    [SerializeField] TextMeshPro timer;


   
    // Start is called before the first frame update
    void Start()
    {
        string[] headers = {"currentTimestamp", "LessonId", "Target Name", "Target Location", "Head Direction", "Head Position"};
        string GAZE_DATA_HEADER_ROW = string.Join(delimeter.ToString(),headers);
        ResetTimer();
        CreateFile(START_UP_LOGS, "Start up successfull at," + getUnixTime().ToString());
        CreateFile(GAZE_DATA, GAZE_DATA_HEADER_ROW);
        CreateFile(HAND_TRACKING, "timestamp, message");
        CreateFile(TASK_LOGS, "Time-Task-Time Taken-Tries");
    }

    public void EndTask(){
        AppendDataToFile(TASK_LOGS, string.Format("{0}-{1}",getUnixTime().ToString(),currentTimerTime.ToString()));
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

    public static void ResetTimer(){
        startTimeInt = getUnixTime();
    }

    // Update is called once per frame
    void Update()
    {
            // ResetTimer();
            currentTimerTime =(getUnixTime()-startTimeInt);
            timer.text = currentTimerTime.ToString();
            string gazeTargetLog = GetGazeTargetLog();
            if(gazeTargetLog!=""){
                AppendDataToFile("gazeData", gazeTargetLog);
            }
    }
    // void LogEyeTracking(){
    //     try
    //     {
    //         frames++;
    //         Debug.Log(getUnixTime());
    //         if (frames % 10 == 0)
    //         {
    //             Stack<EyeTracking.GazeTarget> gazeStack = eyeTracking.gazedObjects;
    //             int n = 3; // Amount of previous gaze targets to be logged
    //             n = Mathf.Min(n, gazeStack.Count);
    //             // Reads n most recent gaze targets from stack, and logs them
    //             for (int i = 0; i < n; i++)
    //             {
    //                 EyeTracking.GazeTarget g = gazeStack.Pop();
    //                 try
    //                 {
    //                     string gText = g.gameObject.gameObject.GetComponent<ButtonConfigHelper>().MainLabelText;
    //                     string gazeTarget = string.Format("\tGaze Start:{0} Gaze Target:{1} Gaze Duration(In Frames): {2}", g.startTime, gText, g.duration);
    //                     AppendDataToFile("EyeGaze", gazeTarget);
    //                 }
    //                 catch (Exception e)
    //                 {
    //                     Debug.Log("Could not find Gaze Target In Stack");
    //                 }

    //             }
    //             //Clear stack, so gaze tracking can start again for next question
    //             eyeTracking.gazedObjects = new Stack<EyeTracking.GazeTarget>();
    //         }
    //     }
    //     catch (NullReferenceException e)
    //     {
    //         Debug.Log(e);
    //     }
    // }
   
    public void CreateFile(string filename, string initialData){
        string path = string.Format("{0}/TrackedData/{1}/{2}", Application.persistentDataPath,lessonID,startTime);
        path = Path.Combine(path, filename + ".txt");
        //If File does not already exist, create it
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        if (!File.Exists(path))
        {
            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine(initialData);
            }
        }
    }
  
    public void OnHandJointsUpdated(InputEventData<IDictionary<TrackedHandJoint, MixedRealityPose>> eventData)
    {
        MixedRealityPose fingerPose;
        if (eventData.InputData.TryGetValue(TrackedHandJoint.IndexTip, out fingerPose)){
            Vector3 dist = fingerPose.Position - position;
            string message = Math.Round(dist.magnitude, 3) + "\n" + dist.ToString();
            AppendDataToFile("handTracking", getUnixTime().ToString() + "," + message);
        }
    }
    private string GetGazeTargetLog()
    {
        string payload = "";
        if (CoreServices.InputSystem.GazeProvider.GazeTarget)
        {
            payload += delimeter + CoreServices.InputSystem.GazeProvider.GazeTarget.ToString();
            payload += delimeter + CoreServices.InputSystem.GazeProvider.GazeTarget.transform.position.ToString();

        }
        else
        {
            payload += delimeter + "No Target";
            payload += delimeter + "No Target Position";
        }
        payload += getUnixTime().ToString();
        payload += delimeter+lessonID.ToString();

        payload += delimeter + CoreServices.InputSystem.GazeProvider.GazeDirection.ToString();
        payload += delimeter + CoreServices.InputSystem.GazeProvider.GazeOrigin.ToString();
        return payload;
    }
  public void TaskOnClick()
    {
        m_Object.text = "Success";
        Button1.SetActive(false);
        Button2.SetActive(true);
        //Output this to console when Button1 or Button3 is clicked
        Debug.Log("You have clicked the button!");
    }
}

}

