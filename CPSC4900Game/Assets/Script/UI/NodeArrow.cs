using System.Collections;
using System.Collections.Generic;
// Scripts/Interactable/NodeArrow.cs
using UnityEngine;

public class NodeArrow : MonoBehaviour
{
    public enum Direction { Left, Right }
    public Direction direction = Direction.Right;

    void OnMouseDown()
    {
        if (NodeRouter.I == null) return;

        if (direction == Direction.Right)
            NodeRouter.I.GoNext();
        else
            NodeRouter.I.GoPrev();
    }
}
