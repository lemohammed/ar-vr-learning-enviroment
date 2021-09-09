using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task2 : MonoBehaviour
{
    private int clickCount =0;
    public GameObject Task2Object;
    public GameObject Task3Object;
    public GameObject LocalVideoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TaskOnClick(){
        clickCount++;
        if(clickCount>=2){
            Task2Object.SetActive(false);
            Task3Object.SetActive(true);
            GameObject.Find("LocalVideoPlayer").SetActive(false);
        }
    }
}
