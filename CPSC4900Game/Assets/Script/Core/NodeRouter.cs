using System.Collections;
using System.Collections.Generic;
// Scripts/Core/NodeRouter.cs
using System;            // for Action<>
using UnityEngine;

public class NodeRouter : MonoBehaviour
{
    public static NodeRouter I;

    [Header("Assign Node_F0..F4 here (same order as ids)")]
    public Transform[] nodes;                    // Node_F0 .. Node_F4

    // Keep ids aligned with nodes order
    static readonly string[] ids = { "F0", "F1", "F2", "F3", "F4" };

    // Fired after every successful switch: (fromId, toId)
    public static event Action<string, string> OnNodeChanged;

    int current = 0; // index into nodes/ids arrays

    void Awake()
    {
        I = this;

        // Start clean: deactivate all, then activate F0 only
        if (nodes != null)
        {
            for (int i = 0; i < nodes.Length; i++)
                if (nodes[i]) nodes[i].gameObject.SetActive(false);
        }

        Show(0); // start on F0
    }

    public static string CurrentId => (I && I.current >= 0 && I.current < ids.Length) ? ids[I.current] : "F0";

    public static void GoTo(string id)
    {
        if (!I) return;

        int idx = id switch
        {
            "F0" => 0,
            "F1" => 1,
            "F2" => 2,
            "F3" => 3,
            "F4" => 4,
            _    => 0
        };

        I.Show(idx);
    }

    public void GoNext()
    {
        int next = current + 1;
        if (next < nodes.Length) Show(next);
    }

    public void GoPrev()
    {
        int prev = current - 1;
        if (prev >= 0) Show(prev);
    }

    void Show(int idx){
        if (nodes == null || nodes.Length == 0) return;
        if (idx < 0 || idx >= nodes.Length) return;

        string fromId = (current >= 0 && current < ids.Length) ? ids[current] : null;
        string toId   = ids[idx];

        for (int i = 0; i < nodes.Length; i++)
            if (nodes[i]) nodes[i].gameObject.SetActive(i == idx);

        current = idx;

        // fire event (even if fromId is null on first show)
        OnNodeChanged?.Invoke(fromId ?? "", toId);
    }
}

// Scripts/Core/DeathHandler.cs 
public static class DeathHandler
{
    public static void Kill(string hint)
    {
        PlayerPrefs.SetString("LastHint", hint ?? "You died.");
        PlayerPrefs.Save();
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
    }
}
