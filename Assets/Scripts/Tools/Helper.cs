using System.Collections.Generic;
using TMPro;

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

    public static void SetText(TextMeshProUGUI tmp, string text)
    {
        tmp.text = text;
    }
}
