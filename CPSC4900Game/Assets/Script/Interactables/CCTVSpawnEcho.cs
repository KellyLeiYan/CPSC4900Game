using System.Collections.Generic;
// Scripts/Interactables/CCTVSpawnEcho.cs
using UnityEngine;
using System.Collections;

public class CCTVSpawnEcho : MonoBehaviour
{
    [Header("Data")]
    public AmbienceState ambience;
    public GameObject echoPrefab;
    public Transform echoTarget;                  // drag Main Camera (or Player proxy)
    public float followDuration = 10f;
    public Vector3 offsetFromCamera = new Vector3(-1.0f, -0.2f, 0f);

    // session-scoped counters (reset each Play)
    static int f1Visits;
    static bool echoSpawned;

    void Awake(){
        f1Visits = 0;
        echoSpawned = false;
    }

    void OnEnable()
    {
        // Called each time Node_F1 is enabled (since you toggle nodes)
        f1Visits++;

        if (echoSpawned) return;

        // Spawn ONLY on the second time F1 becomes active
        if (f1Visits == 2)
        {
            StartCoroutine(SpawnAndFollow());
            echoSpawned = true;
        }
    }

    IEnumerator SpawnAndFollow()
    {
        var cam = Camera.main;
        var spawnPos = (cam ? cam.transform.position : Vector3.zero) + offsetFromCamera;
        spawnPos.z = 0f;

        var echo = Instantiate(echoPrefab, spawnPos, Quaternion.identity);

        var ec = echo.GetComponent<EchoController>();
        var follow = echoTarget ? echoTarget : cam?.transform;
        if (ec && follow) ec.target = follow;

        // ensure Echo cleans up even if F1 disables
        var life = echo.GetComponent<EchoLifetime>();
        if (life){
            life.ambience = ambience;
            life.lifetime = followDuration;
        } else {
            // fallback if EchoLifetime not added
            ambience.SetFollowed(true);
            Destroy(echo, followDuration);
            yield return new WaitForSeconds(followDuration);
            ambience.SetFollowed(false);
        }
    }
}
