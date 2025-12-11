using System.Collections;
using System.Collections.Generic;
// Scripts/Rules/ConcreteRules.cs
using UnityEngine;

[CreateAssetMenu(menuName="Liminal/Rules/Rule A")]
public class RuleA : RuleSO {
    public override bool Applies(AmbienceState s) => true;
    public override RuleOutcome Evaluate(PlayerAction act, AmbienceState s, out string reason){
        reason = "Rule A";
        if (act.Type==PlayerActionType.OpenRedDoor) {
            return s.Static ? RuleOutcome.RouteTrueExit : RuleOutcome.Loop;
        }
        return RuleOutcome.None;
    }
}

[CreateAssetMenu(menuName="Liminal/Rules/Rule B")]
public class RuleB : RuleSO {
    public override bool Applies(AmbienceState s) => s.Hum;
    public override RuleOutcome Evaluate(PlayerAction act, AmbienceState s, out string reason){
        reason="Rule B";
        if (act.Type==PlayerActionType.LookUp && s.Hum) return RuleOutcome.Kill;
        return RuleOutcome.None;
    }
}

[CreateAssetMenu(menuName="Liminal/Rules/Rule C")]
public class RuleC : RuleSO
{
    // Applies only when you're being followed (Echo present).
    public override bool Applies(AmbienceState s) => s.Followed;

    // Design: "If you are followed, walk backward to be left alone."
    // In Chapter 1 we also allow this to open a route (used for Path 3).
    public override RuleOutcome Evaluate(PlayerAction act, AmbienceState s, out string reason)
    {
        reason = "Rule C";
        if (!s.Followed) return RuleOutcome.None;

        if (act.Type == PlayerActionType.WalkBackward)
        {
            // Let Rule C explicitly grant a success route when you're followed.
            return RuleOutcome.RouteTrueExit;
        }

        // Otherwise C doesn't block/kill; it just waits for the backward action.
        return RuleOutcome.None;
    }
}

[CreateAssetMenu(menuName="Liminal/Rules/Rule D")]
public class RuleD : RuleSO
{
    // Rule D ("Two wrongs make a right") is enforced by RuleEngine's timing window.
    // This asset mainly exists so you can list/show it in your codex/UI.
    public override bool Applies(AmbienceState s) => true;

    public override RuleOutcome Evaluate(PlayerAction act, AmbienceState s, out string reason)
    {
        reason = "Rule D";
        // No direct effect here; the RuleEngine checks for two violations within a window
        // and returns RouteTrueExit when that happens.
        return RuleOutcome.None;
    }
}
