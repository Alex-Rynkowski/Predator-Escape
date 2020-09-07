using PR.Display;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PE.Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] Slider musicSlider;

        public float musicVolumeSet;
        public Action a_musicSettings;

        private void Start()
        {
            a_musicSettings += MusicVolumeSettings;
        }

        private void MusicVolumeSettings()
        {
            musicVolumeSet = FindObjectOfType<SliderSettings>().GetComponent<Slider>().value;            
        }

    }

}