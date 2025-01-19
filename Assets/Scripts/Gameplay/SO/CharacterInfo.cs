using System;
using UnityEngine;

[Serializable]
public class CharacterInfo : ScriptableObject
{
    [SerializeField] private string characterName;
    [SerializeField] private Sprite sprite;

    [Space(10)]
    [SerializeField] private int healthPoints;
    [SerializeField] private int shieldPoints;

    public string CharacterName => characterName;
    public Sprite Sprite => sprite;
    public int HealthPoints => healthPoints;
    public int ShieldPoints => shieldPoints;
}
