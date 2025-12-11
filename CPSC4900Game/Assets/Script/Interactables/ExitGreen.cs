using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scripts/Interactables/ExitGreen.cs
public class ExitGreen : MonoBehaviour, IInteractable
{
    public RuleEngine engine;
    public ClipSwap swap;                 // frames[0]=closed, frames[1]=open
    public GameObject outdoorImage;       // assign an "outside" sprite/GO to enable on success
    public float openThenProcessDelay = 0.12f;

    bool busy;

    void Awake()
    {
        // Make sure door starts closed
        if (swap) swap.ShowClosed();

        // Make sure the outdoor image starts hidden
        if (outdoorImage) outdoorImage.SetActive(false);
    }

    public void OnInteract()
    {
        if (!busy)
            StartCoroutine(TryOpen());
    }

    IEnumerator TryOpen()
    {
        busy = true;

        var action  = new PlayerAction { Type = PlayerActionType.UseGreenExit, Violates = false };
        var outcome = engine.Resolve(action, out var why);

        Debug.Log($"[ExitGreen] outcome={outcome}, why={why}, outdoorImageAssigned={(outdoorImage != null)}");

        switch (outcome)
        {
            case RuleOutcome.RouteTrueExit:
                // ✅ Only on TRUE EXIT do we open the door + show outside
                if (swap) swap.ShowOpen();

                if (openThenProcessDelay > 0f)
                    yield return new WaitForSeconds(openThenProcessDelay);

                if (outdoorImage)
                {
                    outdoorImage.SetActive(true);
                    Debug.Log("[ExitGreen] outdoorImage.SetActive(true) called.");
                }

                // Optional: prevent further clicks on the door
                var col = GetComponent<Collider2D>();
                if (col) col.enabled = false;
                break;

            case RuleOutcome.Loop:
                if (swap) swap.ShowClosed();
                NodeRouter.GoTo("F0");
                break;

            case RuleOutcome.Kill:
                if (swap) swap.ShowClosed();
                DeathHandler.Kill(why);
                break;

            default:
                // stay closed, maybe play locked sound later
                if (swap) swap.ShowClosed();
                break;
        }

        busy = false;
    }
}
