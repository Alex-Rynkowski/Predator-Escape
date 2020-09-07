using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace PE.Core
{

    public class SceneLoader : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] string sceneToLoad;

        public void OnPointerClick(PointerEventData eventData)
        {
            StartCoroutine(LoadScene(sceneToLoad));
        }

        IEnumerator LoadScene(string loadScene)
        {
            yield return FindObjectOfType<UIFader>().FadeToBlack();
            yield return SceneManager.LoadSceneAsync(loadScene);            
        }
    }

}