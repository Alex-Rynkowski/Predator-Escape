using PE.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] int playMusic = 0;

    private void Start()
    {
        FindObjectOfType<AudioManager>().PlayMusic(playMusic);
    }

}
