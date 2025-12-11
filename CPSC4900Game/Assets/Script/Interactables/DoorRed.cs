using System.Collections;
using System.Collections.Generic;
// Scripts/Interactables/DoorRed.cs
// Scripts/Interactables/DoorRed.cs
using UnityEngine;

public class DoorRed : MonoBehaviour, IInteractable
{
    public RuleEngine engine;   // drag from RuleSystem in scene
    public ClipSwap swap;       // drag ClipSwap on this object
    public float openThenProcessDelay = 0.15f; // small visual beat

    bool busy;

    public void OnInteract()
    {
        if (!busy) StartCoroutine(OpenThenProcess());
    }

    IEnumerator OpenThenProcess()
    {
        busy = true;

        // 1) Visual: open frame first
        if (swap) swap.ShowOpen();

        // small delay so the player perceives the open
        if (openThenProcessDelay > 0f)
            yield return new WaitForSeconds(openThenProcessDelay);

        // 2) Logic: ask RuleEngine what happens
        var action = new PlayerAction { Type = PlayerActionType.OpenRedDoor, Violates = false };
        var outcome = engine.Resolve(action, out var why);

        // 3) Route based on outcome
        switch (outcome)
        {
            case RuleOutcome.RouteTrueExit:
                NodeRouter.GoTo("F4");               // success path
                break;
            case RuleOutcome.Loop:
                NodeRouter.GoTo("F0");               // sends you back to start
                if (swap) swap.ShowClosed();         // close again if we stayed in this scene
                break;
            case RuleOutcome.Kill:
                DeathHandler.Kill(why);              // game over
                break;
            default:
                // locked feedback if no rule outcome (optional SFX)
                if (swap) swap.ShowClosed();
                break;
        }

        busy = false;
    }
}

