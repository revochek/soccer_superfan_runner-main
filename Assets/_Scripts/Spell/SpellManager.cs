using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpellManager : MonoBehaviour
{
    [SerializeField] private InvisibleHeroSpell _invisibleHeroSpell;

    [SerializeField] private Spell _currentSpell;

    [SerializeField] private SpellButton _spellButton;
    public event UnityAction<int> SpellCountChanged;

    private int _countSpell;

    private IDataProvider _dataProvider;
    private IPersistentData _persistentData;

    public void Initialize(IPersistentData persistentData, IDataProvider dataProvider)
    {
        _dataProvider = dataProvider;
        _persistentData = persistentData;

        _invisibleHeroSpell.Initialize(persistentData, _dataProvider);
        _countSpell = _persistentData.PlayerData.SpellsCount;
    }

    private void Start()
    {
        SpellCountChanged?.Invoke(_countSpell);
        OnSpellCompleted();
    }

    private void OnEnable()
    {
        _currentSpell = _invisibleHeroSpell;
        _currentSpell.SpellCompleted += OnSpellCompleted;
        _spellButton.Click += OnSpellButtonClicked;
    }

    private void OnDisable()
    {
        _currentSpell.SpellCompleted += OnSpellCompleted;
        _spellButton.Click -= OnSpellButtonClicked;
    }

    public void AddSpellsCount(int countSpell)
    {
        if (_countSpell < 0)
            throw new ArgumentOutOfRangeException(nameof(countSpell));

        _countSpell += countSpell;
        _persistentData.PlayerData.Coins = _countSpell;
        _dataProvider.Save();

        //SpellsAdded?.Invoke();
        SpellCountChanged.Invoke(_persistentData.PlayerData.SpellsCount);
    }

    private void TryActivateSpell()
    {
        if (_currentSpell.CheckActivationPossibility(_countSpell))
        {
            _currentSpell.Activate();
            _countSpell--;

            _persistentData.PlayerData.SpellsCount = _countSpell;
            _dataProvider.Save();

            _spellButton.Lock();
            SpellCountChanged?.Invoke(_countSpell);
        }
    }

    private void OnSpellButtonClicked()
    {
        TryActivateSpell();
    }

    private void OnSpellCompleted()
    {
        if (_currentSpell.CheckActivationPossibility(_countSpell))
        {
            _spellButton.Unlock();
        }
        else _spellButton.Lock();
    }

}
