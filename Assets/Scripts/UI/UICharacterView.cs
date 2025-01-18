using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class UICharacterView : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private Image characterImage;
    [Space(20)]
    [SerializeField] private TextMeshProUGUI characterHealthPoints;
    [SerializeField] private Image characterHealthBarImage;
    [Space(20)]
    [SerializeField] private GameObject shieldObject;
    [SerializeField] private TextMeshProUGUI characterShieldPoints;

    private void Awake()
    {
        shieldObject.SetActive(false);
    }

    public void SetCharacter(string name, Sprite sprite, int healthPoints, int shieldPoints)
    {
        characterName.text = name;
        characterImage.sprite = sprite;

        SetHealth(healthPoints, healthPoints);
        if (shieldPoints > 0) SetShield(0, shieldPoints);
    }
    
    public void SetShield(int prevValue, int newValue)
    {
        if (newValue < 0) DebugManager.Log(DebugCategory.Gameplay, $"{newValue} must be positive", DebugStatus.Error);

        if (prevValue == 0 && newValue > 0)
        {
            shieldObject.SetActive(true);
        }
        else if (prevValue > 0 && newValue == 0)
        {
            shieldObject.SetActive(false);
        }

        SetText(characterShieldPoints, newValue.ToString());
    }

    public void SetHealth(int prevValue, int newValue, int maxValue)
    {
        if (prevValue > newValue)
        {
            //play effects
        }

        SetHealth(newValue, maxValue);
    }
    public void SetHealth(int newValue, int maxValue)
    {
        if (newValue < 0) DebugManager.Log(DebugCategory.Gameplay, $"{newValue} must be positive", DebugStatus.Error);

        characterHealthBarImage.fillAmount = newValue / maxValue;

        SetText(characterHealthPoints, $"{newValue} / {maxValue}");
    }


    private void SetText(TextMeshProUGUI tmp, string text)
    {
        tmp.text = text;
    }
}
