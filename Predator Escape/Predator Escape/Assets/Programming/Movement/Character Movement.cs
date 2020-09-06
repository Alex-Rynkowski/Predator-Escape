using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    public CharacterController controller;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;

    void Update()
    {

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;  // Left and Right ( A & D )

        if (Input.GetButtonDown("Space")) // implementing Double Jump?
        {
            jump = true;
        }

    }
    void FixedUpdate()
    {
       
        controller.Move(horizontalMove * Time.deltaTime, jump);    // Move the Character
        jump = false;
    }
}
