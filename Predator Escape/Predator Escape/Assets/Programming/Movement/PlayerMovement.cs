using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PE.Movement
{

    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 40f;
        [SerializeField] float jumpHeight = 10f;

        Rigidbody rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            Movement();
            ClickPosition();
        }
        private void Movement()
        {
            rb.velocity = new Vector2(moveSpeed, 0);
        }

        private void ClickPosition()
        {
            EventSystem events = GetComponent<EventSystem>();
            GraphicRaycaster[] rays = FindObjectsOfType<GraphicRaycaster>();
            PointerEventData pointer;

            if (Input.GetKey(KeyCode.Mouse0))
            {
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

                Jump();
            }
        }

        private void Jump()
        {
            rb.AddForce(Vector2.up * jumpHeight);
        }
    }

}