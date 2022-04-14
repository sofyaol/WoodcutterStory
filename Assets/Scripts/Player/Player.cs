using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class Player : Singleton<Player>, ICharacter
{
    private float _health;
    
    [Header("Attributes")]
    [SerializeField] private float _maxHealth = 100f;
   [SerializeField] private float _damage = 1f;
   
   [Header("Resources")]
   [SerializeField] private int _wood = 0;
   [SerializeField] private int _coin = 0;

   public event Action OnCoinChange;
   public event Action OnWoodChange;
   public event HealthChange OnHealthChange;
   
   
   
    internal float DamageToEnemy 
    { 
        get => _damage; 
        set => _damage = value;
    }

    public float Health
    {
        get => _health;
        set
        {
            _health = value;
            OnHealthChange.Invoke(_maxHealth, _health);
        }
    }

    private void Start()
    {
        _health = _maxHealth;
    }

    public int Wood
    {
        get { return _wood; }
        set
        {
            _wood = value;
            OnWoodChange?.Invoke();
        }
    }

    public int Coin
    {
        get
        {
            return _coin;
        }
        set
        {
            _coin = value;
            OnCoinChange?.Invoke();
        }
    }

  
}
