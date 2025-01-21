using ObserverPattern;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Character
{
    public class CharactersManager : SubjectMonoBehaviour
    {
        [Header("Test")]
        [SerializeField] private PlayerCharacterInfo playerInfo;

        [Space(30)]
        [SerializeField] private UIPlayerCharacterView playerView;
        [SerializeField] private UIEnemyCharacterView[] enemyViews;

        private PlayerCharacterModel _playerModel;
        private List<EnemyCharacterModel> _enemyModels = new List<EnemyCharacterModel>();

        private void Awake()
        {
            AddEventActions(new Dictionary<EventEnum, Action>
            {
                { EventEnum.CombatPlayerTurnStarted, OnPlayerTurnStarted},
                { EventEnum.CombatEnemyTurnStarted, OnEnemyTurnStarted},
            });
        }

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

        public void SetEnemies(EnemyCharacterInfo[] enemyInfos)
        {
            for (int i = 0; i < enemyInfos.Length; i++)
            {
                EnemyCharacterInfo enemyInfo = enemyInfos[i];

                _enemyModels.Add(new EnemyCharacterModel(
                    enemyInfo.HealthPoints,
                    enemyInfo.ShieldPoints,
                    enemyInfo.Strategy,
                    i,
                    OnEnemyDeath));

                enemyViews[i].SetCharacter(
                    enemyInfo.CharacterName,
                    enemyInfo.Sprite,
                    enemyInfo.HealthPoints,
                    enemyInfo.ShieldPoints);
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
            UseCard(energyUsed, actionEffect, value, _playerModel, playerView);
        }
        public void UseCard(int energyUsed, ActionEffectEnum actionEffect, int value, int enemyIndex)
        {
            UseCard(energyUsed, actionEffect, value, FindEnemyModel(enemyIndex), enemyViews[enemyIndex]);
        }

        public int GetPlayerEnergy() => _playerModel.EnergyPoints;

        private void UseCard(int energyUsed, ActionEffectEnum actionEffect, int value,
            CharacterModel characterModel, UICharacterView characterView)
        {
            if (_playerModel.EnergyPoints < energyUsed) return;

            _playerModel.UseEnergy(energyUsed);
            playerView.SetEnergy(_playerModel.EnergyPoints, _playerModel.MaxEnergyPoints);

            UseActionEffect(characterModel, characterView, actionEffect, value);
        }

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

            int enemyIndex = enemyModel.EnemyIndex;

            _enemyModels.Remove(enemyModel);
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

            characterView.SetHealth(prevHealthPoints, characterModel.HealthPoints, characterModel.MaxHealthPoints);
        }

        private void OnPlayerTurnStarted()
        {
            _playerModel.ResetShield();
            playerView.SetShield(0, 0);

            _playerModel.ResetEnergy();
            playerView.SetEnergy(_playerModel.EnergyPoints, _playerModel.MaxEnergyPoints);

            SetEnemiesStrategies();
        }
        private void OnEnemyTurnStarted()
        {
            StartCoroutine(EnemiesTurnCoroutine());
        }

        private void SetEnemiesStrategies()
        {
            for (int i = 0; i < _enemyModels.Count; i++)
            {
                var enemyModel = _enemyModels[i];
                var enemyView = enemyViews[enemyModel.EnemyIndex];

                enemyModel.SetNextStrategyAction();
                var strategy = enemyModel.CurrentActionEffect;

                enemyView.SetAction(
                    strategy.Sprite,
                    strategy.Value.ToString(),
                    $"<color=yellow>{strategy.ActionEffect}</color> {strategy.Value}");
            }
        }

        private EnemyCharacterModel FindEnemyModel(int index) => _enemyModels.First(x => x.EnemyIndex == index);

        private IEnumerator EnemiesTurnCoroutine()
        {
            for (int i = 0; i < _enemyModels.Count; i++)
            {
                var enemyModel = _enemyModels[i];
                var enemyView = enemyViews[enemyModel.EnemyIndex];

                enemyModel.ResetShield();
                enemyView.SetShield(0, 0);

                var actionEffectData = enemyModel.CurrentActionEffect;

                switch (actionEffectData.ActionEffect)
                {
                    case ActionEffectEnum.DealDamage:
                        UseActionEffect(_playerModel, playerView, actionEffectData.ActionEffect, actionEffectData.Value);
                        break;
                    case ActionEffectEnum.AddShield:
                    case ActionEffectEnum.RestoreHealth:
                        UseActionEffect(enemyModel, enemyView, actionEffectData.ActionEffect, actionEffectData.Value);
                        break;
                }

                enemyView.ResetAction();

                yield return new WaitForSeconds(0.5f);
            }

            Observer.OnHandleEvent(EventEnum.CombatEnemyTurnEnded);
        }
    }
}