using Game.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        [Header("Test")]
        [SerializeField] private LevelInfo[] levelInfos;
        [Space(10)]
        [SerializeField] private CombatManager combatManager;

        private int _levelIndex;

        private void Start()
        {
            combatManager.StartLevel(levelInfos[_levelIndex].EnemyCharacters);

            _levelIndex++;
        }
    }
}
