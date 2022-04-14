using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private List<GroundCube> _groundCubes;
    internal void FindNeighbors()
    {
        foreach (var child in GetComponentsInChildren<GroundCube>())
        {
            child.FindNeighbors();
            
        }
    }

    internal void MakeDecorationsChildren()
    {
        foreach (var child in GetComponentsInChildren<GroundCubeEditorComponent>())
        {
            child.MakeDecorationsChildren();
        }
    }
}
