using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] private ResourceType _resourceType;
    [SerializeField] private int _value;
    public int Value
    {
        set => _value = value;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7) // 7 layer - player
        {
            if (_resourceType == ResourceType.Wood)
            {
                Player.Instance.Wood += _value;
                return;
            }

            else if (_resourceType == ResourceType.Coin)
            {
                Player.Instance.Coin += _value;
                return;
            }
        }
    }
    
    
}
