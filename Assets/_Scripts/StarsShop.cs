using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class StarsShop : MonoBehaviour
{
    [SerializeField] private List<StarsItem> _itemsContent;
    [SerializeField] private List<StarsItemView> _itemsContainer;

    private StarsManager _starsManager;
    private CoinManager _coinManager;
    private IDataProvider _dataProvider;
    // Start is called before the first frame update

    private void OnDisable()
    {
        for (int i = 0; i < _itemsContent.Count; i++)
        {
            _itemsContainer[i].BuyButtonClicked -= OnBuyButtonClick;
        }
    }

    private void Start()
    {
        UpdatePrice();
    }

    private void FillItemsContainer()
    {
        for (int i = 0; i < _itemsContent.Count; i++)
        {
            _itemsContainer[i].Initialize(_itemsContent[i]);
            _itemsContainer[i].BuyButtonClicked += OnBuyButtonClick;
        }
    }

    private void UpdatePrice()
    {
        for (int i = 0; i < _itemsContainer.Count; i++)
        {
            if (_coinManager.IsEnough(_itemsContainer[i].Price))
                _itemsContainer[i].BuyButton.Unlock();
            else
                _itemsContainer[i].BuyButton.Lock();
        }
    }

    public void Initialize(IDataProvider dataProvider, CoinManager coinManager, StarsManager starsManager)
    {
        _dataProvider = dataProvider;
        _coinManager = coinManager;
        _starsManager = starsManager;

        FillItemsContainer();
    }

    private void OnBuyButtonClick(StarsItemView previewedItem)
    {
        if (_coinManager.IsEnough(previewedItem.Price))
        {
            _coinManager.Spend(previewedItem.Price);
            _starsManager.AddStars(previewedItem.Stars);

            //previewedItem.BuyButton.UpdateText(previewedItem.Price);

            UpdatePrice();

            _dataProvider.Save();
        }
    }
}
