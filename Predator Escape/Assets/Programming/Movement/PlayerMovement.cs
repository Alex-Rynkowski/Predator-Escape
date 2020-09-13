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
        [Header("Movement")]
        [SerializeField] float moveSpeed = 40f;
        [SerializeField] float speedBoost = 10f;

        [Header("Jump")]
        [SerializeField] KeyCode jumpKey;
        [SerializeField] float jumpForce = 10f;
        [Tooltip("Controls max height of the jump, (depending on how long the user holds the jump button)")]
        [SerializeField] float jumpTimeCounter = 1;
        [SerializeField] float fallSpeed = 20;

        Rigidbody2D rb;
        bool isGrounded = true;
        bool stoppedJumping = true;
        float jumpTimer;

        RaycastHit2D hit;
        EventSystem events;
        GraphicRaycaster[] rays;
        PointerEventData pointer;
        LayerMask groundLayer;
        BoxCollider2D col;

        private void Start()
        {
            col = GetComponent<BoxCollider2D>();
            Physics2D.queriesStartInColliders = false;
            rb = GetComponent<Rigidbody2D>();
            jumpTimer = jumpTimeCounter;

            groundLayer = LayerMask.GetMask("Ground");

            events = GetComponent<EventSystem>();
            rays = FindObjectsOfType<GraphicRaycaster>();

            hit = Physics2D.Raycast(transform.position, Vector2.down, 100, groundLayer);

        }

        void Update()
        {
            if(IsGrounded()){
                isGrounded = true;
            }
        }

        

        private bool IsGrounded(){
            hit = Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f, Vector2.down, groundLayer);
            Color rayColor;
            if(hit.collider != null){
                rayColor = Color.green;
            }else{
                rayColor = Color.red;
            }

            Debug.DrawRay(col.bounds.center + new Vector3(col.bounds.extents.x, 0), Vector2.down * (col.bounds.extents.y + .1f), rayColor);
            Debug.DrawRay(col.bounds.center - new Vector3(col.bounds.extents.x, 0), Vector2.down * (col.bounds.extents.y + .1f), rayColor);
            Debug.DrawRay(col.bounds.center - new Vector3(col.bounds.extents.x, col.bounds.extents.y), Vector2.right * (col.bounds.extents.x), rayColor);
            print("hmmm");
            return hit.collider != null;

        }

        private void FixedUpdate()
        {
            Movement();
            if (!CanJump()) return;
            Jump();
        }

        #region Move function
        private void Movement()
        {
            if (!isGrounded) return;
            rb.velocity = Vector2.right * moveSpeed;
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
        #endregion

        #region Jump Function
        private void Jump()
        {
            if (Input.GetKey(jumpKey) && isGrounded)
                stoppedJumping = false;
            if (Input.GetKey(jumpKey) && !stoppedJumping)
                JumpHeightController();
            if (Input.GetKeyUp(jumpKey))
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
            print("stop jump!");
            rb.AddForce(Vector2.down * fallSpeed);
            //rb.velocity = new Vector2(rb.velocity.x, -fallSpeed);
            jumpTimer = jumpTimeCounter;
            stoppedJumping = true;
            isGrounded = false;
        }

        private bool CanJump()
        {
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
        #endregion



        private void OnCollisionEnter2D(Collision2D other)
        {
            // if(isGrounded) return;
            // if (other.gameObject.tag == "Ground")
            // {
            //     isGrounded = true;
            //     print("yes!");
            // }
        }


    }
}