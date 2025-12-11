using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkBackwardExit : MonoBehaviour, IInteractable
{
    public RuleEngine engine;
    public ExitGreen exit; // reference to ExitGreen on the EXIT prop (optional)

    public void OnInteract(){
        // First, send the “WalkBackward” action (can be a violation)
        var a = new PlayerAction { Type = PlayerActionType.WalkBackward, Violates = true };
        engine.Resolve(a, out _);

        // Then “use” the EXIT so the engine can apply Rule D window if both violations occurred
        var b = new PlayerAction { Type = PlayerActionType.UseGreenExit, Violates = false };
        var outcome = engine.Resolve(b, out var why);

        switch (outcome){
            case RuleOutcome.RouteTrueExit: NodeRouter.GoTo("F4"); break;
            case RuleOutcome.Loop: NodeRouter.GoTo("F0"); break;
            case RuleOutcome.Kill: DeathHandler.Kill(why); break;
            default: /* stay */ break;
        }
    }
}
