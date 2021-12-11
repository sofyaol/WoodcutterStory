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
            //_groundCubes.Add(child);
            child.FindNeighbors();
            
        }
    }
}
