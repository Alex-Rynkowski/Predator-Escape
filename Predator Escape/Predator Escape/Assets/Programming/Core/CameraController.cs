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
            FindObjectOfType<AudioManager>().MusicToPlay(musicToPlay);
        }
    }

}