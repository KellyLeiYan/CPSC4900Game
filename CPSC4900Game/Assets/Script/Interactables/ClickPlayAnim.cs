using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPlayAnim : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnMouseDown()
    {
        // This is called when you click on the sprite (needs a collider)
        anim.SetTrigger("OnClick");
    }
}
