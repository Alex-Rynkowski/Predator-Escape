using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PE.Core
{

    public class AudioManager : MonoBehaviour
    {
        [SerializeField] AudioSource[] music;

        AudioManager instance;

        private void Awake()
        {
            instance = this;
            if (instance != null) return;

            DontDestroyOnLoad(gameObject);
        }
        private void Start()
        {
            SongVolume();
            FindObjectOfType<GameManager>().a_musicSettings += SongVolume;
        }
        public void MusicToPlay(int musicToPlay)
        {
            for (int i = 0; i < music.Length; i++)
            {
                music[i].Stop();
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

}