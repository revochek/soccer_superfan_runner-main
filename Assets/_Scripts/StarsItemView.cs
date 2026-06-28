using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class StarsItemView : MonoBehaviour
{
    //[SerializeField] private IntValueView _priceView;
    public event UnityAction<StarsItemView> BuyButtonClicked;

    [SerializeField] private BuyButton _buyButton;
    public BuyButton BuyButton => _buyButton;
    [SerializeField] private TMP_Text _starsCountText;

    public StarsItem Item { get; private set; }

    public int Stars => Item.Stars;
    public int Price => Item.Price;

    public void Initialize(StarsItem item)
    {
        Item = item;

        _starsCountText.text = "x " + Stars;
        _buyButton.UpdateText(Price);
    }

    private void OnEnable()
    {
        _buyButton.Click += OnButtonClick;
    }

    private void OnDisable()
    {
        _buyButton.Click -= OnButtonClick;
    }

    private void OnButtonClick()
    {
        BuyButtonClicked?.Invoke(this);
    }
}
