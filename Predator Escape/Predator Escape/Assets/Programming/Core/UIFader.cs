using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PE.Core
{    
    public class UIFader : MonoBehaviour
    {        
        CanvasGroup fader;
        private void Start()
        {            
            fader = GetComponent<CanvasGroup>();
        }

        public IEnumerator FadeToBlack(float time)
        {
            while (fader.alpha < 1)
            {
                fader.alpha += Time.deltaTime / time;
                yield return null;
            }
        }

        public IEnumerator FadeFromBlack(float time)
        {
            while (fader.alpha > 0)
            {
                fader.alpha -= Time.deltaTime / time;
                yield return null;
            }
        }

    }

}