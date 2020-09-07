using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantObjects : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

}
