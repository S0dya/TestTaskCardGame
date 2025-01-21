using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Character;
using ObserverPattern;
using System;

namespace Game.Combat
{
    public class CombatManager : SubjectMonoBehaviour
    {
        [SerializeField] private CharactersManager charactersManager;

        [Space(10)]
        [SerializeField] private TimingConfig timingConfig;

        [Space(10)]
        [SerializeField] private UICombatView combatView;

        private Coroutine _fightStartedCoroutine;
        private Coroutine _playerTurnEndedCoroutine;

        private void Awake()
        {
            AddEventActions(new Dictionary<EventEnum, Action>
            {
                { EventEnum.CombatFightStarted, OnFightStarted},
                { EventEnum.CombatPlayerTurnStarted, OnPlayerTurnStarted},
                { EventEnum.CombatPlayerTurnEnded, OnPlayerTurnEnded},
                { EventEnum.CombatEnemyTurnEnded, OnEnemyTurnEnded},
                { EventEnum.CombatFightEnded, OnFightEnded},
            });
        }

        public void StartLevel(EnemyCharacterInfo[] enemyInfos)
        {
            charactersManager.SetEnemies(enemyInfos);
        }

        public void OnFinishPlayerTurnPressed()
        {
            Observer.OnHandleEvent(EventEnum.CombatPlayerTurnEnded);
        }

        private void OnFightStarted()
        {
            if (_fightStartedCoroutine != null) StopCoroutine(_fightStartedCoroutine);
            _fightStartedCoroutine = StartCoroutine(FightStartedCoroutine());
        }
        private void OnPlayerTurnStarted()
        {
            combatView.ToggleEndTurnButtonCanvasGroup(true);
        }
        private void OnPlayerTurnEnded()
        {
            combatView.ToggleEndTurnButtonCanvasGroup(false);

            if (_playerTurnEndedCoroutine != null) StopCoroutine(_playerTurnEndedCoroutine);
            _playerTurnEndedCoroutine = StartCoroutine(PlayerTurnEndedCoroutine());
        }
        private void OnEnemyTurnEnded()
        {
            Observer.OnHandleEvent(EventEnum.CombatPlayerTurnStarted);
        }
        private void OnFightEnded()
        {
            combatView.ToggleEndTurnButtonCanvasGroup(false);
        }

        private IEnumerator FightStartedCoroutine()
        {
            yield return new WaitForSeconds(timingConfig.FightStartWait);

            Observer.OnHandleEvent(EventEnum.CombatPlayerTurnStarted);
        }

        private IEnumerator PlayerTurnEndedCoroutine()
        {
            yield return new WaitForSeconds(timingConfig.PlayerTurnEndedWait);

            Observer.OnHandleEvent(EventEnum.CombatEnemyTurnStarted);
        }
    }
}
