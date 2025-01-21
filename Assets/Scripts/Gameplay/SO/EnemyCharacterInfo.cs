using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(menuName = "Game/Enemy/CharacterEnemy")]
public class EnemyCharacterInfo : CharacterInfo
{
    [SerializeField] private EnemyStrategy strategy;

    public ActionEffectData[] Strategy => strategy.ActionEffects;
}
