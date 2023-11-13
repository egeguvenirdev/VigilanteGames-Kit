using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CoroutineManager 
{
    private static readonly Dictionary<float, WaitForSeconds> waitForSecondsPool = new Dictionary<float, WaitForSeconds>();

    public static WaitForSeconds GetTime(float seconds)
    {
        if (waitForSecondsPool.TryGetValue(seconds, out WaitForSeconds waitForSeconds))
        {
            return waitForSeconds;
        }
        else
        {
            var newWaitForSeconds = new WaitForSeconds(seconds);
            waitForSecondsPool.Add(seconds, newWaitForSeconds);
            Debug.Log($"WaitForSecondsPool Adding {seconds} Size {waitForSecondsPool.Count}");
            return newWaitForSeconds;
        }
    }

    public static void AddTime(float[] secondDurations)
    {
        foreach (var duration in secondDurations)
        {
            var newWaitForSeconds = new WaitForSeconds(duration);
            waitForSecondsPool.Add(duration, newWaitForSeconds);
        }
    }

    public static void RemoveTime(float seconds)
    {
        waitForSecondsPool.Remove(seconds);
    }

    public static void Clear()
    {
        waitForSecondsPool.Clear();
    }
}
