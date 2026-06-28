using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    private int _rewardAmount;
    [SerializeField] private TMP_Text _rewardText;
    [SerializeField] private RewardButton _rewardButton;
    [SerializeField] private CoinManager _coinManager;
    public void Initialize(int rewardAmount)
    {
        _rewardAmount = rewardAmount;
        _rewardText.text = _rewardAmount.ToString();
    }

    private void OnEnable()
    {
        _rewardButton.Click += OnRewardButtonClicked;
    }

    private void OnDisable()
    {
        _rewardButton.Click -= OnRewardButtonClicked;
    }


    private void OnRewardButtonClicked()
    {
        GrantCoins();
    }

    private void GrantCoins()
    {
        _coinManager.AddCoins(_rewardAmount);
    }
}