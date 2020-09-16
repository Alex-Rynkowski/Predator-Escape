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
        [Header("Stamina Attributes, Hover over name for details")]
        [Tooltip("How fast stamina regenerates, Lower is faster")] [SerializeField] private float regenSpeed;
        [Tooltip("How fast stamina drains, Lower is faster")] [SerializeField] private float drainSpeed;
        [Tooltip("Amount of stamina that drains every drainSpeed in seconds")][SerializeField] float depleteAmount;
        [Tooltip("The max amount of stamina")][SerializeField] private float maxStamina;

        bool staminaUsed = false;
        bool sprintToggle = false;
        Text sprintTxt;
        CanvasGroup canvasGroup;
        private void Start()
        {
            SprintText();
            StartCoroutine(RegenStamina());
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (staminaUsed) return;

            float currentAmount = staminaBar.GetComponentInChildren<Image>().fillAmount * maxStamina;

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
            float currentAmount = staminaBar.GetComponentInChildren<Image>().fillAmount * maxStamina;
            float newAmount = currentAmount - depleteAmount;

            print(newAmount);

            if (newAmount < 0)
            {
                return currentAmount;
            }
            else
            {
                //StartCoroutine(ButtonAplha());
                return staminaBar.GetComponentInChildren<Image>().fillAmount -= (depleteAmount / maxStamina);
            }
            
        }

        private float UpdateStamina()
        {
            float currentAmount = staminaBar.GetComponentInChildren<Image>().fillAmount * maxStamina;
            float newAmount = currentAmount + depleteAmount;

            print(currentAmount);

            if (newAmount > maxStamina)
            {
                return currentAmount;
            }
            else
            {
                //StartCoroutine(ButtonAplha());
                return staminaBar.GetComponentInChildren<Image>().fillAmount += (depleteAmount / maxStamina);
            }
        }

        private void SprintText()
        {
            sprintTxt = staminaBar.GetComponentInChildren<Text>();
            sprintTxt.text = string.Format("Stamina: {0}/" + maxStamina, GetFillAmount());
        }

        private float GetFillAmount()
        {
            return Convert.ToInt32(staminaBar.GetComponentInChildren<Image>().fillAmount * maxStamina);
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

            float currentAmount = staminaBar.GetComponentInChildren<Image>().fillAmount * maxStamina;

            if (sprintToggle && currentAmount > 1)
            {
                yield return new WaitForSeconds(drainSpeed);
                StartCoroutine(DrainContinuously());
            }
            else
            {
                yield return new WaitForSeconds(0.5f);
                sprintToggle = false;
            }
        }

        private IEnumerator RegenStamina()
        {
            if (!sprintToggle)
            {
                UpdateStamina();
                SprintText();
            }
            yield return new WaitForSeconds(regenSpeed);
            StartCoroutine(RegenStamina());
        }
    }

}