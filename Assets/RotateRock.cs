using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // StartRotation();
        
    }

    // Update is called once per frame
    void Update()
    {
        objectToRotate.transform.Rotate(0,0,20*Time.deltaTime);
        
    }
    public GameObject objectToRotate;
    private bool rotating ;
    
    private IEnumerator Rotate( Vector3 angles, float duration )
    {
        rotating = true ;
        Quaternion startRotation = objectToRotate.transform.rotation ;
        Quaternion endRotation = Quaternion.Euler( angles ) * startRotation ;
        for( float t = 0 ; t < duration ; t+= Time.deltaTime )
        {
            objectToRotate.transform.rotation = Quaternion.Lerp( startRotation, endRotation, t / duration ) ;
            yield return null;
        }
        objectToRotate.transform.rotation = endRotation  ;
        rotating = false;
    }
    
    public void StartRotation()
    {
        if( !rotating )
            StartCoroutine( Rotate( new Vector3(0, 90, 0), 1 ) ) ;
    }
}


