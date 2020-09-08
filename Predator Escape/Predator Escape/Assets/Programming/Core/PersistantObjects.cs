using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PE.Core
{
    public class PersistantObjects : MonoBehaviour
    {
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            KeepOnlyOneObject();
        }

        void KeepOnlyOneObject()
        {
            for (int i = 0; i < FindObjectsOfType<PersistantObjects>().Length; i++)
            {
                if(i > 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}