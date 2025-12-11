using System.Collections;
using System.Collections.Generic;
// Scripts/Entities/EchoController.cs
using UnityEngine;

public class EchoController : MonoBehaviour
{
    public Transform target;                        // set by EchoManager
    public Vector3 offset = new Vector3(-1.2f, -0.2f, 0f);
    public float followLerp = 3f;

    void Update()
    {
        if (!target) return;
        var want = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, want, followLerp * Time.deltaTime);
    }
}
