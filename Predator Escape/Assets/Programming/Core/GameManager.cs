using PE.Display;
using PE.Saving;
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
        [SerializeField] GameObject settingsPanel;

        public float musicVolumeSet;
        public Action a_musicSettings;

        bool loadedData = false;

        private void Start()
        {
            a_musicSettings += MusicVolumeSettings;
            Load();
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.S))
            {
                Save();
            }
            if (Input.GetKey(KeyCode.L))
            {
                Load();
            }
        }
        private void MusicVolumeSettings()
        {
            if (!loadedData)
            {
                musicVolumeSet = FindObjectOfType<SliderSettings>().GetComponent<Slider>().value;
            }
            else
            {
                FindObjectOfType<SliderSettings>().GetComponent<Slider>().value = musicVolumeSet;
            }
            
        }

        public void Save()
        {
            SavingSystem.SaveData(this);
        }

        public void Load()
        {
            loadedData = true;
            PlayerData data = SavingSystem.LoadData();

            musicVolumeSet = data.musicVolSettings;
            a_musicSettings();
            loadedData = false;
        }
 
    }
}
