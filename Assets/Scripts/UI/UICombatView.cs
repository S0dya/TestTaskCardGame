using UnityEngine;

namespace Game.Combat
{
    public class UICombatView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup endTurnButtonCanvasGroup;

        private void Start()
        {
            ToggleEndTurnButtonCanvasGroup(false);
        }

        public void ToggleEndTurnButtonCanvasGroup(bool toggle) => endTurnButtonCanvasGroup.interactable = toggle;
    }
}