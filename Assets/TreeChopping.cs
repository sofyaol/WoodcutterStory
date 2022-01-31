using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TreeChopping : MonoBehaviour
{
    private Tree _tree;
    
   [SerializeField] private Animator _animator;
   private static readonly int IsChopping = Animator.StringToHash("IsChopping");
   private Transform _model;

   void Start()
   {
       _model = _animator.gameObject.transform;
   }

   private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8) // layer 8 - Tree
        {
            _tree = other.gameObject.GetComponent<Tree>();
            _animator.SetBool(IsChopping, true);
            _animator.applyRootMotion = true;
            _tree.dying += OnTreeDie;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8) // layer 8 - Tree
        {
            _animator.SetBool(IsChopping, false);
            _animator.applyRootMotion = false;
        }
    }

    internal void HitTree()
    {
        _tree.MakeHit();
    }

    void OnTreeDie()
    {
        _animator.SetBool(IsChopping, false);
        _animator.applyRootMotion = false;
    }
}
