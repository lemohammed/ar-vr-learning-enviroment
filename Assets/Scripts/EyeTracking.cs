using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using System;

public class EyeTracking : MonoBehaviour
{

    public GameObject lastObject;


    //Objects which are looked at by the user
    public struct GazeTarget
    {
        public GameObject gameObject;
        public string startTime;
        //Duration is framecount
        public int duration;
    }
    //Stack used to track most recent gaze objects
    public Stack<GazeTarget> gazedObjects;



    // Start is called before the first frame update
    void Start()
    {
        // Initialize the stack
        gazedObjects = new Stack<GazeTarget>();
    }


    // Update is called once per frame
    void Update()
    {
        try
        {
            GameObject CurrentGazeObject = CoreServices.InputSystem.GazeProvider.GazeTarget;
            
            //Hololens can track the room around it and register it as a game object, however we are not interested in this, so we filter it out
            if (CurrentGazeObject.name != null && !CurrentGazeObject.name.StartsWith("SpatialMesh"))
            {
                lastObject = CurrentGazeObject;
                //Add to stack
                if (gazedObjects.Count != 0)
                {
                    GazeTarget lastGaze = gazedObjects.Peek();
                    //If Current gazed gameObject is already on top of stack, then increase the duration instead of adding to stack 
                    if (lastGaze.gameObject.Equals(CurrentGazeObject))
                    {
                        GazeTarget currentGaze = gazedObjects.Pop();
                        currentGaze.duration += 1;   
                        gazedObjects.Push(currentGaze);
                    }
                    else //Add new gazetarget to stack
                    {
                        GazeTarget newGaze;
                        newGaze.gameObject = CurrentGazeObject;
                        newGaze.startTime = System.DateTime.Now.ToString();
                        newGaze.duration = 0;

                        gazedObjects.Push(newGaze);
                    }
                }
                else //Add new gazetarget to stack 
                {
                    GazeTarget newGaze;
                    newGaze.gameObject = CurrentGazeObject;
                    newGaze.startTime = System.DateTime.Now.ToString();
                    newGaze.duration = 0;

                    gazedObjects.Push(newGaze);
                }

            }
        }
        catch(NullReferenceException e)
        {
            //When the user is not looking at anything, a NullReferenceException occurs
        }
        
    }
}
