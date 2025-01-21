using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "Game/Config/Timing")]
public class TimingConfig : ScriptableObject
{
    [Header("Card Timings")]
    [Min(0)][SerializeField] private float fightStartWait = 1f;
    [Min(0)][SerializeField] private float fightEndWait = 1f;
    
    [Header("Card Timings")]
    [Min(0)][SerializeField] private float drawCardWait = 0.1f;
    [Min(0)][SerializeField] private float discardCardWait = 0.05f;

    [Header("Enemy Timings")]
    [Min(0)][SerializeField] private float enemyActionWait = 0.2f;

    [Header("Other Timings")]
    [Min(0)][SerializeField] private float playerTurnEndedWait = 0.5f;

    public float FightStartWait => fightStartWait;
    public float FightEndWait => fightEndWait;
    public float DrawCardWait => drawCardWait;
    public float DiscardCardWait => discardCardWait;
    public float EnemyActionWait => enemyActionWait;
    public float PlayerTurnEndedWait => playerTurnEndedWait;
}