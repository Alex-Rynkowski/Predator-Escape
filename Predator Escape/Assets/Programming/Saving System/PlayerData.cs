using PE.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PE.Saving
{
    [System.Serializable]
    public class PlayerData
    {
        public float musicVolSettings;
        public float playerScore;


        public PlayerData(GameManager gameMan)
        {
            musicVolSettings = gameMan.musicVolumeSet;
        }
    }

}