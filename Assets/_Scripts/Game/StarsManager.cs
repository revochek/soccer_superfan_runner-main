using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StarsManager : MonoBehaviour
{
    private int _starsCount;

    public event UnityAction<int> StarsChanged;
    public event UnityAction StarsAdded;

    private IDataProvider _dataProvider;
    private IPersistentData _persistentData;

    public void Initialize(IPersistentData persistentData, IDataProvider dataProvider)
    {
        _persistentData = persistentData;
        _dataProvider = dataProvider;
        _starsCount = _persistentData.PlayerData.Stars;
    }

    private void Start()
    {
        StarsChanged?.Invoke(_starsCount);
    }

    public void AddStars(int stars)
    {
        if (stars < 0)
            throw new ArgumentOutOfRangeException(nameof(stars));

        _starsCount += stars;
        _persistentData.PlayerData.Stars = _starsCount;
        _dataProvider.Save();

        StarsAdded?.Invoke();
        StarsChanged?.Invoke(_persistentData.PlayerData.Stars);
    }

    public bool IsEnough(int stars)
    {
        if (stars < 0)
            throw new ArgumentOutOfRangeException(nameof(stars));

        return _persistentData.PlayerData.Coins >= stars;
    }

    public void Spend(int stars)
    {
        if (stars < 0)
            throw new ArgumentOutOfRangeException(nameof(stars));

        _starsCount -= stars;
        _persistentData.PlayerData.Stars = _starsCount;
        _dataProvider.Save();

        StarsChanged?.Invoke(_persistentData.PlayerData.Stars);

    }
}
