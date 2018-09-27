using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSurveillance : MonoBehaviour {

    public Camera[] cameras;//array of cams to switch between
    public KeyCode prevKey = KeyCode.Q; //filter back to previous camera
    public KeyCode nextKey = KeyCode.E; //filter forward to next camera
    private int camIndex; //current cam index
    private int camMax; //max amount of camera we can store
    private Camera current; //current camera

	// Use this for initialization
	void Start () {
        
        //get all camera children and store in array
        cameras = GetComponentsInChildren<Camera>();

        //Last index of array is array.length - 1
        camMax = cameras.Length - 1;

        //activate the default camera
        ActivateCamera(camIndex);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(nextKey))
        {
            //increase camIndex
            camIndex++;
            if(camIndex > camMax)
            {
                camIndex = 0;
            }
            ActivateCamera(camIndex);
        }

        if (Input.GetKeyDown(prevKey))
        {
            camIndex--;
            if(camIndex < 0)
            {
                camIndex = camMax;
            }
            ActivateCamera(camIndex);
        }

	}

    void ActivateCamera(int camIndex)
    {
        //loop through all surveilance cams
        for(int i = 0; i < cameras.Length; i++)
        {
            Camera cam = cameras[i];

            //if the current index is equal to given index 'camIndex'
            if(i == camIndex)
            {
                //enable this camera
                cam.gameObject.SetActive(true);
            }
            else
            {
                //disable camera if it is not what we want
                cam.gameObject.SetActive(false);
            }
        }
    }
}
