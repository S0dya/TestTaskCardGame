using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Character;

namespace Game.Combat
{
    public class CombatManager : MonoBehaviour
    {
        [SerializeField] private CharactersManager charactersManager;
        
        public void StartLevel(CharacterInfo[] enemyInfos)
        {
            charactersManager.SetEnemies(enemyInfos);
        }

    }
}
