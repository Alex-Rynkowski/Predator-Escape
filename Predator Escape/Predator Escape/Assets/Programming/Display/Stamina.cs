using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PE.Display
{
    public class Stamina : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] GameObject staminaBar;
        [SerializeField] float depleteAmount;

        Text sprintTxt;

        private void Start()
        {
            SprintText();
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            SprintBar();
            SprintText();
        }

        private float SprintBar()
        {
            float currentAmount = staminaBar.GetComponentInChildren<Image>().fillAmount * 100;
            float newAmount = currentAmount - depleteAmount;

            print(newAmount);
            if (newAmount < 0)
            {
                return currentAmount;
            }
            else
            {
                return staminaBar.GetComponentInChildren<Image>().fillAmount -= (depleteAmount / 100);
            }
            
        }

        private void SprintText()
        {
            sprintTxt = staminaBar.GetComponentInChildren<Text>();
            sprintTxt.text = string.Format("Stamina: {0}/100", GetFillAmount());
        }

        private float GetFillAmount()
        {
            return Convert.ToInt32(staminaBar.GetComponentInChildren<Image>().fillAmount * 100);
        }
    }

}