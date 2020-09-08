using PE.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PE.Display
{

    public class SliderSettings : MonoBehaviour, IPointerClickHandler, IDragHandler, IPointerDownHandler
    {

        private void Start()
        {
            GetComponent<Slider>().value = FindObjectOfType<GameManager>().musicVolumeSet;
        }
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

}