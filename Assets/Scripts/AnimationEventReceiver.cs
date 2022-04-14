using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class AnimationEventReceiver : MonoBehaviour
{
    private InteractiveZone _interactiveZone;
    void Start()
    {
        _interactiveZone = GetComponentInChildren<InteractiveZone>();
    }

    void OnHitTree()
    {
        _interactiveZone.Damage();
    }
    
}
