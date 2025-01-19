using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "Game/Cards Bundle")]
public class CardsBundleInfo : ScriptableObject
{
    [SerializeField] private CardInfo[] cardInfos;

    public CardInfo[] CardInfos => cardInfos;
}
