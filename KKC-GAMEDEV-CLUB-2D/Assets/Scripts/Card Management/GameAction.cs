using UnityEngine;
using System.Collections.Generic;

public abstract class GameAction
    // This class represents an action that can be performed in the game. It contains lists of reactions that can be triggered before, during, and after the action is performed.
{
    public List<GameAction> PreReactions { get; private set; } = new();
    public List<GameAction> PreformReactions { get; private set; } = new();
    public List<GameAction> PostReactions { get; private set; } = new();
}
