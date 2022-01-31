using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventReceiver : MonoBehaviour
{
    private TreeChopping _chopping;
    void Start()
    {
        _chopping = GetComponentInParent<TreeChopping>();
    }

    void OnHitTree()
    {
        _chopping.HitTree();
    }
    
}
