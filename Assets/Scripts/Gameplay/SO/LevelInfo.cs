using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Game/Level")]
public class LevelInfo : ScriptableObject
{
    [SerializeField] private CharacterInfo[] enemyCharacters;

    public CharacterInfo[] EnemyCharacters => enemyCharacters;
}
