using System.Collections;
using System.Collections.Generic;
// Scripts/Interactables/VentStaticBurst.cs
using UnityEngine;
using System.Collections;

public class VentStaticBurst : MonoBehaviour
{
    public AmbienceState ambience;      // drag Ambience_Chapter1
    public AudioSource staticSource;    // looping Static_Loop.wav (start muted)
    public Vector2 burstEverySeconds = new Vector2(2.0f, 4.0f);
    public float burstDuration = 1.8f;

    void Start(){ StartCoroutine(BurstLoop()); }

    IEnumerator BurstLoop(){
        while(true){
            yield return new WaitForSeconds(Random.Range(burstEverySeconds.x, burstEverySeconds.y));
            ambience.SetStatic(true);
            if (staticSource) staticSource.mute = false;

            yield return new WaitForSeconds(burstDuration);
            ambience.SetStatic(false);
            if (staticSource) staticSource.mute = true;
        }
    }
}
