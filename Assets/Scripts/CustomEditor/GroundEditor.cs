using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(Ground))]
public class GroundEditor : Editor
{
    private Ground ground;

    private void OnEnable()
    {
        ground = (Ground) target;
    }

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Find Neighbors Of Children"))
        {
            ground.FindNeighbors();
        }
    }
}
