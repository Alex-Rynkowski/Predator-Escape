using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PE.Movement
{

    public class PlayerMovement : MonoBehaviour
    {
        #region variables
        [Header("Movement")]
        [SerializeField] float moveSpeed = 40f;
        [Tooltip("50% of move speed (sets automatically upon starting the game)")]
        [SerializeField] float speedBoost = 10f;

        [Header("Jump")]
        [SerializeField] KeyCode jumpKey;
        [Tooltip("How high should the first jump be?")]
        [SerializeField] float jumpForceFirst = 10f;
        [Tooltip("How high should the second jump be?")]
        [SerializeField] float jumpForceSecond = 15f;

        [SerializeField] ParticleSystem jumpBurstParticles;
        Rigidbody2D rb;
        bool canJumpAgain = false;

        EventSystem events;
        GraphicRaycaster[] rays;
        PointerEventData pointer;

        BoxCollider2D collider;
        LayerMask groundLayer;

        #endregion

        #region start and update
        private void Start()
        {
            speedBoost = moveSpeed * .5f;
            rb = GetComponent<Rigidbody2D>();

            rays = FindObjectsOfType<GraphicRaycaster>();
            events = GetComponent<EventSystem>();

            collider = GetComponent<BoxCollider2D>();
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
            if (Input.GetKeyUp(jumpKey) && canJumpAgain)
            {
                SecondJump();
            }
        }
        #endregion

        #region jump functions
        private void FirstJump()
        {
            rb.velocity = Vector2.up * jumpForceFirst;
            var particles = GameObject.Instantiate(jumpBurstParticles, transform.position, transform.rotation);
            Destroy(particles.gameObject, 1);
            canJumpAgain = true;
        }

        private void SecondJump()
        {
            var burst = GameObject.Instantiate(jumpBurstParticles, transform.position, transform.rotation);
            rb.velocity = Vector2.up * jumpForceSecond;
            Destroy(burst.gameObject, 1);
            canJumpAgain = false;
        }
        #endregion

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

        public bool IsGrounded()
        {
            RaycastHit2D hit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, .1f, groundLayer);
            Color rayColor;
            if (hit.collider != null)
            {
                rayColor = Color.green;
            }
            else
            {
                rayColor = Color.red;
            }
            Debug.DrawRay(collider.bounds.center + new Vector3(collider.bounds.extents.x, 0), Vector2.down * (collider.bounds.extents.y + .1f), rayColor);
            Debug.DrawRay(collider.bounds.center - new Vector3(collider.bounds.extents.x, 0), Vector2.down * (collider.bounds.extents.y + .1f), rayColor);
            Debug.DrawRay(collider.bounds.center - new Vector3(collider.bounds.extents.x, collider.bounds.extents.y), Vector2.right, rayColor);
            return hit.collider != null;
        }
        #endregion
    }
}