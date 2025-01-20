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

        protected Action _deathAction;

        public CharacterModel(int healthPoints, int shieldPoints, Action deathAction)
        {
            _maxHealthPoints = _healthPoints = healthPoints;
            _shieldPoints = shieldPoints;

            _deathAction = deathAction;
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
        public void SetShield(int value)
        {
            if (value < 0) DebugManager.Log(DebugCategory.Gameplay, $"{value} must be a positive value", DebugStatus.Error);

            _shieldPoints += value;
        }

        public void DealDamage(int damage)
        {
            if (damage < 0) DebugManager.Log(DebugCategory.Gameplay, $"{damage} must be a positive value", DebugStatus.Error);

            _healthPoints = Math.Max(0, _healthPoints - damage);

            if (_healthPoints == 0)
            {
                _deathAction?.Invoke();
            }
        }
        public void RestoreHealth(int value)
        {
            if (value < 0) DebugManager.Log(DebugCategory.Gameplay, $"{value} must be a positive value", DebugStatus.Error);

            _healthPoints = Math.Min(_healthPoints + value, _maxHealthPoints);
        }
    }
}
