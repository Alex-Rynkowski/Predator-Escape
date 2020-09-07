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
        [SerializeField] GameObject settingsPanel;

        public float musicVolumeSet;
        public Action a_musicSettings;

        GraphicRaycaster m_Raycaster;
        PointerEventData m_PointerEventData;
        EventSystem m_EventSystem;

        private void Start()
        {
            a_musicSettings += MusicVolumeSettings;
        }
        private void Update()
        {
            Settings();
        }
        private void MusicVolumeSettings()
        {
            musicVolumeSet = FindObjectOfType<SliderSettings>().GetComponent<Slider>().value;            
        }

        private void Settings()
        {

        }

    }

}