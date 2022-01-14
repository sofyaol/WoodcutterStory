using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GroundCanvas : MonoBehaviour
{
  [Header("Sprites of background")]
  [SerializeField] private Sprite _green;
  [SerializeField] private Sprite _red;

  [Space]
  [Header("Coin Sprite")]
  [SerializeField] private Sprite _coin;
  
  [Space]
  [Header("Images on Canvas")]
  [SerializeField] private Image _background;
  [SerializeField] private Image _resource;
  
  [Space]
  [Header("Text of ground price")]
  [SerializeField] private TextMeshProUGUI _price;

  public void SetGreenBackground()
  {
    _background.sprite = _green;
  }

  public void SetRedBackground()
  {
    _background.sprite = _red;
  }

  public void SetResource(ResourceType resourceType)
  {
    if (resourceType == ResourceType.Coin)
    {
      _resource.sprite = _coin;
    }
  }

  public void SetPrice(int price)
  {
    _price.text = price.ToString();
  }
}
