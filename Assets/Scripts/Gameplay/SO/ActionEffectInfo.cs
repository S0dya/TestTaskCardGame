using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "Game/Action Effect")]
public class ActionEffectInfo : ScriptableObject
{
    [SerializeField] private ActionEffectEnum actionEffect;
    [SerializeField] private Sprite sprite;
    [SerializeField] private int value;

    public ActionEffectEnum ActionEffect => actionEffect;
    public Sprite Sprite => sprite;
    public int Value => value;
}
