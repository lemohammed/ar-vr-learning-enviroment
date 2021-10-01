using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Debug = UnityEngine.Debug;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Diagnostics;
namespace file.logger { 
    class Logger { 
    public static string TASK_LOGS ="Task Logs";
    static public char delimeter = '/';
    public static string HAND_TRACKING = "handTracking";
    public static string GAZE_DATA ="gazeData";
    public static string START_UP_LOGS = "StartUpLogs";
    private static int lessonID = 1;
    static private string startTime = getUnixTime().ToString();
     static public int getUnixTime()
    {
        System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
        int cur_time = (int)(System.DateTime.UtcNow - epochStart).TotalSeconds;
        return cur_time;
    }
        public static void AppendDataToFile(string filename, string data)
    {
        Debug.Log(string.Format("Appending: {0} to {1}", data, filename));
        string path = string.Format("{0}/TrackedData/{1}/{2}", Application.persistentDataPath,lessonID,startTime);
        //If file path does not already exists, create it
        
        //Write to file
        using (StreamWriter sw = File.AppendText(path))
        {
            sw.WriteLine(data);
        }
    }
     public static void CreateFile(string filename, string initialData){
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
    }
}

namespace DemoNamespace
{
    class DemoClass { 
        //
    }
}