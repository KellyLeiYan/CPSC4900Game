using System.Collections;
using System.Collections.Generic;
// Scripts/Entities/EchoLifetime.cs
using UnityEngine;

public class EchoLifetime : MonoBehaviour
{
    public AmbienceState ambience;
    public float lifetime = 15f;

    void OnEnable()
    {
        if (ambience) ambience.SetFollowed(true);
        Invoke(nameof(End), lifetime);
    }

    void End()
    {
        if (ambience) ambience.SetFollowed(false);
        Destroy(gameObject);
    }
}
