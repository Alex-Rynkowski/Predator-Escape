using PE.Movement;
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

        bool staminaUsed = false;
        bool sprintToggle = false;
        Text sprintTxt;
        CanvasGroup canvasGroup;
        private void Start()
        {
            SprintText();
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            if (staminaUsed) return;

            float currentAmount = staminaBar.GetComponentInChildren<Image>().fillAmount * 100;

            if (!sprintToggle && currentAmount > 10)
            {
                sprintToggle = true;
                StartCoroutine(DrainContinuously());
            }

            else
            {
                sprintToggle = false;
            }
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
                //StartCoroutine(ButtonAplha());
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

        private IEnumerator ButtonAplha()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.interactable = false;
            FindObjectOfType<PlayerMovement>().MoveSpeed(true);
            staminaUsed = true;

            yield return new WaitForSeconds(1);
            canvasGroup.interactable = true;
            FindObjectOfType<PlayerMovement>().MoveSpeed(false);
            staminaUsed = false;
        }

        private IEnumerator DrainContinuously()
        {
            SprintBar();
            SprintText();

            float currentAmount = staminaBar.GetComponentInChildren<Image>().fillAmount * 100;

            if (sprintToggle && currentAmount >= 1.1f)
            {
                yield return new WaitForSeconds(0.2f);
                StartCoroutine(DrainContinuously());
            }
        }
    }

}