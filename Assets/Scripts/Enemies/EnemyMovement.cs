using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator), typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private float _damageToPlayer;
    private NavMeshAgent _agent;
    private Transform _playerTransform;
    private Animator _animator;

    private bool _isAttacking = false;
    private bool _isMoving = false;
    private bool _isDead;
    
    [SerializeField] private float _chaseDistance = 15f;
    [SerializeField] private float _attackDistance = 2.5f;
    
    
    private static readonly int IsRunning = Animator.StringToHash("IsRunning");
    private static readonly int Attacking = Animator.StringToHash("Attack");
    private static readonly int IsDying = Animator.StringToHash("IsDying");

    void Start() 
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _playerTransform = Player.Instance.transform;

        _agent.stoppingDistance = _attackDistance;

        _damageToPlayer = GetComponent<Enemy>().Damage;
    }

    
    void Update()
    {
        var distance = Vector3.Distance(_playerTransform.position, transform.position);

        if (_isDead) return;
            
        if (distance <= _chaseDistance)
        {
            StartChasing(distance);
        }
        
        else if(_isMoving)
        {
            Moving(false);
        }
    }

    private void StartChasing(float distance)
    {
        transform.LookAt(_playerTransform);
        
        if (!_isMoving)
        {
            Moving(true);
        }

        if (distance <= _attackDistance)
        {
            if (!_isAttacking)
            {
                Attack(true);
            }
        }

        else
        {
            if (_isAttacking)
            {
                Attack(false);
            }

            Chasing();
        }
    }

    private void Moving(bool value)
    {
        _isMoving = value;
        _animator.SetBool(IsRunning, value);
    }

    private void Attack(bool value)
    {
        _isAttacking = value;
        _animator.SetBool(Attacking, value);
    }

    private void Chasing()
    {
        _isAttacking = false;
        _agent.SetDestination(_playerTransform.position);
    }

    internal void Dying()
    {
        _animator.SetTrigger(IsDying);
        _isDead = true;
        _agent.enabled = false;
        transform.DOMoveY(-2f, 10f);
    }

    public void DamageToPlayer()
    {
        Player.Instance.Health -= _damageToPlayer;
    }
}
