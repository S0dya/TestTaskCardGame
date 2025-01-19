using System;

namespace Game.Character
{
    public class PlayerCharacterModel : CharacterModel
    {
        public int EnergyPoints => _energyPoints;
        public int MaxEnergyPoints => _maxEnergyPoints;

        private int _maxEnergyPoints;

        private int _energyPoints;

        public PlayerCharacterModel(int energyPoints, int healthPoints, int shieldPoints) 
            : base (healthPoints, shieldPoints)
        {
            _maxEnergyPoints = _energyPoints = energyPoints;
        }

        public override void OnTurnEnded()
        {
            base.OnTurnEnded();

            _energyPoints = _maxEnergyPoints;
        }

        public void UseEnergy(int value)
        {
            if (value < 0) DebugManager.Log(DebugCategory.Gameplay, $"{value} must be a positive value", DebugStatus.Error);

            _energyPoints = Math.Max(0, _energyPoints + value);
        }
    }
}