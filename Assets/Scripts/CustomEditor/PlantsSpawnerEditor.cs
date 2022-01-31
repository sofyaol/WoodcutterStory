using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlantsSpawner))]
public class PlantsSpawnerEditor : Editor
{
    private PlantsSpawner _plantsSpawner;

    private void OnEnable()
    {
        _plantsSpawner = (PlantsSpawner) target;
    }

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Spawn Plants"))
        {
            _plantsSpawner.SpawnPlants();
        }
    }
}
