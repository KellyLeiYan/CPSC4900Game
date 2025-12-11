using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ClipSwap : MonoBehaviour
{
    public Sprite[] frames; // 0=closed, 1=open (add more if needed)
    SpriteRenderer sr;

    void Awake(){ sr = GetComponent<SpriteRenderer>(); }

    public void SetFrame(int index)
    {
        if (frames == null || frames.Length == 0) return;
        index = Mathf.Clamp(index, 0, frames.Length - 1);
        sr.sprite = frames[index];
    }

    public void ShowClosed() => SetFrame(0);
    public void ShowOpen()   => SetFrame(1);
}

