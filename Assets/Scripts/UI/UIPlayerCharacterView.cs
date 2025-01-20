using TMPro;
using UnityEngine;

namespace Game.Character
{
    public class UIPlayerCharacterView : UICharacterView
    {
        [Space(10)]
        [SerializeField] private TextMeshProUGUI energyText;

        public void SetCharacter(string name, Sprite sprite, int healthPoints, int shieldPoints, int energyPoints)
        {
            SetCharacter(name, sprite, healthPoints, shieldPoints);

            SetEnergy(energyPoints, energyPoints);
        }

        public void SetEnergy(int energy, int maxEnergy)
        {
            Helper.SetText(energyText, $"{energy}/{maxEnergy}");
        }
    }
}