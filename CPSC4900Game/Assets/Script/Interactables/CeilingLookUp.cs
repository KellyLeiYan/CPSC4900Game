using System.Collections;
using System.Collections.Generic;
// Scripts/Interactables/CeilingLookUp.cs
using UnityEngine;

public class CeilingLookUp : MonoBehaviour, IInteractable
{
    public RuleEngine engine;  // drag RuleSystem here in Inspector

    public void OnInteract(){
        var act = new PlayerAction { Type = PlayerActionType.LookUp, Violates = true };
        var outcome = engine.Resolve(act, out var why);

        if (outcome == RuleOutcome.Kill){
            DeathHandler.Kill(why);   // <- this loads GameOver and shows your hint
            return;
        }

    }
}
