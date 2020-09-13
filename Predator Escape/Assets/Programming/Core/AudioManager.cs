using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PE.Core
{

    public class AudioManager : MonoBehaviour
    {
        [SerializeField] AudioSource[] music;

        private void Start()
        {
            SongVolume();

            FindObjectOfType<GameManager>().a_musicSettings += SongVolume;

        }

        public void MusicToPlay(int musicToPlay)
        {
            if (music[musicToPlay].isPlaying) return;

            StopMusic();

            for (int i = 0; i < music.Length; i++)
            {
                if (musicToPlay == i)
                {
                    music[i].GetComponent<AudioSource>().Play();
                }
            }
        }

        private void StopMusic()
        {
            for (int i = 0; i < music.Length; i++)
            {
                music[i].Stop();
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