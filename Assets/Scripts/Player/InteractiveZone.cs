using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

public class InteractiveZone : MonoBehaviour
{
    private List<IChoppable> _choppables = new List<IChoppable>();
    private int _enemyCount = 0;
    private Animator _animator;
    private static readonly int IsChopping = Animator.StringToHash("IsChopping");
    private bool _inPeace = true;
  

    void Start()
    {
        _animator = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        IChoppable choppable = other.GetComponent<IChoppable>();
        
        if (choppable != null)
        {
            if (choppable is Enemy)
            {
                _enemyCount++;
               // if (_enemyCount == 1) Player.Instance.HealthBar.MakeVisible(true);
            }
            
            if (_inPeace)
            {
                _animator.SetBool(IsChopping, true);
                _animator.applyRootMotion = true;
                _inPeace = false;
            }
            
            choppable.Id = _choppables.Count;
            _choppables.Add(choppable);

            choppable.Dying += ChoppableDie;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IChoppable choppable = other.GetComponent<IChoppable>();
        if (choppable != null)
        {
            RemoveChoppableAt(choppable.Id);
        }
    }

    private void RemoveChoppableAt(int id)
    {
        _choppables[id].Dying -= ChoppableDie;
        
        if (_choppables[id] is Enemy)
        {
            _enemyCount--;
          //  if (_enemyCount == 0) Player.Instance.HealthBar.MakeVisible(false);
        }

        _choppables.RemoveAt(id);

        if (_choppables.Count == 0)
        {
            StopChopping();
            return;
        }
        
        for (int i = 0; i < _choppables.Count; i++)
        {
            _choppables[i].Id = i;
        }
    }

    void ChoppableDie(int id)
    {
        RemoveChoppableAt(id);
    }

    private void StopChopping()
    {
        _inPeace = true;
        _animator.SetBool(IsChopping, false);
        _animator.applyRootMotion = false;
    }
    
    public void Damage()
    {
        // Make damage => Die => OnTreeDie => RemoveAt => _interactives.Count --;
        for (int i = 0; i < _choppables.Count; i++)
        {
            _choppables[i].GetDamage();
        }
    }
}
