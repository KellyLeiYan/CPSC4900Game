using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scripts/Audio/VentStaticBurst.cs
public class VentStaticBurst : MonoBehaviour, IInteractable
{
    public AmbienceState ambience;     // drag Ambience_Chapter1
    public AudioSource staticSource;   // 2D loop, Loop ON, start Muted

    public float burstDuration = 1.8f; // how long the static stays ON

    bool isBursting = false;           // prevents overlap

    // Called when the player clicks this object
    public void OnInteract()
    {
        if (!isBursting)
            StartCoroutine(SingleBurst());
    }

    IEnumerator SingleBurst()
    {
        isBursting = true;

        // STATIC ON
        ambience.SetStatic(true);
        if (staticSource) staticSource.mute = false;

        yield return new WaitForSeconds(burstDuration);

        // STATIC OFF
        ambience.SetStatic(false);
        if (staticSource) staticSource.mute = true;

        isBursting = false;
    }

    void OnDisable()
    {
        // failsafe reset if object disables mid-burst
        ambience.SetStatic(false);
        if (staticSource) staticSource.mute = true;
        isBursting = false;
    }
}
