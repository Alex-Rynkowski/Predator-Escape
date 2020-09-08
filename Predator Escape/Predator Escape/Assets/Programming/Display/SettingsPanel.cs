using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PE.Core
{
    public class SettingsPanel : MonoBehaviour
    {
        private void Update()
        {
            if (GetComponent<CanvasGroup>().alpha == 0) return;
            CloseSettings();
        }
    private void CloseSettings()
        {
            PointerEventData pointerEvent;
            GraphicRaycaster ray = GetComponent<GraphicRaycaster>();
            EventSystem eventSystem = GetComponent<EventSystem>();
                        
            if (Input.GetKey(KeyCode.Mouse0))
            {
                pointerEvent = new PointerEventData(eventSystem);
                pointerEvent.position = Input.mousePosition;

                List<RaycastResult> results = new List<RaycastResult>();
                ray.Raycast(pointerEvent, results);

                foreach (RaycastResult result in results)
                {
                    return;
                }
                ShowSettings(false);
            }
        }

        public void ShowSettings(bool shouldShowSettings)
        {
            CanvasGroup canvas = GetComponent<CanvasGroup>();

            if (shouldShowSettings)
            {
                canvas.alpha = 1;
                canvas.interactable = true;
                canvas.blocksRaycasts = true;

            }
            else if (!shouldShowSettings)
            {
                canvas.alpha = 0;
                canvas.interactable = false;
                canvas.blocksRaycasts = false;

            }
        }
    }

}