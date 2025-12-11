using System.Collections;
using System.Collections.Generic;
// Scripts/Core/RuleEngine.cs
using UnityEngine;

public class RuleEngine : MonoBehaviour
{
    public AmbienceState ambience;
    public List<RuleSO> activeRules;

    [SerializeField] float twoWrongsWindow = 2f;

    // Track the last violation
    bool hasLastViolation = false;
    PlayerActionType lastViolationType;
    float lastViolationTime;

    public RuleOutcome Resolve(PlayerAction action, out string reason)
    {
        reason = "";

        // --- (1) Two-wrongs short-circuit: LookUp@HUM then OpenRedDoor@noSTATIC ---
        // If current is the door click and it violates Rule A *and* the last violation
        // was a LookUp under HUM within the window, upgrade to True Exit.
        if (IsViolation_OpenRedDoor_NoStatic(action))
        {
            if (hasLastViolation &&
                lastViolationType == PlayerActionType.LookUp &&
                (Time.time - lastViolationTime) <= twoWrongsWindow)
            {
                // consume the stored violation
                hasLastViolation = false;
                reason = "Rule D";
                return RuleOutcome.RouteTrueExit;
            }
        }

        // --- (2) Buffer the first wrong: LookUp while HUM (don't kill now) ---
        if (IsViolation_LookUp_Hum(action))
        {
            hasLastViolation = true;
            lastViolationType = PlayerActionType.LookUp;
            lastViolationTime = Time.time;
            // Important: return None so Rule B doesn't kill immediately.
            return RuleOutcome.None;
        }

        // --- (3) Normal rule evaluation (A/B/C etc.) ---
        RuleOutcome final = RuleOutcome.None;

        foreach (var r in activeRules)
        {
            if (!r) continue;
            if (!r.Applies(ambience)) continue;

            var res = r.Evaluate(action, ambience, out var why);
            if (res == RuleOutcome.Kill) { reason = why; return res; }
            if (res != RuleOutcome.None) { final = res; reason = why; }
        }

        return final;
    }

    // Helpers to define the two specific "wrongs"
    bool IsViolation_LookUp_Hum(PlayerAction a)
        => a.Type == PlayerActionType.LookUp && ambience.Hum;

    bool IsViolation_OpenRedDoor_NoStatic(PlayerAction a)
        => a.Type == PlayerActionType.OpenRedDoor && !ambience.Static;
}
