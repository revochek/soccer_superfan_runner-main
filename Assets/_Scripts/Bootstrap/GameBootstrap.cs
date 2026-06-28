using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject.Asteroids;

public class GameBootstrap : MonoBehaviour
{
    [SerializeField] private HeroModelLoader _heroModelLoader;
    [SerializeField] private CoinManager _coinManager;
    [SerializeField] private StarsManager _starsManager;
    [SerializeField] private SpellManager _spellManager;
    [SerializeField] private LevelNumberUI _levelNumberUI;
    [SerializeField] private LevelLoader _levelLoader;

    private IDataProvider _dataProvider;
    private IPersistentData _persistentPlayerData;


    public void Awake()
    {
        InitializeData();
        InitializeGame();
    }

    private void InitializeData()
    {
        _persistentPlayerData = new PersistentData();
        _dataProvider = new DataLocalProvider(_persistentPlayerData);

        LoadDataOrInit();
    }

    private void InitializeGame()
    {
        _heroModelLoader.Initialize(_persistentPlayerData.PlayerData.SelectedHeroSkin);
        _coinManager.Initialize(_persistentPlayerData, _dataProvider);
        _starsManager.Initialize(_persistentPlayerData, _dataProvider);
        _spellManager.Initialize(_persistentPlayerData, _dataProvider);
        _levelLoader.Initialize(_persistentPlayerData, _dataProvider);
        _levelNumberUI.Initialize(_persistentPlayerData);
    }

    private void LoadDataOrInit()
    {
        if (_dataProvider.TryLoad() == false)
            _persistentPlayerData.PlayerData = new PlayerData();
    }
}
