using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCountUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinCountText;
    [SerializeField] private CoinManager _coinManager;

    public void OnEnable()
    {
        _coinManager.CoinsAdded += OnCoinCountAdded;
        _coinManager.CoinsChanged += OnCoinCountChanged;
    }
    private void OnDisable()
    {
        _coinManager.CoinsAdded -= OnCoinCountAdded;
        _coinManager.CoinsChanged -= OnCoinCountChanged;
    }

    public void OnCoinCountChanged(int currentCoinCount)
    {
        _coinCountText.text = currentCoinCount.ToString();
    }

    public void OnCoinCountAdded()
    {
        transform.DOScale(1.07f, 0.1f).OnComplete(() => {
            transform.DOScale(1f, 0.15f);
        });
    }
}
