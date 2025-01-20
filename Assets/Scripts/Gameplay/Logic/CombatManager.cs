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

        private Coroutine _fightStartedCoroutine;

        private void Awake()
        {
            AddEventActions(new Dictionary<EventEnum, Action>
            {
                { EventEnum.CombatFightStarted, OnFightStarted},
            });
        }

        public void StartLevel(CharacterInfo[] enemyInfos)
        {
            charactersManager.SetEnemies(enemyInfos);
        }

        private void OnFightStarted()
        {
            if (_fightStartedCoroutine != null) StopCoroutine(_fightStartedCoroutine);
            _fightStartedCoroutine = StartCoroutine(FightStartedCoroutine());
        }

        private IEnumerator FightStartedCoroutine()
        {
            yield return new WaitForSeconds(1);

            Observer.OnHandleEvent(EventEnum.CombatPlayerTurnStarted);
        }
    }
}
