// For now, enums and general utils 

using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum ResourceType
{
    Money = 1,
    Personnel = 2,
    Facilities = 3
}

public enum BoardMemberType 
{
    Owner = 1, // likes all three resources equally
    HR = 2, // likes employees the most
    FacilitiesManager = 3, // likes facilities the most
    Accountant = 4 // likes money the most
}

public enum HappinessLevel
{
    Mad = 0, 
    Unhappy = 1, 
    Ok = 2, 
    Happy = 3 
}

public static class Utils
{
    // Fisher-Yates shuffle
    public static void Shuffle<T>(IList<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int r = Random.Range(0, i + 1);
            (list[i], list[r]) = (list[r], list[i]);
        }
    }

    public static float EaseInOutBack(float number)
    {
        const float c1 = 1.70158f;
        const float c2 = c1 * 1.525f;

        return number < 0.5f
            ? (Mathf.Pow(2f * number, 2f) * ((c2 + 1f) * 2f * number - c2)) / 2.0f
            : (Mathf.Pow(2f * number - 2f, 2f) * ((c2 + 1f) * (number * 2 - 2) + c2) + 2) / 2.0f;
    }

    public static float EaseInOutCubic(float x)
    {
        return x < 0.5 ? 4 * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 3) / 2;
    }

    public static Func<float, float> PickRandomEase()
    {
        List<Func<float, float>> eases = new List<Func<float, float>>();
        eases.Add(EaseInOutBack);
        eases.Add(EaseInOutCubic);
        eases.Add(EaseInOutCirc);
        eases.Add(EaseInOutBounce);
        eases.Add(EaseOutBounce);
        return eases[Random.Range(0, eases.Count - 1)];
    }

    public static float EaseOutBounce(float x)
    {
        var n1 = 7.5625f;
        var d1 = 2.75f;

        if (x < 1 / d1)
        {
            return n1 * x * x;
        }

        if (x < 2 / d1)
        {
            return n1 * (x -= 1.5f / d1) * x + 0.75f;
        }

        if (x < 2.5 / d1)
        {
            return n1 * (x -= 2.25f / d1) * x + 0.9375f;
        }

        return n1 * (x -= 2.625f / d1) * x + 0.984375f;
    }

    public static float EaseInOutBounce(float x)
    {
        return x < 0.5
            ? (1 - EaseOutBounce(1 - 2 * x)) / 2
            : (1 + EaseOutBounce(2 * x - 1)) / 2;
    }
    
    public static float EaseInOutCirc(float x)
    {
        return x < 0.5
            ? (1 - Mathf.Sqrt(1 - Mathf.Pow(2 * x, 2))) / 2
            : (Mathf.Sqrt(1 - Mathf.Pow(-2 * x + 2, 2)) + 1) / 2;
    }
}