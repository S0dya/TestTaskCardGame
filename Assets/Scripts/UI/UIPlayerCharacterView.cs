using TMPro;
using UnityEngine;

namespace Game.Character
{
    public class UIPlayerCharacterView : UICharacterView
    {
        [Space(10)]
        [SerializeField] private TextMeshProUGUI energyText;

        public override void SetCharacter(string name, Sprite sprite, int healthPoints, int shieldPoints)
        {
            base.SetCharacter(name, sprite, healthPoints, shieldPoints);


        }

        public void SetEnergy(int energy, int maxEnergy)
        {
            Helper.SetText(energyText, $"{energy} / {maxEnergy}");

            //if (energy == 0)
            //{
            //    //red outline, stop effect
            //}
        }
    }
}