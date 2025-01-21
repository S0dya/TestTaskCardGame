using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Game/Level")]
public class LevelInfo : ScriptableObject
{
    [SerializeField] private EnemyCharacterInfo[] enemyCharacters;

    public EnemyCharacterInfo[] EnemyCharacters => enemyCharacters;
}
