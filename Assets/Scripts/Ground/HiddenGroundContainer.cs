using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenGroundContainer : MonoBehaviour
{
    internal void MakeChildrenTagHidden()
    {
        foreach (var child in GetComponentsInChildren<GroundCube>())
        {
            child.gameObject.tag = "Hidden";
        }
    }
}
