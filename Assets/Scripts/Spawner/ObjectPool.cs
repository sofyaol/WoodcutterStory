using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEditor.SceneTemplate;
using UnityEngine;
using Object = UnityEngine.Object;


public class ObjectPool <T> where T:MonoBehaviour
{
    private int _poolCapacity = 10;
    private Vector3 _position = new Vector3(0, -100, 0);
    private T _instance;
    private int _currentItem;
    private List<T> _pool = new List<T>();
    private int CurrentItem
    {
        get
        {
            return _currentItem;
        }
        set
        {
            if (value == _poolCapacity)
            {
                _currentItem = 0;
                return;
            }

            _currentItem = value;
        }
    }

    public ObjectPool(T prefab)
    {
        for (int i = 0; i < _poolCapacity; i++)
        {
            _instance = Object.Instantiate(prefab, _position, Quaternion.identity);
            _instance.gameObject.SetActive(false);
            _pool.Add(_instance);
        }
    }

    public T GetObject()
    {
        var _object = _pool[CurrentItem];
        CurrentItem ++;

        if (_object.gameObject.activeInHierarchy == true)
        {
            throw new Exception("There are not free objects in the pool.");
        }

        _object.gameObject.SetActive(true);
        
        return _object;
    }

    
    
}

