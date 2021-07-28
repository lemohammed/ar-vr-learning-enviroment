// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Mono.Data.Sqlite;
// using System.Data;
// using System;
// using TMPro;
// using Microsoft.MixedReality.Toolkit;
// using Microsoft.MixedReality.Toolkit.Input;
// using Microsoft.MixedReality.Toolkit.Utilities;

// public class Test : MonoBehaviour
// {
//     public struct GazeTarget
//     {
//         public GameObject gameObject;
//         public string startTime;
//         //Duration is framecount
//         public int duration;
//     }
//     //Stack used to track most recent gaze objects
//     public Stack<GazeTarget> gazedObjects;

//     public GameObject lastObject;


//     int getUnixTime()
//     {
//         System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
//         int cur_time = (int)(System.DateTime.UtcNow - epochStart).TotalSeconds;
//         return cur_time;
//     }
//     // Start is called before the first frame update
//     void Start()
//     {
//     string conn = "URI=file:" + Application.dataPath + "/arvrResearch.db;MultipleActiveResultSets=True;"; //Path to database.
//         IDbConnection dbconn = (IDbConnection)new SqliteConnection(conn);
//         dbconn.Open(); //Open connection to the database.
//         Debug.Log(dbconn);

//         dbconn.Close();

//         Debug.Log(getUnixTime());
//     }


//     void getGameObjectGaze()
//     {
//         GameObject CurrentGazeObject = CoreServices.InputSystem.GazeProvider.GazeTarget;
//         if (CurrentGazeObject.name != null && !CurrentGazeObject.name.StartsWith("SpatialMesh"))
//         {
//             lastObject = CurrentGazeObject;
//             //Add to stack
//             if (gazedObjects.Count != 0)
//             {
//                 GazeTarget lastGaze = gazedObjects.Peek();
//                 //If Current gazed gameObject is already on top of stack, then increase the duration instead of adding to stack 
//                 if (lastGaze.gameObject.Equals(CurrentGazeObject))
//                 {
//                     GazeTarget currentGaze = gazedObjects.Pop();
//                     currentGaze.duration += 1;
//                     gazedObjects.Push(currentGaze);
//                 }
//                 else //Add new gazetarget to stack
//                 {
//                     GazeTarget newGaze;
//                     newGaze.gameObject = CurrentGazeObject;
//                     newGaze.startTime = System.DateTime.Now.ToString();
//                     newGaze.duration = 0;

//                     gazedObjects.Push(newGaze);
//                 }

//             }
//             Debug.Log("Test debug log");
//         }


//         // Update is called once per frame
//         void Update()
//         {

//         }
//         void OnMouseDown()
//         {
//             Destroy(gameObject);
//         }
//     }
// }