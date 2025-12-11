using System.Collections;
using System.Collections.Generic;
// Scripts/Rules/RuleSO.cs
using UnityEngine;

public enum RuleOutcome { None, Allow, RouteTrueExit, Loop, Kill }

public abstract class RuleSO : ScriptableObject {
    [TextArea] public string description;
    public abstract bool Applies(AmbienceState s);
    // Called when a player attempts an action (e.g., open door, look up)
    public abstract RuleOutcome Evaluate(PlayerAction action, AmbienceState s, out string reason);
}

public enum PlayerActionType { OpenRedDoor, UseGreenExit, LookUp, WalkBackward, UseCamera }

public struct PlayerAction {
    public PlayerActionType Type;
    public bool Violates; // set by caller if the player's intent is to break a posted rule
}
