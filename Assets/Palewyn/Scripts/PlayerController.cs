using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Palewyn
{
    public class PlayerController : MonoBehaviour
    {

        private CharacterController controller;
        public Animator anim;
        public float speed;
        // Use this for initialization
        void Start()
        {
            controller = GetComponent<CharacterController>();
            anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            float inputH = Input.GetAxis("Horizontal");
            float inputV = Input.GetAxis("Vertical");
            Move(inputH, inputV);

            
        }

        void Move(float inputH, float inputV)
        {
            Vector3 moveDir = new Vector3(inputH, 0f, inputV);
            controller.Move(moveDir * speed * Time.deltaTime);
            anim.SetFloat("MoveDirection", moveDir.z);
            anim.SetBool("isWalking", moveDir.magnitude > 0);
            
        }
    }
}