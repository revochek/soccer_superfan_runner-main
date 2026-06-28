using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject.SpaceFighter;

public class MenuBootstrap : MonoBehaviour
{
    [SerializeField] private Shop _shop;
    [SerializeField] private StarsShop _starsShop;
    [SerializeField] private HourlyReward _hourlyReward;

    private IDataProvider _dataProvider;
    private IPersistentData _persistentPlayerData;

    [SerializeField] private CoinManager _coinManager;
    [SerializeField] private StarsManager _starsManager;
    [SerializeField] private SoundManager _soundManager;

    public void Awake()
    {
        InitializeData();
        InitializeShop();

        _coinManager.Initialize(_persistentPlayerData, _dataProvider);
        _starsManager.Initialize(_persistentPlayerData, _dataProvider);
        _hourlyReward.Initialize(_persistentPlayerData, _dataProvider);
        _soundManager.Initialize(_persistentPlayerData, _dataProvider);
    }

    private void InitializeData()
    {
        _persistentPlayerData = new PersistentData();
        _dataProvider = new DataLocalProvider(_persistentPlayerData);

        LoadDataOrInit();
    }

    private void InitializeShop()
    {
        OpenSkinsChecker openSkinsChecker = new OpenSkinsChecker(_persistentPlayerData);
        SelectedSkinChecker selectedSkinChecker = new SelectedSkinChecker(_persistentPlayerData);
        SkinSelector skinSelector = new SkinSelector(_persistentPlayerData);
        SkinUnlocker skinUnlocker = new SkinUnlocker(_persistentPlayerData);

        _shop.Initialize(_dataProvider, _coinManager, openSkinsChecker, selectedSkinChecker, skinSelector, skinUnlocker);
        _starsShop.Initialize(_dataProvider, _coinManager, _starsManager);
    }

        private void LoadDataOrInit()
    {
        if (_dataProvider.TryLoad() == false)
            _persistentPlayerData.PlayerData = new PlayerData();
    }
}
