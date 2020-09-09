using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PE.Movement
{

    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 40f;
        [SerializeField] float jumpHeight = 10f;
        [SerializeField] float speedBoost = 10f;
        [SerializeField] float fallSpeed = 1;
        
        Rigidbody rb;
        bool grounded = true;
        float currentYPos;


        private void Start()
        {
            rb = GetComponent<Rigidbody>();

        }

        private void Update()
        {

        }

        private void FixedUpdate()
        {
           // Movement();
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Jump();
                //ClickPosition();
            }
        }
        private void Movement()
        {
            rb.velocity = new Vector2(moveSpeed, transform.position.y);
        }

        private void ClickPosition()
        {
            EventSystem events = GetComponent<EventSystem>();
            GraphicRaycaster[] rays = FindObjectsOfType<GraphicRaycaster>();
            PointerEventData pointer;

                pointer = new PointerEventData(events);
                pointer.position = Input.mousePosition;

                List<RaycastResult> results = new List<RaycastResult>();

                foreach (GraphicRaycaster ray in rays)
                {
                    ray.Raycast(pointer, results);
                    foreach (RaycastResult result in results)
                    {
                        return;
                    }
                }

                if (!grounded) return;
                Jump();
        }

        private void FallSpeed()
        {
            rb.AddForce(Vector2.down * fallSpeed);
        }

        private void Jump()
        {
            rb.AddForce(Vector2.up * jumpHeight);
            //grounded = false;
        }

        public float MoveSpeed(bool boostUsed)
        {
            if (boostUsed)
            {
                return moveSpeed += speedBoost;
            }
            else
            {
                return moveSpeed -= speedBoost;
            }            
        }
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag == "Ground")
            {                
                grounded = true;
            }
        }
    }

    
    

}