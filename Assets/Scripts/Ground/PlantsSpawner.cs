using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantsSpawner : MonoBehaviour
{
    private const int s_maxModuleXZ = 35;

    [SerializeField] private ScriptableObjectList _plants;

    [SerializeField] private Transform _plantsParent;
   // [SerializeField] private List<GameObject> _plants;
    private float[,] _plantsPositions;
    private int _plantsNumber;
    private System.Random _random = new System.Random();

    public void SpawnPlants()
    {
        _plants = (ScriptableObjectList) Resources.Load("PlantsList");
        
        foreach (var child in GetComponentsInChildren<GroundCube>())
        {
            //_groundCubes.Add(child);
            _plantsParent = child.GetComponentInChildren<DecorationsParent>().transform;
            SpawnPlantsOnCube(_plantsParent);
        }
        
    }

    private void SpawnPlantsOnCube(Transform parent)
    {
        var parentPosition = parent.transform.position;

        _plantsNumber = UnityEngine.Random.Range(0, 2);

        _plantsPositions = new float[2, _plantsNumber];

        for (int i = 0; i < _plantsNumber; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                _plantsPositions[j, i] = _random.Next(-s_maxModuleXZ, s_maxModuleXZ) / 10f;
            }
            
            float x = parentPosition.x + _plantsPositions[0, i];
            float z = parentPosition.z + _plantsPositions[1, i];
            

            var prefab = _plants.List[UnityEngine.Random.Range(0, _plants.List.Count)];
            prefab.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            
            Instantiate(prefab,
                new Vector3(x, parentPosition.y, z),
                Quaternion.identity,
                parent);
        }
    }
}
