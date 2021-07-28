using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class GlobalInputRegister : MonoBehaviour,
    IMixedRealitySourceStateHandler, // Handle source detected and lost
    IMixedRealityHandJointHandler // handle joint position updates for hands
{
    private GameObject textObject;
    private TMP_Text text;
    private Vector3 position;
    private Vector3 average;
    private int handCount;

    public void UpdateText(string s)
    {
        string str = "Distance from cube: \n " + s + "\n handcount:" + handCount;
        if (text == null)
        {
            Debug.Log("TMP text component not found");
        }
        else
        {
            text.SetText(str);
            Debug.Log(position);
        }
    }

    public void Start()
    {
        textObject = this.gameObject.transform.GetChild(1).gameObject;
        text = textObject.GetComponent(typeof(TMP_Text)) as TMP_Text;
        position = transform.localPosition;
        handCount = 0;
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
            handCount ++;
            Debug.Log("Source detected: " + hand.ControllerHandedness);
        }
    }

    public void OnSourceLost(SourceStateEventData eventData)
    {
        var hand = eventData.Controller as IMixedRealityHand;

        // Only react to articulated hand input sources
        if (hand != null)
        {
            handCount--;
            Debug.Log("Source lost: " + hand.ControllerHandedness);
            UpdateText("Source lost");
        }
    }

    public void UpdateAverage(Vector3 dist)
    {
        average = dist;
    }

    public void OnHandJointsUpdated(InputEventData<IDictionary<TrackedHandJoint, MixedRealityPose>> eventData)
    {
        MixedRealityPose fingerPose;
        if (eventData.InputData.TryGetValue(TrackedHandJoint.IndexTip, out fingerPose))
        {
            if(eventData.Handedness == Handedness.Right)
            {
                Vector3 dist = fingerPose.Position - position;
                string message = Math.Round(dist.magnitude, 3) + "\n" + dist.ToString();
                UpdateText(message);
                UpdateAverage(dist);

            }
            
        }
    }
}