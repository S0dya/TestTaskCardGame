using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Game/Character")]
public class CharacterInfo : ScriptableObject
{
    [SerializeField] private string characterName;
    [SerializeField] private Sprite sprite;

    [SerializeField] private int healthPoints;
    [SerializeField] private int shieldPoints;

    public string CharacterName => characterName;
    public Sprite Sprite => sprite;
    public int HealthPoints => healthPoints;
    public int ShieldPoints => shieldPoints;
}
