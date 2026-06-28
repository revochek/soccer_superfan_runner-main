using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject.SpaceFighter;
using static UnityEditor.Progress;
using Debug = UnityEngine.Debug;

public class Shop : MonoBehaviour
{
    [SerializeField] private ShopContent _contentItems;

    //[SerializeField] private ShopCategoryButton _characterSkinsButton;

    [SerializeField] private BuyButton _buyButton;
    [SerializeField] private Image _selectedImage;

    [SerializeField] private ShopPanel _shopPanel;

    //[SerializeField] private SkinPlacement _skinPlacement;

    private ShopItemView _previewedItem;

    private SkinSelector _skinSelector;
    private SkinUnlocker _skinUnlocker;
    private OpenSkinsChecker _openSkinsChecker;
    private SelectedSkinChecker _selectedSkinChecker;

    private CoinManager _coinManager;
    private IDataProvider _dataProvider;

    private void OnEnable()
    {
        //_characterSkinsButton.Click += OnHeroSkinsButtonClick;
        _shopPanel.PreviewedItemChanged += OnSelectedItemChanged;

        _buyButton.Click += OnBuyButtonClick;
    }

    private void OnDisable()
    {
        //_characterSkinsButton.Click -= OnHeroSkinsButtonClick;
        _shopPanel.PreviewedItemChanged -= OnSelectedItemChanged;

        _buyButton.Click -= OnBuyButtonClick;
    }

    public void Initialize(IDataProvider dataProvider, CoinManager coinManager, OpenSkinsChecker openSkinsChecker, SelectedSkinChecker selectedSkinChecker, SkinSelector skinSelector, SkinUnlocker skinUnlocker)
    {
        _coinManager = coinManager;
        _dataProvider = dataProvider;
        _openSkinsChecker = openSkinsChecker;
        _selectedSkinChecker = selectedSkinChecker;
        _skinSelector = skinSelector;
        _skinUnlocker = skinUnlocker;

        _shopPanel.Initialize(_openSkinsChecker, _selectedSkinChecker);

        OnHeroSkinsButtonClick();
    }

    private void OnSelectedItemChanged(ShopItemView itemView)
    {       
        _previewedItem = itemView;
        _dataProvider.Save();

        _openSkinsChecker.Visit(_previewedItem.Item);
        _buyButton.UpdateText(_previewedItem.Price);

        Debug.Log("OnSelectedItemChanged-->" + _previewedItem.Item);

        if (_openSkinsChecker.IsOpened)
        {
            Debug.Log("Select-->" + _previewedItem.Item);
            _selectedSkinChecker.Visit(_previewedItem.Item);
            _skinSelector.Visit(_previewedItem.Item);
            HideBuyButton();

            _dataProvider.Save();

            //HideBuyButton();
            //_selectedSkinChecker.Visit(_previewedItem.Item);

            //if (_selectedSkinChecker.IsSelected)
            //{
            //    Debug.Log("Select-->" + _previewedItem.Item);
            //    _skinSelector.Visit(_previewedItem.Item);
            //    _dataProvider.Save();
            //}
        }
        else
        {
           // _previewedItem.Lock();
            ShowBuyButton(_previewedItem.Price);
        }
    }


    private void OnBuyButtonClick()
    {
        if (_coinManager.IsEnough(_previewedItem.Price))
        {
            _coinManager.Spend(_previewedItem.Price);
            _skinUnlocker.Visit(_previewedItem.Item);
            _skinSelector.Visit(_previewedItem.Item);
            _previewedItem.Unlock();
            HideBuyButton();

            _dataProvider.Save();
        }
    }

    private void OnHeroSkinsButtonClick()
    {
        //_characterSkinsButton.Select();

        _shopPanel.Show(_contentItems.HeroSkinItems.Cast<ShopItem>());
    }

    private void ShowBuyButton(int price)
    {
        _buyButton.gameObject.SetActive(true);
        _buyButton.UpdateText(price);

        if (_coinManager.IsEnough(price))
            _buyButton.Unlock();
        else
            _buyButton.Lock();

        //HideSelectionImage();
    }

    private void HideBuyButton() => _buyButton.gameObject.SetActive(false);
    //private void HideSelectionImage() => _selectedImage.gameObject.SetActive(false);
}