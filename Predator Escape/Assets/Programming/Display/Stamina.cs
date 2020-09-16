using PE.Movement;
using System;
using System.Collections;
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
        private Image img;
        private void Start()
        {
            img = staminaBar.GetComponentInChildren<Image>();
            sprintTxt = staminaBar.GetComponentInChildren<Text>();
            canvasGroup = GetComponent<CanvasGroup>();
            SprintText();
            StartCoroutine(RegenStamina());
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            ToggleSprint();
            //StartCoroutine(ButtonAplha());
        }
        
        public void ToggleSprint() // Call this in an input script
        {
            if (staminaUsed) return;

            float currentAmount = GetFillAmountF();

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
            float currentAmount = GetFillAmountF();
            float newAmount = currentAmount - depleteAmount;

            if (newAmount < 0)
            {
                return currentAmount;
            }
            else
            {
                //StartCoroutine(ButtonAplha());
                return img.fillAmount -= (depleteAmount / maxStamina);
            }
            
        }

        private float UpdateStamina()
        {
            float currentAmount = GetFillAmountF();
            float newAmount = currentAmount + depleteAmount;

            if (newAmount > maxStamina)
            {
                return currentAmount;
            }
            else
            {
                //StartCoroutine(ButtonAplha());
                return img.fillAmount += (depleteAmount / maxStamina);
            }
        }
        
        private float ChangeMaxStamina(float amount, float duration)
        {
            float currentAmount = GetFillAmountF();
            maxStamina += amount;
            img.fillAmount = currentAmount / maxStamina;
            StartCoroutine(ResetMaxStamina(amount, duration));
            
            return img.fillAmount += (depleteAmount / maxStamina);
        }

        private void SprintText()
        {
            sprintTxt.text = string.Format("Stamina: {0}/" + maxStamina, GetFillAmountInt32());
        }

        private float GetFillAmountInt32()
        {
            return Convert.ToInt32(GetFillAmountF());
        }

        private float GetFillAmountF()
        {
            return img.fillAmount * maxStamina;
        }

        private IEnumerator ButtonAplha()
        {
            canvasGroup.interactable = false;
            //FindObjectOfType<PlayerMovement>().MoveSpeed(true);
            staminaUsed = true;

            yield return new WaitForSeconds(1f);
            canvasGroup.interactable = true;
            //FindObjectOfType<PlayerMovement>().MoveSpeed(false);
            staminaUsed = false;
        }

        private IEnumerator DrainContinuously()
        {
            SprintBar();
            SprintText();

            float currentAmount = GetFillAmountF();

            if (sprintToggle && currentAmount > 1)
            {
                yield return new WaitForSeconds(drainSpeed);
                StartCoroutine(DrainContinuously());
            }
            else
            {
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

        private IEnumerator ResetMaxStamina(float amount, float duration)
        {
            yield return new WaitForSeconds(duration);
            float currentAmount = GetFillAmountF();
            maxStamina -= amount;
            img.fillAmount = currentAmount / maxStamina;
        }
    }

}