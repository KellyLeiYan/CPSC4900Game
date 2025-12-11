using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour {
    public MonoBehaviour interactable; // assign a component that implements IInteractable
    void OnMouseUpAsButton(){
        if (interactable is IInteractable i) i.OnInteract();
    }
}

public interface IInteractable
{
    void OnInteract();
}