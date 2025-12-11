using System.Collections;
using System.Collections.Generic;
// Scripts/Core/NodeRouter.cs
// Scripts/Core/NodeRouter.cs
using UnityEngine;

public class NodeRouter : MonoBehaviour
{
    public static NodeRouter I;

    public Transform[] nodes; // Assign Node_F0..F4 in the Inspector
    int current = 0;

    void Awake()
    {
        I = this;
        Show(0); // start on F0
    }

    public static void GoTo(string id)
    {
        int idx = id switch {
            "F0" => 0,
            "F1" => 1,
            "F2" => 2,
            "F3" => 3,
            "F4" => 4,
            _ => 0
        };
        I.Show(idx);
    }

    // New: go to next node
    public void GoNext()
    {
        int next = current + 1;
        if (next < nodes.Length)
            Show(next);
        // else: already at last node, do nothing
    }

    // New: go to previous node
    public void GoPrev()
    {
        int prev = current - 1;
        if (prev >= 0)
            Show(prev);
        // else: already at first node, do nothing
    }

    void Show(int idx)
    {
        if (nodes == null || nodes.Length == 0) return;
        if (idx < 0 || idx >= nodes.Length) return;

        for (int i = 0; i < nodes.Length; i++)
        {
            if (nodes[i] != null)
                nodes[i].gameObject.SetActive(i == idx);
        }

        current = idx;
    }
}

// Scripts/Core/DeathHandler.cs
public class DeathHandler : MonoBehaviour {
    public static void Kill(string reason){
        PlayerPrefs.SetString("LastHint", reason);
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
    }
}
