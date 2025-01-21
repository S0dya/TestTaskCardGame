using System;


namespace Game.Character
{
    public class PlayerCharacterModel : CharacterModel
    {
        public int EnergyPoints => _energyPoints;
        public int MaxEnergyPoints => _maxEnergyPoints;

        private int _maxEnergyPoints;

        private int _energyPoints;

        public PlayerCharacterModel(int energyPoints, int healthPoints, int shieldPoints, Action deathAction) 
            : base (healthPoints, shieldPoints, deathAction)
        {
            _maxEnergyPoints = _energyPoints = energyPoints;
        }

        public void UseEnergy(int value)
        {
            if (value < 0) DebugManager.Log(DebugCategory.Gameplay, $"{value} must be a positive value", DebugStatus.Error);

            _energyPoints = Math.Max(0, _energyPoints - value);
        }

        public void ResetEnergy()
        {
            _energyPoints = _maxEnergyPoints;
        }
    }
}