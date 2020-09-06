using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace PE.Core
{

    public class SceneLoader : MonoBehaviour
    {
        public void LoadScene(string sceneToLoad)
        {
            StartCoroutine(StartToFade(sceneToLoad));
        }

        IEnumerator StartToFade(string loadScene)
        {
            yield return FindObjectOfType<UIFader>().FadeToBlack(1);
            yield return SceneManager.LoadSceneAsync(loadScene);
            yield return FindObjectOfType<UIFader>().FadeFromBlack(1);

        }

    }

}