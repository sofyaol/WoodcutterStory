using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
   [SerializeField] private float _health = 100f;
   [SerializeField] private int _wood = 0;
   [SerializeField] private int _coin = 0;
    public float Health { get => _health; set => _health = value; }

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

    public Action OnCoinChange;
    public Action OnWoodChange;
}
