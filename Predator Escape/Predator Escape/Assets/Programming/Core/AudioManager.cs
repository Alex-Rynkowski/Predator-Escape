using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource[] music;

    private void Start()
    {
        //FindObjectOfType<GameManager>().a_musicSettings += SongVolume;
    }
    public void MusicToPlay(int musicToPlay)
    {
        for (int i = 0; i < music.Length; i++)
        {
            if (musicToPlay == i)
            {
                music[i].GetComponent<AudioSource>().Play();
            }
        }
    }


    void SongVolume()
    {
        for (int i = 0; i < music.Length; i++)
        {
            music[i].GetComponent<AudioSource>().volume = FindObjectOfType<GameManager>().musicVolumeSet;
        }
    }

}
