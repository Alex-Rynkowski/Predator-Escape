using PE.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PE.Core
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] int musicToPlay;

        float xPos;
        private void Start()
        {            
            
        }

        private void LateUpdate()
        {
            FindObjectOfType<AudioManager>().MusicToPlay(musicToPlay);

            xPos = gameObject.transform.position.x;
            xPos = FindObjectOfType<PlayerMovement>().GetComponent<Transform>().position.x;

            transform.position = new Vector2(xPos, 0);

        }
    }

}