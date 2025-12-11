using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickAnywhereToClose : MonoBehaviour
{
    public GameObject panel;
    private bool canClose = false;

    private void OnEnable()
    {
        // Prevent closing on the same click that opened the panel
        canClose = false;
        StartCoroutine(EnableCloseNextFrame());
    }

    IEnumerator EnableCloseNextFrame()
    {
        yield return null; // wait 1 frame
        canClose = true;
    }

    void Update()
    {
        if (!canClose) return;

        // If user clicks ANYWHERE (except UI blocking)
        if (Input.GetMouseButtonDown(0))
        {
            panel.SetActive(false);
        }
    }
}
