using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace PE.Core
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] AudioSource[] music;

        public void PlayMusic(int musicToPlay)
        {
            for (int i = 0; i < music.Length; i++)
            {
                if(musicToPlay == i)
                {
                    music[i].GetComponent<AudioSource>().Play();
                }
            }
        }
    }

}