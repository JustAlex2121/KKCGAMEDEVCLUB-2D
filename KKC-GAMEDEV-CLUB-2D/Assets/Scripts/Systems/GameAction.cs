using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using NUnit.Framework;

public abstract class GameAction
{
    public List<GameAction> PreReactions { get; private set; } = new();
    public List<GameAction> PerformReactions { get; private set; } = new();
    public List<GameAction> PostReactions { get; private set; } = new();
}

// Update is called once per frame
/*void Update()
{
        
}*/

