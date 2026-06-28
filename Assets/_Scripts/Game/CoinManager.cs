using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoinManager : MonoBehaviour
{
    private int _coinCount;
    //{
    //    get => _persistentData.PlayerData.Coins;
    //    set {}
    //}

    public event UnityAction<int> CoinsChanged;
    public event UnityAction CoinsAdded;
    //private PlayerData _data;
    private IDataProvider _dataProvider;
    private IPersistentData _persistentData;

    public void Initialize(IPersistentData persistentData, IDataProvider dataProvider)
    {
        _persistentData = persistentData;
        _dataProvider = dataProvider;
        _coinCount = _persistentData.PlayerData.Coins;
        //InitializeData();
    }

    //private void InitializeData()
    //{
    //    _persistentData = new PersistentData();
    //    _dataProvider = new DataLocalProvider(_persistentData);

    //    LoadDataOrInit();
    //}

    //private void LoadDataOrInit()
    //{
    //    if (_dataProvider.TryLoad() == false)
    //        _persistentData.PlayerData = new PlayerData();
    //}

    private void Start()
    {
        CoinsChanged?.Invoke(_coinCount);
    }

    public void AddCoins(ICoinsHolder coinHolder)
    {
        if (coinHolder.GetCoins() < 0)
            throw new ArgumentOutOfRangeException(nameof(coinHolder));

        _coinCount += coinHolder.GetCoins();
        _persistentData.PlayerData.Coins = _coinCount;
        _dataProvider.Save();

        CoinsAdded?.Invoke();
        CoinsChanged?.Invoke(_persistentData.PlayerData.Coins);
    }

    public void AddCoins(int coins)
    {
        if (coins < 0)
            throw new ArgumentOutOfRangeException(nameof(coins));

        _coinCount += coins;
        _persistentData.PlayerData.Coins = _coinCount;
        _dataProvider.Save();

        CoinsAdded?.Invoke();
        CoinsChanged?.Invoke(_persistentData.PlayerData.Coins);
    }

    public bool IsEnough(int coins)
    {
        if (coins < 0)
            throw new ArgumentOutOfRangeException(nameof(coins));

        return _persistentData.PlayerData.Coins >= coins;
    }

    public void Spend(int coins)
    {
        if (coins < 0)
            throw new ArgumentOutOfRangeException(nameof(coins));

        _coinCount -= coins;
        _persistentData.PlayerData.Coins = _coinCount;
        _dataProvider.Save();

        CoinsChanged?.Invoke(_persistentData.PlayerData.Coins);

    }

    //private void SaveCoinCount()
    //{
    //    _data.Coins = _coinCount;
    //    SaveGame.Save<PlayerData>("playerData", _data);
    //}
}
