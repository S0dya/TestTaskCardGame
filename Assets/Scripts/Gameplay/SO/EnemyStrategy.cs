using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(menuName = "Game/Enemy/Strategy")]
public class EnemyStrategy : ScriptableObject
{
    [SerializeField] private ActionEffectData[] actionEffects;

    public ActionEffectData[] ActionEffects => actionEffects;
}
