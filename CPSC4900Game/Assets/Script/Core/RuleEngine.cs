using System.Collections;
using System.Collections.Generic;
// Scripts/Core/RuleEngine.cs
using UnityEngine;

public class RuleEngine : MonoBehaviour {
    public AmbienceState ambience;
    public List<RuleSO> activeRules;
    private float violationWindow = 2f;
    private List<PlayerActionType> recentViolations = new();

    public RuleOutcome Resolve(PlayerAction action, out string reason) {
        reason = "";
        RuleOutcome final = RuleOutcome.None;

        foreach (var r in activeRules) {
            if (!r.Applies(ambience)) continue;
            var res = r.Evaluate(action, ambience, out var why);
            if (res == RuleOutcome.Kill) { reason = why; return res; }
            if (res != RuleOutcome.None) { final = res; reason = why; }
        }

        // Rule D: exactly two violations in window → True Exit
        if (action.Violates) {
            recentViolations.Add(action.Type);
            Invoke(nameof(FlushViolations), violationWindow);
            if (recentViolations.Count == 2) {
                recentViolations.Clear();
                reason = "Rule D";
                return RuleOutcome.RouteTrueExit;
            }
        }

        return final;
    }
    void FlushViolations(){ recentViolations.Clear(); }
}
