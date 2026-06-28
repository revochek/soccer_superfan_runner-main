using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.U2D;

public class ShopItemView : MonoBehaviour
{
    [SerializeField] private Image _contentImage;
    [SerializeField] private Image _contentLockImage;

    [SerializeField] private Image _lockBackground;

    //[SerializeField] private IntValueView _priceView;

    private Image _backgroundImage;

    public ShopItem Item { get; private set; }

    public bool IsLock { get; private set; }

    public int Price => Item.Price;
    public GameObject Model => Item.Model;

    public void Initialize(ShopItem item)
    {
        Item = item;

        _contentImage.sprite = item.Image;
        _contentLockImage.sprite = item.Image;
        _contentImage.SetNativeSize();
        _contentLockImage.SetNativeSize();
        //_priceView.Show(Price);
    }

    public void Lock()
    {
        IsLock = true;
        _lockBackground.gameObject.SetActive(IsLock);
        _contentLockImage.gameObject.SetActive(IsLock);
        //_priceView.Show(Price);
    }

    public void Unlock()
    {
        IsLock = false;
        _lockBackground.gameObject.SetActive(IsLock);
        _contentLockImage.gameObject.SetActive(IsLock);
        //_priceView.Hide();
    }

    //public void Select() => _selectionText.gameObject.SetActive(true);
    //public void Unselect() => _selectionText.gameObject.SetActive(false);
}