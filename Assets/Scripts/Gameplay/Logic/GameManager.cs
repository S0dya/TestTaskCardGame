using Game.Card;
using Game.Combat;
using ObserverPattern;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GameManager : SubjectMonoBehaviour
    {
        [Header("Test")]
        [SerializeField] private LevelInfo[] levelInfos;

        [Space(10)]
        [SerializeField] private CardsBundleInfo startingCardsBundle;

        [Space(10)]
        [SerializeField] private CombatManager combatManager;
        [SerializeField] private CardsManager cardsManager;

        private int _levelIndex;

        private Coroutine _fightEndCoroutine;

        private void Awake()
        {
            AddEventActions(new Dictionary<EventEnum, Action>
            {
                { EventEnum.CombatFightEnded, OnFightEnded},
            });
        }

        private void Start()
        {
            foreach (var card in startingCardsBundle.CardInfos)
            {
                cardsManager.AddCard(card);
            }

            StartLevel();

            _levelIndex++;
        }

        private void OnFightEnded()
        {
            if (_fightEndCoroutine != null) StopCoroutine(_fightEndCoroutine);
            _fightEndCoroutine = StartCoroutine(FightEndCoroutine());
        }

        private void StartLevel()
        {
            int index = _levelIndex % levelInfos.Length;

            combatManager.StartLevel(levelInfos[index].EnemyCharacters);

            Observer.OnHandleEvent(EventEnum.CombatFightStarted);
        }

        private IEnumerator FightEndCoroutine()
        {
            yield return new WaitForSeconds(1);

            StartLevel();

            _levelIndex++;
        }
    }
}
