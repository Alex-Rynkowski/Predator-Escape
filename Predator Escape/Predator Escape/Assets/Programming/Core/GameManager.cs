using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject settingsPanel;

    public float musicVolumeSet;
    public Action a_musicSettings;

    private void Start()
    {
        a_musicSettings += MusicVolumeSettings;
    }
    private void Update()
    {
        CloseSettings();
    }
    private void MusicVolumeSettings()
    {
        musicVolumeSet = FindObjectOfType<SliderSettings>().GetComponent<Slider>().value;
    }

    private void CloseSettings()
    {        
        PointerEventData pointerEvent;
        GraphicRaycaster ray = settingsPanel.GetComponent<GraphicRaycaster>();
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
            HideSettingsPanel();
        }
    }

    private void HideSettingsPanel()
    {
        CanvasGroup canvas = settingsPanel.GetComponent<CanvasGroup>();
        canvas.alpha = 0;
        canvas.interactable = false;
        canvas.blocksRaycasts = false;
    }
}
