using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwap : MonoBehaviour
{
    #region Variables

    public Transform[] lookObject; //array of objects to look at
    public bool smooth = true;//is smooth enabled?
    public float damping = 6f;//smoothness of camera
    [Header("GUI")]
    public float scrW; //used for screen width
    public float scrH; //used for screen height

    private int lookIndex; //index of look array
    private int lookMax; //max index of look array
    private Transform target; //current target of look object

    #endregion

    // Use this for initialization
    void Start()
    {
        //last index of array becomes lookMax
        lookMax = lookObject.Length - 1;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        //get current object to look at
        target = lookObject[lookIndex];

        //if target is not null
        if (target)
        {
            //if smoothing enabled?
            if (smooth)
            {
                //calculate direction to look at target
                Vector3 lookDirection = target.position - transform.position;
                Quaternion rotation = Quaternion.LookRotation(lookDirection);

                //look at and dampen the rotation
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
            }
            else
            {
                //look at target without smooth and dmapen
                transform.LookAt(target);
            }
        }
        else
        {
            //keep swapping until valid target is found
            CamSwap();
        }
    }

    void CamSwap()
    {

        lookIndex++;
        //if exceeds array index max
        if (lookIndex > lookMax)
        {
            //set back to 0 to cycle
            lookIndex = 0;
        }
    }
       

    private void OnGUI()
    {
        if(scrW != Screen.width / 16 || scrH != Screen.height / 9)
        {
            scrW = Screen.width / 16;
            scrH = Screen.height / 9;
        }

        //the button in this IF statement will always display but will enter the if condition if it is pressed!
        if(GUI.Button(new Rect(scrW * 0.5f, scrH * 0.25f, scrW * 1.5f, scrH * 0.75f), "swap"))
        {
            //swap the camera
            CamSwap();
        }
    }
}
