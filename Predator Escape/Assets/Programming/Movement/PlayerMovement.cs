using System;
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
        [Tooltip("How should the first jump be?")]
        [SerializeField] float jumpForceFirst = 10f;
        [Tooltip("How should the second jump be?")]
        [SerializeField] float jumpForceSecond = 15f;

        Rigidbody2D rb;
        bool canJumpAgain = false;

        EventSystem events;
        GraphicRaycaster[] rays;
        PointerEventData pointer;

        BoxCollider2D collider2D;
        LayerMask groundLayer;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();

            rays = FindObjectsOfType<GraphicRaycaster>();
            events = GetComponent<EventSystem>();

            collider2D = GetComponent<BoxCollider2D>();
            groundLayer = LayerMask.GetMask("Ground");

        }

        private void Update()
        {
            MoveRight();
            if (Input.GetKeyUp(jumpKey) && IsGrounded() && CanJump())
            {
                FirstJump();
                return;
            }
            if(Input.GetKeyUp(jumpKey) && canJumpAgain)
            {
                SecondJump();
            }
            
        }

        private void FirstJump()
        {            
            rb.velocity = Vector2.up * jumpForceFirst;
            canJumpAgain = true;
        }

        private void SecondJump()
        {
            rb.velocity = Vector2.up * jumpForceSecond;
            canJumpAgain = false;
                       
        }

        #region Move function
        private void MoveRight()
        {
            rb.velocity = new Vector3(moveSpeed, rb.velocity.y);
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


        #region booleans
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
            return true;
        }

        private bool IsGrounded()
        {
            RaycastHit2D hit = Physics2D.BoxCast(collider2D.bounds.center, collider2D.bounds.size, 0f, Vector2.down, .1f, groundLayer);
            Color rayColor;
            if (hit.collider != null)
            {
                rayColor = Color.green;
            }
            else
            {
                rayColor = Color.red;
            }
            Debug.DrawRay(collider2D.bounds.center + new Vector3(collider2D.bounds.extents.x, 0), Vector2.down * (collider2D.bounds.extents.y + .1f), rayColor);
            Debug.DrawRay(collider2D.bounds.center - new Vector3(collider2D.bounds.extents.x, 0), Vector2.down * (collider2D.bounds.extents.y + .1f), rayColor);
            Debug.DrawRay(collider2D.bounds.center - new Vector3(collider2D.bounds.extents.x, collider2D.bounds.extents.y), Vector2.right, rayColor);
            return hit.collider != null;
        }
        #endregion
    }
}