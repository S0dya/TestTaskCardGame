using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Character
{
    public class UIEnemyCharacterView : UICharacterView
    {
        [Space(10)]
        [SerializeField] private Image[] actionValueImages;
        [SerializeField] private TextMeshProUGUI[] actionValueTexts;
        [SerializeField] private TextMeshProUGUI actionDescription;

        [Space(10)]
        [SerializeField] private CanvasGroup[] actionCanvasGroups;

        protected override void Awake()
        {
            base.Awake();

            ToggleActionVisibility(false);
        }

        public void SetAction(Sprite sprite, string text, string description)
        {
            ToggleActionVisibility(true);

            foreach (var actionImage in actionValueImages) actionImage.sprite = sprite;
            foreach (var actionText in actionValueTexts) actionText.text = text;
            actionDescription.text = description;
        }

        public void ResetAction()
        {
            ToggleActionVisibility(false);
        }

        public override void ResetCharacter()
        {
            base.ResetCharacter();

            ResetAction();
        }

        private void ToggleActionVisibility(bool toggle)
        {
            foreach (var canvasGroup in actionCanvasGroups)
            {
                canvasGroup.alpha = toggle ? 1 : 0;
            }
        }
    }
}