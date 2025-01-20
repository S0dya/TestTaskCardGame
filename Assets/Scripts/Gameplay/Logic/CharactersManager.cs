using ObserverPattern;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Character
{
    public class CharactersManager : MonoBehaviour
    {
        [Header("Test")]
        [SerializeField] private PlayerCharacterInfo playerInfo;

        [Space(30)]
        [SerializeField] private UICharacterView playerView;
        [SerializeField] private UICharacterView[] enemyViews;

        private PlayerCharacterModel _playerModel;
        private List<CharacterModel> _enemyModels = new List<CharacterModel>();

        private void Start()
        {
            _playerModel = new PlayerCharacterModel(playerInfo.EnergyPoints, playerInfo.HealthPoints, playerInfo.ShieldPoints);
            playerView.SetCharacter(
                    playerInfo.CharacterName,
                    playerInfo.Sprite,
                    playerInfo.HealthPoints,
                    playerInfo.ShieldPoints);
        }

        public void InitActions(Action OnPointerUpActionPlayer, Action<int> OnPointerUpActionEnemy)
        {
            playerView.Init(OnPointerUpActionPlayer);

            for (int i = 0; i < enemyViews.Length; i++)
            {
                enemyViews[i].Init(() => OnPointerUpActionEnemy.Invoke(i));
            }
        }

        public void SetEnemies(CharacterInfo[] enemyInfos)
        {
            for (int i = 0; i < enemyInfos.Length; i++)
            {
                _enemyModels.Add(new CharacterModel(enemyInfos[i].HealthPoints, enemyInfos[i].ShieldPoints));
                enemyViews[i].SetCharacter(
                    enemyInfos[i].CharacterName,
                    enemyInfos[i].Sprite,
                    enemyInfos[i].HealthPoints,
                    enemyInfos[i].ShieldPoints);
            }
        }

        public void OnCardDragged(ActionEffectEnum actionEffect)
        {
            switch (actionEffect)
            {
                case ActionEffectEnum.DealDamage:
                    foreach (var enemyView in enemyViews)
                    {
                        enemyView.ToggleAction(true);
                    }
                    break;

                case ActionEffectEnum.AddShield:
                case ActionEffectEnum.RestoreHealth:
                    playerView.ToggleAction(true);
                    break;

                case ActionEffectEnum.none: 
                    DebugManager.Log(DebugCategory.Gameplay, "None action effect", DebugStatus.Error);
                    break;
            }
        }
        public void UseCard(int energyUsed)
        {
            _playerModel.UseEnergy(energyUsed);
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

                if (_enemyModels.Count == 0)
                {
                    Observer.OnHandleEvent(EventEnum.CombatFightEnded);
                }

                //handle death
            }
        }

        public int GetPlayerEnergy() => _playerModel.EnergyPoints;

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