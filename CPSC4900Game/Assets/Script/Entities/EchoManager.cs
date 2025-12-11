using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoManager : MonoBehaviour
{
    [Header("Data")]
    public AmbienceState ambience;          // drag Ambience_Chapter1
    public GameObject echoPrefab;           // Echo prefab (EchoController + optional EchoLifetime)
    public Transform echoTarget;            // Main Camera (or a Player proxy that mirrors camera)
    public Vector3 offsetFromCamera = new Vector3(-1f, -0.2f, 0f);

    [Header("Lifetime Safety Cap")]
    public float hardCapSeconds = 15f;      // max time Echo can exist if nothing else despawns it

    [Header("Rate Limiting (optional)")]
    [Tooltip("0 = unlimited; 1 = spawn once; 2 = twice; etc.")]
    public int maxSpawnsPerSession = 1;
    [Tooltip("Seconds before Echo can spawn again after despawn. Set 0 to disable.")]
    public float cooldownSeconds = 0f;
    [Tooltip("0/1 to ignore; otherwise spawn on every Nth return to F1 after the first loop.")]
    public int spawnEveryNthReturn = 0;

    // Internal state
    bool hasLoopedOnce = false;   // becomes true after first F1 -> F0
    int  spawnsThisSession = 0;
    int  f1Visits = 0;            // count arrivals to F1
    float _lastDespawnAt = -999f;

    GameObject echoInstance;
    Coroutine capRoutine;

    void OnEnable()  { NodeRouter.OnNodeChanged += HandleNodeChange; }
    void OnDisable() { NodeRouter.OnNodeChanged -= HandleNodeChange; }

    void HandleNodeChange(string fromId, string toId)
    {
        // Count every arrival to F1 (used by the Nth-return gate)
        if (toId == "F1") f1Visits++;

        // Mark that we've looped once the first time we actually go back to F0 from F1
        if (fromId == "F1" && toId == "F0")
            hasLoopedOnce = true;

        // DESPAWN rules
        if ((toId == "F0" || toId == "F4") && echoInstance != null)
        {
            DespawnEcho();
            return;
        }

        // SPAWN rule: returning to F1 after having looped once, no Echo currently alive
        if (fromId == "F0" && toId == "F1" && hasLoopedOnce && echoInstance == null)
        {
            if (PassesRateLimits())
                SpawnEcho();
        }
    }

    bool PassesRateLimits()
    {
        // 1) per-session cap
        bool underCap = (maxSpawnsPerSession == 0) || (spawnsThisSession < maxSpawnsPerSession);

        // 2) cooldown
        bool cooled = (cooldownSeconds <= 0f) || ((Time.time - _lastDespawnAt) >= cooldownSeconds);

        // 3) every-Nth return (ignore if 0 or 1)
        bool nthOk = true;
        if (spawnEveryNthReturn >= 2)
        {
            // f1Visits == 1 : first-ever arrival to F1 (no)
            // f1Visits == 2 : first return after loop (count as 0 in the modulo)
            int returnsAfterFirst = Mathf.Max(0, f1Visits - 2);
            nthOk = (returnsAfterFirst % spawnEveryNthReturn) == 0;
        }

        return underCap && cooled && nthOk;
    }

    void SpawnEcho()
    {
        var cam = Camera.main;
        Vector3 pos = (cam ? cam.transform.position : Vector3.zero) + offsetFromCamera;
        pos.z = 0f;

        echoInstance = Instantiate(echoPrefab, pos, Quaternion.identity);

        // Follow target (camera or proxy)
        var ec = echoInstance.GetComponent<EchoController>();
        var follow = echoTarget ? echoTarget : cam?.transform;
        if (ec && follow) ec.target = follow;

        // Turn on Followed (EchoLifetime will also set this if present; redundant is fine)
        ambience.SetFollowed(true);

        // Give prefab a self-cap if it has EchoLifetime
        var life = echoInstance.GetComponent<EchoLifetime>();
        if (life)
        {
            life.ambience = ambience;
            life.lifetime = hardCapSeconds;
        }
        else
        {
            // Manager-enforced safety cap
            if (capRoutine != null) StopCoroutine(capRoutine);
            capRoutine = StartCoroutine(HardCap());
        }

        spawnsThisSession++;
    }

    IEnumerator HardCap()
    {
        yield return new WaitForSeconds(hardCapSeconds);
        if (echoInstance) DespawnEcho();
    }

    void DespawnEcho()
    {
        ambience.SetFollowed(false);
        if (capRoutine != null) { StopCoroutine(capRoutine); capRoutine = null; }
        if (echoInstance) Destroy(echoInstance);
        echoInstance = null;
        _lastDespawnAt = Time.time;  // start cooldown
    }
}
