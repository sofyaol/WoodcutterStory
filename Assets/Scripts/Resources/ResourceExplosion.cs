using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class ResourceExplosion : MonoBehaviour
{
    [SerializeField] private ResourceType _resourceType;
    [SerializeField] private float _forceAcceleration;
    [SerializeField] private int _offsetForce = 3;
    private GameObject _resource;
    private Vector3 _offset;
    private System.Random _random = new Random();

    private void Start()
    {
        if (_resourceType == ResourceType.Wood)
        {
            _resource = (GameObject) Resources.Load("Resources/Wood");
        }
        
        if (_resourceType == ResourceType.Coin)
        {
            _resource = (GameObject) Resources.Load("Resources/Coin");
        }
    }

    public void Play(int instanceCount, int resourceCount)
    {
        List<int> _resourceValues = new List<int>();
        int value = resourceCount / instanceCount;
        int modulo = resourceCount % instanceCount;
        
        for (int i = 0; i < resourceCount; i++)
        {
            _resourceValues.Add(value);
        }

        for (int i = 0; i < modulo; i++)
        {
            _resourceValues[i]++;
        }

        var spawn = transform.position;
        
        for (int i = 0; i < instanceCount; i++)
        {
            int x = _random.Next(0, _offsetForce);
            int y = _random.Next(0, _offsetForce);
            int z = _random.Next(0, _offsetForce);
            
            _offset = new Vector3(x, y, z);
            
            GameObject instance = Instantiate(_resource, spawn, Quaternion.identity);
            instance.GetComponent<Resource>().Value = _resourceValues[i];
            instance.GetComponent<Rigidbody>().AddForce((Vector3.up + _offset) * _forceAcceleration, ForceMode.Impulse);
            // make offset of next resource instance
            spawn = new Vector3(spawn.x + 0.5f, spawn.y + 0.5f, spawn.z + 0.5f);
        }
            
    }

}
