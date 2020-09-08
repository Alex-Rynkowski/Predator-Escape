using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace PE.Core
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] AudioSource[] music;
                
        private void Start()
        {
            FindObjectOfType<GameManager>().a_musicSettings += SongVolume;
        }
        public void PlayMusic(int musicToPlay)
        {
            for (int i = 0; i < music.Length; i++)
            {                

                music[i].GetComponent<AudioSource>().volume = FindObjectOfType<GameManager>().musicVolumeSet;
                if(musicToPlay == i)
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

}