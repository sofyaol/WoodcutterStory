using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradePoint : MonoBehaviour
{
    [SerializeField] private TradeWindow _tradeWindow;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7) // player
        {
            //_tradeWindow.SetActive(true);
            _tradeWindow.Active(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            _tradeWindow.Active(false);
        }
    }
}
