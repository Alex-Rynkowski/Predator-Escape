using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PE.Core
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] int musicToPlay;
        private void Start()
        {            
        }

        private void LateUpdate()
        {
            FindObjectOfType<AudioManager>().MusicToPlay(musicToPlay);

        }
    }

}