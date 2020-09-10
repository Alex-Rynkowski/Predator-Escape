using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10;

    Rigidbody2D rb;    
    float obstHeight;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        LookForObstacles();
    }
    private void MoveSpeed()
    {
        rb.velocity = new Vector2(moveSpeed, 0);
    }

    private void LookForObstacles()
    {
        float rayDist = 5f;
        Physics2D.queriesStartInColliders = false;
        Vector3 right = transform.TransformDirection(Vector3.right) * rayDist;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, right);
                
        Debug.DrawRay(transform.position, right, Color.red);
        if(hit.collider != null)
        {
            if(hit.distance < rayDist)
            {
                obstHeight = hit.transform.GetComponent<Collider2D>().transform.position.y;
                print(obstHeight);
                rb.velocity = new Vector2(0, obstHeight);
            }
        }               
    }
}
