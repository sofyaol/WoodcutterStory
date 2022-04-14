using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(Ground))]
public class GroundEditor : Editor
{
    private Ground _ground;

    private void OnEnable()
    {
        _ground = (Ground) target;
    }

    public override void OnInspectorGUI()
    {
        
        if (GUILayout.Button("Find Neighbors Of Children"))
        {
            _ground.FindNeighbors();
        }

        if (GUILayout.Button("Make Decorations children of Ground"))
        {
            _ground.MakeDecorationsChildren();
        }
    }
}
