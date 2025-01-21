using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "Game/Card")]
public class CardInfo : ScriptableObject
{
    [SerializeField] private ActionEffectData actionEffectData;
    [SerializeField] private string cardName;
    [SerializeField] private int energyNeeded;
    //[SerializeField] private Sprite cardBg;

    public ActionEffectData ActionEffectData => actionEffectData;
    public string CardName => cardName;
    public int EnergyNeeded => energyNeeded;
}
