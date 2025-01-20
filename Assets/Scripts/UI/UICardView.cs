using events;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Card
{
    public class UICardView : MonoBehaviour
    {
        [SerializeField] private GameObject mainObject;

        [Space(20)]
        [SerializeField] private TextMeshProUGUI cardName;
        [SerializeField] private Image cardImage;
        [SerializeField] private TextMeshProUGUI description;

        [Space(10)]
        [SerializeField] private TextMeshProUGUI energyNeeded;

        public bool IsActive => mainObject.activeSelf;

        public void SetCard(string name, Sprite sprite, string description, string energyNeeded)
        {
            cardName.text = name;
            cardImage.sprite = sprite;
            this.description.text = description;
            
            this.energyNeeded.text = energyNeeded;

            ToggleCard(true);
        }

        public void ResetCard()
        {
            ToggleCard(false);
        }

        public void MoveToParent(RectTransform parent)
        {
            mainObject.transform.SetParent(parent);
        }

        //public void ToggleEnergySatisfied(bool toggle)

        private void ToggleCard(bool toggle) => mainObject.SetActive(toggle);
    }
}