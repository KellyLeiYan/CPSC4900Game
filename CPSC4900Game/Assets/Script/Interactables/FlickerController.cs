using System.Collections;
using System.Collections.Generic;
// Assets/Scripts/FX/FlickerController.cs
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FlickerController : MonoBehaviour
{
    public float speed = 8f;       // how fast it flickers
    public float amplitude = 0.12f;// how strong the flicker is
    public float baseAlpha = 1f;   // base brightness/alpha

    SpriteRenderer sr;
    float t0;

    void Awake(){
        sr = GetComponent<SpriteRenderer>();
        t0 = Random.value * 100f; // desync multiple lights
    }

    void Update(){
        // simple alpha pulse; swap to color if you prefer brightness via material
        float pulse = (Mathf.Sin((Time.time + t0) * speed) * 0.5f + 0.5f) * amplitude;
        var c = sr.color;
        c.a = Mathf.Clamp01(baseAlpha * (1f - amplitude) + pulse);
        sr.color = c;
    }

    public void SetEnabled(bool on){
        enabled = on;
        if (!on){
            var c = sr.color; c.a = baseAlpha; sr.color = c;
        }
    }
}
