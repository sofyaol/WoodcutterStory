using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class TradeWindow : MonoBehaviour
{
    [Tooltip("1 coin for [rate] wood")]
    [SerializeField] private int _rate = 5;
    [SerializeField] private TextMeshProUGUI _textWood;
    [SerializeField] private TextMeshProUGUI _textCoin;
    
    private int _playerWood;
    private Slider _slider;
    
    private int _maxValue;
    private int _minValue = 0;

    private int _woodForTrade;
    private int _coinForTrade;
    [SerializeField] private ParticleSystem _coinParticles;
    void Start()
    {
        if (_slider == null)
        {
            _slider = GetComponentInChildren<Slider>();
        }
        ValuesUpdating();
    }

    public void OnValueChanged()
    {
        _woodForTrade = (int) _slider.value * _rate;
        _textWood.text = _woodForTrade.ToString();

        _coinForTrade = _woodForTrade / _rate;
        _textCoin.text = _coinForTrade.ToString();
    }

    void ValuesUpdating()
    {
        _slider.value = 0;
        
        _playerWood = Player.Instance.Wood;
        _maxValue = _playerWood - (_playerWood % _rate);
        _slider.maxValue = _maxValue / _rate;
    }

    public void OnTrade()
    {
        Player.Instance.Wood -= _woodForTrade;
        Player.Instance.Coin += _coinForTrade;
        
        gameObject.SetActive(false);
        _coinParticles.Play();
    }

    public void Active(bool active)
    {
        if (active)
        {
            gameObject.SetActive(true);
            ValuesUpdating();
        }

        else
        {
            gameObject.SetActive(false);
        }
    }
}
