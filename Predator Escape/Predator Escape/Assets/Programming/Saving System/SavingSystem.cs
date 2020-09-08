using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using PE.Core;

namespace PE.Saving
{
    public static class SavingSystem
    {
        public static void SaveData(GameManager gameMan)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + ".sav";
            Debug.Log(path);
            FileStream stream = new FileStream(path, FileMode.Create);

            PlayerData data = new PlayerData(gameMan);

            formatter.Serialize(stream, data);

            stream.Close();

        }

        public static PlayerData LoadData()
        {
            string path = Application.persistentDataPath + ".sav";
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);
                
                PlayerData data = formatter.Deserialize(stream) as PlayerData;
                stream.Close();
                return data;
                
            }
            else
            {
                Debug.LogError("Save file not found in " + path);
                return null;
            }
        }

    }
}