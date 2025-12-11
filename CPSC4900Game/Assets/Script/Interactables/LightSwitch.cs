using System.Collections;
using System.Collections.Generic;
// Scripts/Interactables/LightSwitch.cs
using UnityEngine;

public class LightSwitch : MonoBehaviour, IInteractable
{
    public AmbienceState ambience;    // drag Ambience_Chapter1
    public AudioSource humSource;     // optional: looping hum AudioSource
    public FlickerController flicker; // optional: your flicker script

    public bool humOnAtStart = true;

    void Start(){
        ambience.SetHum(humOnAtStart);
        if (humSource) humSource.mute = !humOnAtStart;
        if (flicker) flicker.SetEnabled(humOnAtStart);
    }

    public void OnInteract(){
        bool next = !ambience.Hum;
        ambience.SetHum(next);
        if (humSource) humSource.mute = !next;
        if (flicker) flicker.SetEnabled(next);
        // (optional) play click SFX
    }
}
