using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ActionSystem : Singleton<ActionSystem>
// This class manages the flow of game actions and their associated reactions. It allows for the performance of actions, subscription to reactions, and handling of preformers for specific action types.
{
    private List<GameAction> reactions = null;// This list holds the current set of reactions being processed during the flow of an action.
    public bool IsPreforming { get; private set; } = false; // This property indicates whether an action is currently being performed, preventing re-entrant calls to the Perform method.
    private static Dictionary<Type, List<Action<GameAction>>> preSubs = new();// when you draw a card you can have a reaction that happens before that card's action can be triggered.
    private static Dictionary<Type, List<Action<GameAction>>> postSubs = new();// when you draw a card you can have a reaction  that happens after that card's action has been triggered.
    private static Dictionary<Type, Func<GameAction, IEnumerator>> preformers = new();
    private static Dictionary<Delegate, Action<GameAction>> wrappedDelegates = new(); // top of ActionSystem
    public void Perform(GameAction action, Action OnFlowFinished = null)
    {
        if (IsPreforming) return;
        IsPreforming = true;
        StartCoroutine(Flow(action, () =>
        {
            IsPreforming = false;
            OnFlowFinished?.Invoke();
        }));
    }

    public void AddReaction(GameAction gameAction)
    {
        if (reactions != null)
        {
            reactions.Add(gameAction);
        }
    }

    private IEnumerator Flow(GameAction action, Action OnFlowFinished = null)
    {
        reactions = action.PreReactions;
        PerformSubscribers(action, preSubs);
        yield return PerformReaction();

        reactions = action.PreformReactions;
        yield return PerformPerformer(action);
        yield return PerformReaction();

        reactions = action.PostReactions;
        PerformSubscribers(action, postSubs);
        yield return PerformReaction();

        OnFlowFinished?.Invoke();

    }



    private void PerformSubscribers(GameAction action, Dictionary<Type, List<Action<GameAction>>> subs)
    {
        Type type = action.GetType();
        if (subs.ContainsKey(type))
        {
            foreach (var sub in subs[type])
            {
                sub(action);
            }
        }
    }

    private IEnumerator PerformReaction()
    {
        foreach (var reaction in reactions)
        {
            yield return Flow(reaction);
        }
    }
    //Main method to perform an action, which initiates the flow of reactions and preformers associated with that action.
    public static void AttachPerformer<T>(Func<T, IEnumerator> performer) where T : GameAction
    {
        Type type = typeof(T);
        IEnumerator wrappedPerformer(GameAction action) => performer((T)action);
        if (preformers.ContainsKey(type)) preformers[type] = wrappedPerformer;
        else preformers.Add(type, wrappedPerformer);
    }

    public static void DetachPerformer<T>(GameAction reaction) where T : GameAction
    {
        Type type = typeof(T);
        if (preformers.ContainsKey(type)) preformers.Remove(type);

    }

    public static void SubscribeReaction<T>(Action<T> reaction, ReactionTiming timing) where T : GameAction
    {
        Dictionary<Type, List<Action<GameAction>>> subs = timing == ReactionTiming.PRE ? preSubs : postSubs;

        if (wrappedDelegates.ContainsKey(reaction)) return;

        Action<GameAction> wrappedReaction = action => reaction((T)action);
        wrappedDelegates[reaction] = wrappedReaction;

        if (!subs.TryGetValue(typeof(T), out var list))
        {
            list = new List<Action<GameAction>>();
            subs[typeof(T)] = list;
        }

        list.Add(wrappedReaction);
    }

    public static void UnsubscribeReaction<T>(Action<T> reaction, ReactionTiming timing) where T : GameAction
    {
        Dictionary<Type, List<Action<GameAction>>> subs = timing == ReactionTiming.PRE ? preSubs : postSubs;

        if (wrappedDelegates.TryGetValue(reaction, out var wrappedReaction))
        {
            if (subs.TryGetValue(typeof(T), out var list))
            {
                list.Remove(wrappedReaction);
            }

            wrappedDelegates.Remove(reaction);
        }
    }



    private IEnumerator PerformPerformer(GameAction action)
    {
        Type type = action.GetType();
        if (preformers.TryGetValue(type, out var performer))
        {
            yield return performer(action);
        }
    }
}
