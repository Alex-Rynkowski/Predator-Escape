using System;
using System.Collections;
using System.Collections.Generic;
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

        [Tooltip("Controls max height of the jump, (depending on how long the user holds the jump button)")]
        [SerializeField] float jumpTimeCounter = 1;

        [SerializeField] float fallSpeed = 20;

        Rigidbody2D rb;
        bool isGrounded = true;
        bool stoppedJumping = true;
        float jumpTimer;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
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
                rb.AddForce(Vector2.up * jumpForce );
                jumpTimer -= Time.deltaTime;
            }
            else
            {
                StopJumpHeight();
            }
        }
        private void StopJumpHeight()
        {
            print("stop jump!");
            rb.velocity = new Vector2(rb.velocity.x, -fallSpeed * Time.fixedDeltaTime);            
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
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "Ground")
            {                
                print("hello?");
                isGrounded = true;
            }
        }
    }
}