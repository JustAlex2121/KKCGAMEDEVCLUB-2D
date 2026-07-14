using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Unity.VisualScripting;
using UnityEditor.PackageManager;

public class ActionSystem : Singleton<ActionSystem>
{
    private List<GameAction> reactions = null;
    public bool IsPerforming { get; private set; } = false;

    private static Dictionary<Type, List<Action<GameAction>>> postSubs = new();
  
    private static Dictionary<Type, List<Action<GameAction>>> preSubs = new();
   
    private static Dictionary<Type, Func<GameAction, IEnumerator>> performers = new();
    public void PreformAction(GameAction gameaction)
    {
        reactions?.Add(gameaction);
    }
    public static void AttachPerformer<T>(Func<GameAction, IEnumerator> performer) where T : GameAction
    {
        performers[typeof(T)] = performer;
    }
    public Static void detachPerformer<T>() where T : GameAction
    {
        performers.Remove(typeof(T));
    }
public static void SubscribeReac
}
