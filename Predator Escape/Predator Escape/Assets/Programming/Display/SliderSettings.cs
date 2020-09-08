﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SliderSettings : MonoBehaviour, IPointerClickHandler, IDragHandler, IPointerDownHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        SliderValueChange();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SliderValueChange();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SliderValueChange();
    }

    public void SliderValueChange()
    {
        FindObjectOfType<GameManager>().a_musicSettings();
    }

}
