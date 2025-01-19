using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.Character
{
    public class CharacterModel
    {
        public int MaxHealthPoints => _maxHealthPoints;

        public int HealthPoints => _healthPoints;
        public int ShieldPoints => _shieldPoints;

        private int _maxHealthPoints;

        private int _healthPoints;
        private int _shieldPoints;

        public CharacterModel(int healthPoints, int shieldPoints)
        {
            _maxHealthPoints = _healthPoints = healthPoints;
            _shieldPoints = shieldPoints;
        }

        public virtual void OnTurnEnded()
        {
            //passives
        }

        public void HitShield(int damage, out int afterShieldDamage)
        {
            if (damage < 0) DebugManager.Log(DebugCategory.Gameplay, $"{damage} must be a positive value", DebugStatus.Error);

            afterShieldDamage = Math.Max(0, damage - _shieldPoints);

            _shieldPoints = Math.Max(0, _shieldPoints - damage);
        }

        public void DealDamage(int damage)
        {
            if (damage < 0) DebugManager.Log(DebugCategory.Gameplay, $"{damage} must be a positive value", DebugStatus.Error);

            _healthPoints = Math.Max(0, _healthPoints - damage);
        }

        public void RestoreHealth(int value)
        {
            if (value < 0) DebugManager.Log(DebugCategory.Gameplay, $"{value} must be a positive value", DebugStatus.Error);

            _healthPoints = Math.Min(_healthPoints + value, _maxHealthPoints);
        }
    }
}
