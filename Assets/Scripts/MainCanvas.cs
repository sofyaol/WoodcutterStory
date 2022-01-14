using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coin;
    [SerializeField] private TextMeshProUGUI _wood;

    private void Start()
    {
        _coin.text = Player.Instance.Coin.ToString();
        _wood.text = Player.Instance.Wood.ToString();
        
        Player.Instance.OnCoinChange += ChangeCoinCount;
        Player.Instance.OnWoodChange += ChangeWoodCount;
    }

    private void ChangeCoinCount()
    {
        _coin.text = Player.Instance.Coin.ToString();
    }
    private void ChangeWoodCount()
    {
        _wood.text = Player.Instance.Wood.ToString();
    }
}
