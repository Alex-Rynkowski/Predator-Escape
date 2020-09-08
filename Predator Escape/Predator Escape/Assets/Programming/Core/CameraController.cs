using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] int musicToPlay;
    private void Start()
    {
        FindObjectOfType<AudioManager>().MusicToPlay(musicToPlay);
    }

}
