using events;
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

        private void Awake()
        {
            mainObject.SetActive(false);
        }


        public void SetCard(string name, Sprite sprite, string description, int energyNeeded)
        {
            cardName.text = name;
            cardImage.sprite = sprite;
            this.description.text = description;
            
            this.energyNeeded.text = energyNeeded.ToString();

            mainObject.SetActive(false);
        }

        public void ResetCard()
        {
            mainObject.SetActive(false);
        }

        //public void ToggleEnergySatisfied(bool toggle)

        public void ToggleCard(bool toggle) => mainObject.SetActive(toggle);
    }
}