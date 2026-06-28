using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

public class ShopPanel : MonoBehaviour
{
    //public event Action<ShopItemView> ItemViewClicked;

    private List<ShopItemView> _shopItems = new List<ShopItemView>();

    [SerializeField] private Transform _itemsParent;
    [SerializeField] private ShopItemViewFactory _shopItemViewFactory;

    [SerializeField] private SwipeMenu _swipeMenu;

    public event UnityAction<ShopItemView> PreviewedItemChanged;
    //[SerializeField] private ShopItemView _selectedItem;

    private OpenSkinsChecker _openSkinsChecker;
    private SelectedSkinChecker _selectedSkinChecker;

    public void Initialize(OpenSkinsChecker openSkinsChecker, SelectedSkinChecker selectedSkinChecker)
    {
        _openSkinsChecker = openSkinsChecker;
        _selectedSkinChecker = selectedSkinChecker;
    }

    private void OnEnable()
    {
        _swipeMenu.PreviewedIndexItemChanged += OnPreviewedItemChanged;
    }

    private void OnDisable()
    {
        _swipeMenu.PreviewedIndexItemChanged -= OnPreviewedItemChanged;
    }

    private void OnPreviewedItemChanged(ShopItemView shopItemView)
    {
        PreviewedItemChanged?.Invoke(shopItemView);
        Debug.Log("OnPreviewedItemChanged-->" + shopItemView);
    }

    public void Show(IEnumerable<ShopItem> items)
    {
        Clear();

        int iteration = 0;

        foreach (ShopItem item in items)
        {
            ShopItemView spawnedItem = _shopItemViewFactory.Get(item, _itemsParent);

            _openSkinsChecker.Visit(spawnedItem.Item);
            _shopItems.Add(spawnedItem);

            if (_openSkinsChecker.IsOpened)
            {
                _selectedSkinChecker.Visit(spawnedItem.Item);
                spawnedItem.Unlock();

                if (_selectedSkinChecker.IsSelected)
                {
                    Debug.Log("IsSelected-->" + spawnedItem.Item + " / " + iteration);
                    _swipeMenu.SetStartIndexSelectedItem(iteration);
                }
            }
            else
            {
                spawnedItem.Lock();
            }

            iteration++;
        }

        _swipeMenu.Initialize();
        //Sort();
    }

    private void Sort()
    {
        _shopItems = _shopItems
            .OrderBy(item => item.IsLock)
            .ThenByDescending(item => item.Price)
            .ToList();

        for (int i = 0; i < _shopItems.Count; i++)
            _shopItems[i].transform.SetSiblingIndex(i);
    }



    private void Clear()
    {
        foreach (ShopItemView item in _shopItems)
        {
            Destroy(item.gameObject);
        }

        _shopItems.Clear();
    }
}