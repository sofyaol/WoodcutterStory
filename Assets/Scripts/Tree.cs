using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using DG.Tweening;
public class Tree : MonoBehaviour, IChoppable
{
   [SerializeField] private int _id;
    public int Id {
        get => _id;
        set => _id = value;
    }
    
    private ParticleSystem _dieParticle;
    private MeshRenderer _meshRenderer;
    private List<Collider> _colliders = new List<Collider>();
    
    [SerializeField] private ResourceExplosion _resourceExplosion;

    [Tooltip("Count of resources given by the tree")]
    [SerializeField] private  int _resourceCount;
    [Tooltip("The time in seconds after which the Tree respawns")]
    [SerializeField] private float _respawnTime = 5f;

    internal int ResourceCount { get; }
    private int _health = 3;
    public event ChoppableDie Dying; 
    
    private int Health
    {
        get { return _health; }
        set
        {
            _health = value;
            if (_health <= 0)
                Die();
        }
    }

    private void Die()
    {
        Dying?.Invoke(Id);
        
        SetVisibility(false);
        _dieParticle.Play();
        _resourceExplosion.Play(3, _resourceCount);
        StartCoroutine("Respawn");
    }

    private void SetVisibility(bool isVisible)
    {
        foreach (var item in _colliders)
        {
            item.enabled = isVisible;
        }

        _meshRenderer.enabled = isVisible;
    }

    private void Start()
    {
        _dieParticle = GetComponentInChildren<ParticleSystem>();
        _meshRenderer = GetComponent<MeshRenderer>();
        
        foreach (var item in GetComponents<Collider>())
        {
            _colliders.Add(item);
        }
    }
    

   public void GetDamage()
   {
       transform.DOShakeRotation(0.5f, 15f, 5).OnComplete(NormalizeRotation);
       Health--;
   }
   
   void NormalizeRotation()
    {
        transform.DORotate(new Vector3(0, 0, 0), 0.5f);
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(_respawnTime);
        SetVisibility(true);
        //transform.DOShakeScale(0.5f, 1f, 3, 90f);
        transform.DOScaleY( transform.localScale.y * 0.8f, 0.2f).SetLoops(4, LoopType.Yoyo);
        _health = 3;
    }
}
