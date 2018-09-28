using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Palewyn
{
    public class CameraOrbit : MonoBehaviour
    {


        #region Vars
        public Transform target;//target to orbit around
        public bool hideCursor = true; //is the curser hidden?
        public bool detatchFromParent = false;
        [Header("Orbit")]
        public Vector3 offset = new Vector3(0, 0, 0); //offset for the camera
        public float xSpeed = 120f; //x orbiting speed
        public float ySpeed = 120f; //y orbiting speed
        public float yMinLimit = -20f; //limiting y movement (CLAMPS)
        public float yMaxLimit = 80f;
        public float distanceMin = 0.5f;//min distance to the target (especially if camera is colliding with a wall)
        public float distanceMax = 15f; //max distance to the target

        [Header("Collision")]
        public bool cameraCollision = true; //is camera collision enabled
        public float cameraRadius = 0.3f; //radius of camera collison
        public LayerMask ignoreLayers; //Layers ignored by collision

        private Vector3 originalOffset; //original offset at start of game
        private float distance; //current distance to the camera
        private float rayDistance = 1000f; //max distance the ray can shoot to check collision
        private float x = 0f;//x degrees of rotation
        private float y = 0f;//y degrees of rotation
        #endregion
        // Use this for initialization
        void Start()
        {

            //Detatch camera from parent
            if (detatchFromParent)
            {
                transform.SetParent(null);
            }

            //Set target
            //target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

            //is the cureser supposed to be hidden?
            if (hideCursor) //if hideCursor == true
            {
                //Lock to center of the screen
                Cursor.lockState = CursorLockMode.Locked;

                //..Hide the curser
                Cursor.visible = false;
            }

            //Calculate original offset from target position
            originalOffset = transform.position - target.position;

            //Set ray distance to current distance magnitude of the camera
            rayDistance = originalOffset.magnitude;

            //get camera rotation
            Vector3 angles = transform.eulerAngles;

            //set X and Y values to current camera rotation
            x = angles.y; //horizontal rotation revolves around Y-axis
            y = angles.x; //Vertical rotation revolves around X-axis
        }


        #region UpdateFunctions
        // Update is called once per frame
        void Update()
        {

            //if target has been set
            if (target)
            {
                //rotate the camera based on Mouse X and Y inputs
                x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
                y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime; //using -= here to invert the Y

                //clamp the angle by using custom ClampAngle function
                y = ClampAngle(y, yMinLimit, yMaxLimit);

                //Rotate the transform using Euler angles (y for X and x for Y)
                transform.rotation = Quaternion.Euler(y, x, 0f);

            }
        }

        private void FixedUpdate()
        {
            //if the target has been set
            if (target)
            {
                //is camera collision enabled
                if (cameraCollision)
                {
                    //create a ray starting from target position
                    Ray camRay = new Ray(target.position, -transform.forward);//send ray from target in direction of backwards relative to the camera
                    RaycastHit hit;
                    //shoot a sphere in defined ray direction
                    if (Physics.SphereCast(camRay, cameraRadius, out hit, rayDistance, ~ignoreLayers, QueryTriggerInteraction.Ignore))
                    {
                        //set current cam distance to hit objects distance
                        distance = hit.distance;
                        //exit the function
                        return;
                    }
                }

                //Set distance to original distance
                distance = originalOffset.magnitude;
            }
        }

        private void LateUpdate()
        {
            if (target)
            {
                //calculate the local offset from offset
                Vector3 localOffset = transform.TransformDirection(offset);

                //reposition the camera based off distance and offset
                transform.position = (target.position + localOffset) + -transform.forward * distance;
            }
        }

        #endregion

        //clamps angle between -360 and 360 degrees using the min and max angle
        public static float ClampAngle(float angle, float minClamp, float maxClamp)
        {
            if (angle < -360f)
            {
                angle += 360f;
            }

            if (angle > 360f)
            {
                angle -= 360f;
            }
            return Mathf.Clamp(angle, minClamp, maxClamp);
        }
    }
}
