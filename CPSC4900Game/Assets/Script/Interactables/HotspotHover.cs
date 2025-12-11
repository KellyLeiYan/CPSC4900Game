using System.Collections;
using System.Collections.Generic;
// Scripts/Interactables/HotspotHover.cs
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HotspotHover : MonoBehaviour
{
    public SpriteRenderer glow;   // drag the Glow child here
    public float fadeSpeed = 10f;
    float targetA = 0f;

    void OnMouseEnter(){ targetA = 0.35f; }
    void OnMouseExit(){ targetA = 0f; }

    void Update(){
        if (!glow) return;
        var c = glow.color;
        c.a = Mathf.MoveTowards(c.a, targetA, fadeSpeed * Time.deltaTime);
        glow.color = c;
    }
}
