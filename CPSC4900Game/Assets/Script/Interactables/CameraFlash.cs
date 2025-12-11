using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlash : MonoBehaviour, IInteractable
{
    public AmbienceState ambience;

    public void OnInteract(){
        if (ambience.Hum && ambience.Static){
            // optional: play slip SFX / sprinkler VFX trigger here
            DeathHandler.Kill("Bright flashes agitate the sprinklers under hum and static.");
        }
        else {
            // harmless flash feedback
            // (play a small ‘click’ or short white overlay)
        }
    }
}
