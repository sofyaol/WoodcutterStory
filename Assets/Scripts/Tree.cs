using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Tree : MonoBehaviour
{
    private ParticleSystem _dieParticle;
    private MeshRenderer _meshRenderer;

  //  [SerializeField] private GameObject _resource;
    [SerializeField] private ResourceExplosion _resourceExplosion;

    [Tooltip("Count of resources given by the tree")]
    [SerializeField] private  int _resourceCount;

    internal int ResourceCount { get; }
    private int _health = 3;
    
    private int Health
    {
        get { return _health; }
        set
        {
            _health = value;
            if (_health == 0)
                Die();
        }
    }

    private void Die()
    {
        foreach (var boxCollider in GetComponents<BoxCollider>())
        {
            boxCollider.enabled = false;
        }

        _meshRenderer.enabled = false;
        _dieParticle.Play();
        _resourceExplosion.Play(3, _resourceCount);
        StartCoroutine("Destroy");
    }

    private void Start()
    {
        _dieParticle = GetComponentInChildren<ParticleSystem>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.layer == 7) // 7 layer - player
        {
            
            transform.DOShakeRotation(0.5f, 15f, 5).OnComplete(NormalizeRotation);
            Health--;
        }
    }

    void NormalizeRotation()
    {
        transform.DORotate(new Vector3(0, 0, 0), 0.5f);
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
