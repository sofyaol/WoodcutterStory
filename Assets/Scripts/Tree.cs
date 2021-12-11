using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

using Random = System.Random;
public class Tree : MonoBehaviour
{
    private int Health { get; set; } = 3;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.layer == 7) // 7 layer - player
        {
            
            transform.DOShakeRotation(0.5f, 15f, 5).OnComplete(NormalizeRotation);


        }
    }

    void NormalizeRotation()
    {
        transform.DORotate(new Vector3(0, 0, 0), 0.5f);
    }

}
