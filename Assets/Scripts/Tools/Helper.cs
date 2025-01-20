using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public static class Helper
{
    public static void Shuffle<T>(this IList<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    static public void SetText(TextMeshProUGUI tmp, string text)
    {
        tmp.text = text;
    }

    static public void AddEventToEventTrigger(EventTrigger eventTrigger, Action action, EventTriggerType eventTriggerType)
    {
        var pointerUpEntry = new EventTrigger.Entry
        {
            eventID = eventTriggerType
        };
        pointerUpEntry.callback.AddListener(_ => action?.Invoke());
        eventTrigger.triggers.Add(pointerUpEntry);
    }
}
