using ObserverPattern;
using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace Game.Character
{
    public class CharactersManager : MonoBehaviour
    {
        [Header("Test")]
        [SerializeField] private PlayerCharacterInfo playerInfo;

        [Space(30)]
        [SerializeField] private UIPlayerCharacterView playerView;
        [SerializeField] private UICharacterView[] enemyViews;

        private PlayerCharacterModel _playerModel;
        private List<EnemyCharacterModel> _enemyModels = new List<EnemyCharacterModel>();

        private void Start()
        {
            _playerModel = new PlayerCharacterModel(
                playerInfo.EnergyPoints,
                playerInfo.HealthPoints,
                playerInfo.ShieldPoints,
                OnPlayerDeath); 

            playerView.SetCharacter(
                    playerInfo.CharacterName,
                    playerInfo.Sprite,
                    playerInfo.HealthPoints,
                    playerInfo.ShieldPoints,
                    playerInfo.EnergyPoints);
        }

        public void InitActions(Action OnPointerUpActionPlayer, Action<int> OnPointerUpActionEnemy)
        {
            playerView.Init(OnPointerUpActionPlayer);

            for (int i = 0; i < enemyViews.Length; i++)
            {
                int index = i;

                enemyViews[i].Init(() => OnPointerUpActionEnemy.Invoke(index));
            }
        }

        public void SetEnemies(CharacterInfo[] enemyInfos)
        {
            for (int i = 0; i < enemyInfos.Length; i++)
            {
                _enemyModels.Add(new EnemyCharacterModel(
                    enemyInfos[i].HealthPoints, 
                    enemyInfos[i].ShieldPoints, 
                    OnEnemyDeath));

                enemyViews[i].SetCharacter(
                    enemyInfos[i].CharacterName,
                    enemyInfos[i].Sprite,
                    enemyInfos[i].HealthPoints,
                    enemyInfos[i].ShieldPoints);
            }
        }

        public void EnableActionForCard(ActionEffectEnum actionEffect)
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
        public void DisableAction()
        {
            playerView.ToggleAction(false);
            foreach (var enemyView in enemyViews) enemyView.ToggleAction(false);
        }

        public void UseCard(int energyUsed, ActionEffectEnum actionEffect, int value)
        {
            _playerModel.UseEnergy(energyUsed);
            playerView.SetEnergy(_playerModel.EnergyPoints, _playerModel.MaxEnergyPoints);

            UseActionEffect(_playerModel, playerView, actionEffect, value);
        }
        public void UseCard(int energyUsed, ActionEffectEnum actionEffect, int value, int enemyIndex)
        {
            _playerModel.UseEnergy(energyUsed);
            playerView.SetEnergy(_playerModel.EnergyPoints, _playerModel.MaxEnergyPoints);

            Debug.Log(enemyIndex + " enemy");

            UseActionEffect(_enemyModels[enemyIndex], enemyViews[enemyIndex], actionEffect, value);
        }

        public int GetPlayerEnergy() => _playerModel.EnergyPoints;

        private void UseActionEffect(CharacterModel characterModel, UICharacterView characterView, ActionEffectEnum actionEffect, int value)
        {
            switch (actionEffect)
            {
                case ActionEffectEnum.DealDamage:
                    DealDamage(characterModel, characterView, value);
                    break;
                case ActionEffectEnum.AddShield:
                    AddShield(characterModel, characterView, value);
                    break;
                case ActionEffectEnum.RestoreHealth:
                    RestoreHealth(characterModel, characterView, value);
                    break;

                case ActionEffectEnum.none:
                    DebugManager.Log(DebugCategory.Gameplay, "None action effect", DebugStatus.Error);
                    break;
            }
        }

        private void OnPlayerDeath()
        {
            DebugManager.Log(DebugCategory.Points, "Player died");
        }

        private void OnEnemyDeath(EnemyCharacterModel enemyModel)
        {
            DebugManager.Log(DebugCategory.Points, "Enemy died");

            int enemyIndex = _enemyModels.IndexOf(enemyModel);
            _enemyModels.RemoveAt(enemyIndex);
            enemyViews[enemyIndex].ResetCharacter();

            if (_enemyModels.Count == 0)
            {
                Observer.OnHandleEvent(EventEnum.CombatFightEnded);
            }

            //handle death
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

        private void AddShield(CharacterModel characterModel, UICharacterView characterView, int value)
        {
            var prevShieldPoints = characterModel.ShieldPoints;

            characterModel.SetShield(value);

            characterView.SetShield(prevShieldPoints, characterModel.ShieldPoints);
        }

        private void RestoreHealth(CharacterModel characterModel, UICharacterView characterView, int value)
        {
            var prevHealthPoints = characterModel.HealthPoints;

            characterModel.RestoreHealth(value);

            characterView.SetHealth(prevHealthPoints, characterModel.HealthPoints);
        }
    }
}