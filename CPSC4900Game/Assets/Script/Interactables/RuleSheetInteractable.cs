using System.Collections;
using System.Collections.Generic;
// Scripts/Interactables/RuleSheetInteractable.cs
using UnityEngine;

public class RuleSheetInteractable : MonoBehaviour
{
    [Header("UI Panel that shows the big rule sheet")]
    public GameObject ruleSheetPanel;

    void OnMouseDown()
    {
        // This is called when the player clicks on the collider with the mouse
        if (ruleSheetPanel != null)
        {
            ruleSheetPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("RuleSheetInteractable: ruleSheetPanel is not assigned in inspector.");
        }
    }
}
