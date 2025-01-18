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

    [SerializeField] private TextMeshProUGUI characterHealthPoints;
    [SerializeField] private Image characterHealthBarImage;

    [SerializeField] private TextMeshProUGUI characterShieldPoints;

    public void SetCharacter(string name, Sprite sprite, int healthPoints, int shieldPoints)
    {
        characterName.text = name;
        characterImage.sprite = sprite;

        characterHealthPoints.text = healthPoints.ToString();
        characterShieldPoints.text = shieldPoints.ToString();
    }
    
    public void SetShield(int prevValue, int newValue)
    {
        if (newValue < 0) DebugManager.Log(DebugCategory.Gameplay, $"{newValue} must be positive", DebugStatus.Error);

        if (prevValue == 0 && newValue > 0)
        {
            //show
        }
        else if (prevValue > 0 && newValue == 0)
        {
            //remove
        }

        SetText(characterShieldPoints, newValue.ToString());
    }

    public void SetHealth(int prevValue, int newValue, int maxValue)
    {
        if (newValue < 0) DebugManager.Log(DebugCategory.Gameplay, $"{newValue} must be positive", DebugStatus.Error);

        characterHealthBarImage.fillAmount = newValue / maxValue;

        SetText(characterHealthPoints, newValue.ToString());
    }


    private void SetText(TextMeshProUGUI tmp, string text)
    {
        tmp.text = text;
    }
}
