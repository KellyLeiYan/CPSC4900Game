using System.Collections;
using System.Collections.Generic;
// Scripts/Interactables/ExitGreen.cs
using UnityEngine;

public class ExitGreen : MonoBehaviour, IInteractable
{
    public RuleEngine engine;
    public ClipSwap swap;              // optional if you want an "open" frame
    public float openThenProcessDelay = 0.12f;

    bool busy;

    public void OnInteract()
    {
        if (!busy) StartCoroutine(OpenThenProcess());
    }

    System.Collections.IEnumerator OpenThenProcess()
    {
        busy = true;
        if (swap) swap.ShowOpen();
        if (openThenProcessDelay > 0f)
            yield return new WaitForSeconds(openThenProcessDelay);

        var action = new PlayerAction { Type = PlayerActionType.UseGreenExit, Violates = false };
        var outcome = engine.Resolve(action, out var why);

        switch (outcome)
        {
            case RuleOutcome.RouteTrueExit:
                NodeRouter.GoTo("F4");  // only when Rule D says so in your engine
                break;
            case RuleOutcome.Loop:
                NodeRouter.GoTo("F0");  // liar's loop, if that's how you set it up
                if (swap) swap.ShowClosed();
                break;
            case RuleOutcome.Kill:
                DeathHandler.Kill(why);
                break;
            default:
                if (swap) swap.ShowClosed();
                break;
        }

        busy = false;
    }
}
