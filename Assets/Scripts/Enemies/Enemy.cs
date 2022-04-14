using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using DG.Tweening;
[RequireComponent(typeof(EnemyMovement))]
public class Enemy : MonoBehaviour, IChoppable, ICharacter
{
    [Header("Attributes")]
    [SerializeField] private float _maxHealth = 3f;
    [SerializeField] private float _damage;

    public int Id { get; set; }
    private float _health = 3f;
    
    private EnemyMovement _enemyMovement;
    private Collider _collider;
    
    public event HealthChange OnHealthChange;
    public event ChoppableDie Dying; 
    
    public float Health
    {
        get => _health;
        set
        {
            _health = value;
            OnHealthChange.Invoke(_maxHealth, _health);
            if (_health <= 0)
            {
                Die();
            }
        }
    }

    public float Damage { get => _damage; set => _damage = value; }

    private void Start()
    {
        _health = _maxHealth;
        _enemyMovement = GetComponent<EnemyMovement>();
        _collider = GetComponent<Collider>();
    }

    public void GetDamage()
    {
        Health -= Player.Instance.DamageToEnemy;
    }

    private void Die()
    {
        _collider.enabled = false;
        Dying.Invoke(Id);
        _enemyMovement.Dying();
    }

}
