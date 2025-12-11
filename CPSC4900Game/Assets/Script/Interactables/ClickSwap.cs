using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scripts/Interactables/ClipSwap.cs
[RequireComponent(typeof(SpriteRenderer))]
public class ClipSwap : MonoBehaviour
{
    [Tooltip("frames[0] = closed, frames[1] = open")]
    public Sprite[] frames;

    SpriteRenderer sr;

    void Awake()
    {
        // Get or cache the SpriteRenderer
        sr = GetComponent<SpriteRenderer>();

        // Start in closed state if possible
        if (frames != null && frames.Length > 0 && frames[0] != null)
        {
            sr.sprite = frames[0];
        }
    }

    public void SetFrame(int index)
    {
        if (sr == null)
        {
            sr = GetComponent<SpriteRenderer>();
            if (sr == null)
            {
                Debug.LogError("[ClipSwap] No SpriteRenderer found on this GameObject.", this);
                return;
            }
        }

        if (frames == null || frames.Length == 0)
        {
            Debug.LogError("[ClipSwap] Frames array is null or empty.", this);
            return;
        }

        if (index < 0 || index >= frames.Length)
        {
            Debug.LogError($"[ClipSwap] Frame index {index} is out of range. frames.Length = {frames.Length}", this);
            return;
        }

        if (frames[index] == null)
        {
            Debug.LogError($"[ClipSwap] frames[{index}] is null. Did you assign all sprites?", this);
            return;
        }

        sr.sprite = frames[index];
    }

    public void ShowClosed()
    {
        SetFrame(0);
    }

    public void ShowOpen()
    {
        SetFrame(1);
    }
}
