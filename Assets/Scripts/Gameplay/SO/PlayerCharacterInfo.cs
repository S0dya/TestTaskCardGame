using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(menuName = "Game/Player/CharacterPlayer")]
public class PlayerCharacterInfo : CharacterInfo
{
    [SerializeField] private int energyPoints;

    public int EnergyPoints => energyPoints;
}
