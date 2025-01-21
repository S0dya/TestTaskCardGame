using System;
using UnityEngine;

[Serializable]
public class ActionEffectData
{
    [SerializeField] private ActionEffectEnum actionEffect;
    [SerializeField] private Sprite sprite;
    [SerializeField] private int value;

    public ActionEffectEnum ActionEffect => actionEffect;
    public Sprite Sprite => sprite;
    public int Value => value;

    public ActionEffectData(ActionEffectEnum actionEffect, Sprite sprite, int value)
    {
        this.actionEffect = actionEffect;
        this.sprite = sprite;
        this.value = value;
    }
}
