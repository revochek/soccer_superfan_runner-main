using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpellCountUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _spellCountText;
    [SerializeField] private SpellManager _spellManager;

    public void OnEnable()
    {
        _spellManager.SpellCountChanged += OnSpellCountChanged;
    }
    private void OnDisable()
    {
        _spellManager.SpellCountChanged -= OnSpellCountChanged;
    }

    public void OnSpellCountChanged(int currentCoinCount)
    {
        _spellCountText.text = "x " + currentCoinCount;
    }

}
