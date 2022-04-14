using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class ResourceMovement : MonoBehaviour
{
    private Transform _playerTransform;
    private bool _isTrigger = false;
    private float _timer;
    [SerializeField] private float _maxTimeLeft = 1.5f;
    [SerializeField] private float _minTimeLeft = 1.2f;
    private float _timeLeft;
    [Tooltip("The time it takes for the resource to reach the player (seconds).")]
    [SerializeField] private int moveSpeed = 3;
    private Collider _collider;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _playerTransform = Player.Instance.transform;
        _collider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();
        _timeLeft = UnityEngine.Random.Range(_minTimeLeft, _maxTimeLeft);
    }

    private void Update()
    {
        if (_timeLeft > 0)
        {
            _timeLeft -= Time.deltaTime;
            return;
        }
        
        _rigidbody.useGravity = false;
        FollowPlayer(moveSpeed);
    }

    private void FollowPlayer(int moveSeconds)
    {
       // var offset = _playerTransform.position - transform.position;
       transform.position = Vector3.Lerp(transform.position, _playerTransform.position, Time.deltaTime * moveSeconds);
       
       if (!_isTrigger)
       {
           _collider.isTrigger = true;
           _isTrigger = true;
       }
    }

    public void ResetChanges()
    {
        _timeLeft = UnityEngine.Random.Range(_minTimeLeft, _maxTimeLeft);
        _collider.isTrigger = false;
        _rigidbody.useGravity = true;
        _isTrigger = false;
    }
}
