using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Combat
{
    public class CombatManager : MonoBehaviour
    {
        [Header("Test")]
        [SerializeField] private CharacterInfo playerInfo;
        [Space(30)]

        [SerializeField] private UICharacterView playerView;
        
        [Space(10)]
        [SerializeField] private UICharacterView[] enemyViews;

        private CharacterModel _playerModel;

        private List<CharacterModel> _enemyModels;

        private void Start()
        {
            _playerModel = new CharacterModel(playerInfo.HealthPoints, playerInfo.ShieldPoints);
        }

        public void StartLevel(CharacterInfo[] enemyInfos)
        {
            for (int i = 0; i < enemyInfos.Length; i++)
            {
                _enemyModels.Add(new CharacterModel(enemyInfos[i].HealthPoints, enemyInfos[i].ShieldPoints));
                enemyViews[i].SetCharacter(
                    enemyInfos[i].name, 
                    enemyInfos[i].Sprite, 
                    enemyInfos[i].HealthPoints, 
                    enemyInfos[i].ShieldPoints);
            }
        }

        public void DealDamageToPlayer(int damage)
        {
            DealDamage(_playerModel, playerView, damage);

            if (_playerModel.HealthPoints == 0)
            {
                //handle death
            }
        }
        public void DealDamageToEnemy(int damage, int enemyIndex)
        {
            DealDamage(_enemyModels[enemyIndex], enemyViews[enemyIndex], damage);

            if (_enemyModels[enemyIndex].HealthPoints == 0)
            {
                _enemyModels.RemoveAt(enemyIndex);

                //handle death
            }
        }

        private void DealDamage(CharacterModel characterModel, UICharacterView characterView, int startDamage)
        {
            int prevShieldPoints = characterModel.ShieldPoints;

            characterModel.HitShield(startDamage, out int afterShieldDamage);

            characterView.SetShield(prevShieldPoints, characterModel.ShieldPoints);

            if (afterShieldDamage > 0)
            {
                int prevHealthPoints = characterModel.HealthPoints;

                characterModel.DealDamage(afterShieldDamage);

                characterView.SetHealth(prevHealthPoints, characterModel.HealthPoints, characterModel.MaxHealthPoints);
            }
        }

    }
}
