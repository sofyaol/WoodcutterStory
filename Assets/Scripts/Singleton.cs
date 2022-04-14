using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    protected virtual void Awake()
    {
        _instance = FindObjectOfType<T>();

        if (_instance == null)
        {
            Debug.LogWarning("Warning! Instance " + typeof(T) + " not found");
        }

    }
    public static T Instance
    {
        get
        {
            return _instance;
        }
    }
}
