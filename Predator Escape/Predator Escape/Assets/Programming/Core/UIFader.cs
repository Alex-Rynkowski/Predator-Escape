using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PE.Core
{
    public class UIFader : MonoBehaviour
    {
        [SerializeField] float fadeToBlackTimer;
        [SerializeField] float fadeFromBlackTimer;

        CanvasGroup canvasGroup;

        public bool sceneLoaded = false;
        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public IEnumerator FadeToBlack()
        {
            while (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += Time.deltaTime / fadeToBlackTimer;
                yield return null;
            }

            yield return new WaitForSeconds(1);
            StartCoroutine(FadeFromBlack());
        }

        public IEnumerator FadeFromBlack()
        {
            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime / fadeFromBlackTimer;
                yield return null;
            }
        }
    }

}