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
        Transform playerTransform;
        private void Start()
        {
            FindObjectOfType<AudioManager>().MusicToPlay(musicToPlay);
            playerTransform = FindObjectOfType<PlayerMovement>().GetComponent<Transform>();
        }

        private void LateUpdate()
        {
            xPos = playerTransform.position.x;
            transform.position = new Vector2(xPos, 0);
        }
    }

}