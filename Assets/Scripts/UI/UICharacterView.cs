using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Character
{
    public class UICharacterView : MonoBehaviour
    {
        [SerializeField] private GameObject mainObject;

        [Space(20)]
        [SerializeField] private TextMeshProUGUI characterName;
        [SerializeField] private Image characterImage;

        [Space(20)]
        [SerializeField] private TextMeshProUGUI characterHealthPoints;
        [SerializeField] private Image characterHealthBarImage;

        [Space(20)]
        [SerializeField] private GameObject shieldObject;
        [SerializeField] private TextMeshProUGUI characterShieldPoints;

        [Header("Details")]
        [SerializeField] private CanvasGroup detailsCanvasGroup;

        [Space(10)]
        [Header("Action")]
        [SerializeField] private CanvasGroup actionCanvasGroup;
        [SerializeField] private EventTrigger actionEventTrigger;

        private void Awake()
        {
            mainObject.SetActive(false);

            shieldObject.SetActive(false);

            detailsCanvasGroup.alpha = 0f;
            ToggleAction(false);
        }

        public void Init()
        {

        }

        public virtual void SetCharacter(string name, Sprite sprite, int healthPoints, int shieldPoints)
        {
            characterName.text = name;
            characterImage.sprite = sprite;

            SetHealth(healthPoints, healthPoints);
            if (shieldPoints > 0) SetShield(0, shieldPoints);

            mainObject.SetActive(true);
        }

        public void OnDetailsPointerEnter()
        {
            detailsCanvasGroup.alpha = 1;
        }
        public void OnDetailsPointerExit()
        {
            detailsCanvasGroup.alpha = 0;
        }

        public void OnActionPointerEnter()
        {
            actionCanvasGroup.alpha = 1;
        }
        public void OnActionPointerExit()
        {
            actionCanvasGroup.alpha = 0;
        }

        public void SetShield(int prevValue, int newValue)
        {
            if (prevValue == 0 && newValue > 0)
            {
                shieldObject.SetActive(true);
            }
            else if (prevValue > 0 && newValue == 0)
            {
                shieldObject.SetActive(false);
            }

            Helper.SetText(characterShieldPoints, newValue.ToString());
        }

        public void SetHealth(int prevValue, int newValue, int maxValue)
        {
            if (prevValue > newValue)
            {
                //play effects
            }

            SetHealth(newValue, maxValue);
        }
        public void SetHealth(int newValue, int maxValue)
        {
            characterHealthBarImage.fillAmount = newValue / maxValue;

            Helper.SetText(characterHealthPoints, $"{newValue} / {maxValue}");
        }

        public void ToggleAction(bool toggle)
        {
            actionCanvasGroup.alpha = toggle ? 1 : 0; actionCanvasGroup.blocksRaycasts = toggle;
        }
    }
}
