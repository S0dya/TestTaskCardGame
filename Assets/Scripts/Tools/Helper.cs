using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

using Random = UnityEngine.Random;

public static class Helper
{
    
    public static void AdjustValueBetweenMinMax(int min, int max, ref int value) => value = Math.Max(min, Math.Min(value, max));
    public static int AdjustValueBetweenMinMax(int min, int max, int value) => value = Math.Max(min, Math.Min(value, max));

}
