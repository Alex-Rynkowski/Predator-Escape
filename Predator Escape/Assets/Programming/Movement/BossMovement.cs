using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PE.Movement
{
    public class BossMovement : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 10;
        [SerializeField] float rayDistance = 5;

        Rigidbody2D rb;
        float obstHeight;

        bool isGrounded;
        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        private void Update()
        {            
            

            if (!isGrounded) return;
            MoveSpeed();
        }
        private void LateUpdate()
        {
            LookForObstacles();
        }
        private void MoveSpeed()
        {
            if (!isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, -40);
            }
            else
            {
                rb.velocity = new Vector2(moveSpeed, 0);
            }
            
        }

        private void LookForObstacles()
        {            
            Physics2D.queriesStartInColliders = false;
            Vector3 right = transform.TransformDirection(Vector3.right) * rayDistance;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, right);

            Debug.DrawRay(transform.position, right, Color.red);
            if (hit.collider != null)
            {
                if (hit.distance < rayDistance)
                {
                    obstHeight = hit.transform.GetComponent<Collider2D>().transform.position.y;
                    print(obstHeight);
                    rb.AddForce(Vector2.up * obstHeight * 200);

                    //rb.velocity = new Vector2(0, obstHeight + 5);
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if(other.gameObject.tag == "Ground")
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
        }
    }
}
