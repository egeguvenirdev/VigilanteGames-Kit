using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public static class ActionManager
{
    //Game Actions
    public static Action GameStart { get; set; }

    public static Action<bool> GameEnd { get; set; }

    public static Action<Vector3> ManagerUpdate { get; set; }

    public static void ResetAllStaticVariables()
    {
        Type type = typeof(ActionManager);
        var fields = type.GetFields(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.Public);
        foreach (var fieldInfo in fields)
        {
            fieldInfo.SetValue(null, GetDefault(type));
        }
    }

    public static object GetDefault(Type type)
    {
        if (type.IsValueType)
        {
            return Activator.CreateInstance(type);
        }
        return null;
    }
}
