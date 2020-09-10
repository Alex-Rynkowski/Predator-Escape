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
        [SerializeField] float speedBoost = 10f;
        [SerializeField] float jumpForce = 10f;        
        [SerializeField] float jumpTimeCounter = 1;
        [SerializeField] float fallSpeed = 20;

        Rigidbody rb;
        bool isGrounded = true;
        bool stoppedJumping = true;
        float jumpTimer;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            jumpTimer = jumpTimeCounter;
        }

        private void Update()
        {
            Movement();

            if (!CanJump()) return;
            Jump();
        }

        private void Movement()
        {
            if (!isGrounded) return;
            rb.velocity = Vector2.right * moveSpeed;
        }

        private void Jump()
        {    
            if (Input.GetKey(KeyCode.Mouse0) && isGrounded) 
                stoppedJumping = false;
            if (Input.GetKey(KeyCode.Mouse0) && !stoppedJumping) 
                JumpHeightController();
            if (Input.GetKeyUp(KeyCode.Mouse0)) 
                StopJumpHeight();
        }

        private void JumpHeightController()
        {
            if (jumpTimer > 0)
            {                
                rb.AddForce(Vector2.up * jumpForce);
                jumpTimer -= Time.deltaTime;
            }
            else
            {
                StopJumpHeight();
            }
        }
        private void StopJumpHeight()
        {
            rb.velocity = new Vector2(rb.velocity.x, -fallSpeed);
            //rb.AddForce(Vector3.down * fallSpeed);
            jumpTimer = jumpTimeCounter;
            stoppedJumping = true;
            isGrounded = false;
        }

        private bool CanJump()
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
                    return false;
                }
            }
            if (!isGrounded) return false;
            return true;        
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
                isGrounded = true;
            }
        }
    }
}