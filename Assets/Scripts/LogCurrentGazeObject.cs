using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit;


public class LogCurrentGazeObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // LogGazeTarget();
        Debug.Log("Gaze Target Script Running");
    }

    private void LogGazeTarget()
    {
        if (CoreServices.InputSystem.GazeProvider.GazeTarget)
        {
            Debug.Log("User gaze is currently over game object: " +
                CoreServices.InputSystem.GazeProvider.GazeTarget.transform.position);
            print("User gaze is over game object: " +
                CoreServices.InputSystem.GazeProvider.GazeTarget.transform.position);
            Debug.Log("Gaze is looking in direction: "
           + CoreServices.InputSystem.GazeProvider.GazeDirection);

            Debug.Log("Gaze origin is: "
                + CoreServices.InputSystem.GazeProvider.GazeOrigin);
        }
    }
    // Update is called once per frame
    void Update()
    {

        LogGazeTarget();
    }
}


