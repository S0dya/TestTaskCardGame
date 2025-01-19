using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "Game/Action Effect/Card")]
public class CardInfo : ActionEffectInfo
{
    [SerializeField] private string cardName;
    [SerializeField] private int energyNeeded;
    //[SerializeField] private Sprite cardBg;

    public string CardName => cardName;
    public int EnergyNeeded => energyNeeded;
}
