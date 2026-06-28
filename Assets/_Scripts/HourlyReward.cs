using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HourlyReward : MonoBehaviour
{
    [SerializeField] private Image _giftImage;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private Button _claimButton;

    [SerializeField] private Vector3 activatedScale = new Vector3(1.2f, 1.2f, 1.2f);
    [SerializeField, Range(0, 1)] private float _lockAnimationDuration = 0.4f;
    [SerializeField, Range(0.5f, 5)] private float _lockAnimationStrength = 5f;

    private IDataProvider _dataProvider;
    private IPersistentData _persistentData;

    private Tween _shakeTween;


    public void Initialize(IPersistentData persistentData, IDataProvider dataProvider)
    {
        _persistentData = persistentData;
        _dataProvider = dataProvider;
    }

    void OnEnable()
    {
        _claimButton.onClick.AddListener(OnClaimButtonClicked);
    }

    void OnDisable()
    {
        _claimButton.onClick.RemoveListener(OnClaimButtonClicked);
    }

    void Update()
    {
        UpdateTimerText();
    }

    public bool IsRewardAvailable()
    {
        return DateTime.Now - _persistentData.PlayerData.LastRewardTime > _persistentData.PlayerData.RewardInterval;
    }

    public void ClaimReward()
    {
        if (!IsRewardAvailable())
        {
            transform.DOShakePosition(_lockAnimationDuration, _lockAnimationStrength);
            return;
        }

        SoundLoader.Instance.PlaySound(SoundLoader.Instance.PrizeCollect);
        _persistentData.PlayerData.SpellsCount++;
        _persistentData.PlayerData.LastRewardTime = DateTime.Now;
        _dataProvider.Save();

        _giftImage.color = new Color(0.5f, 0.5f, 0.5f, 0.6f);

        transform.DOScale(activatedScale, 0.2f).SetLoops(2, LoopType.Yoyo);
    }

    private void UpdateTimerText()
    {
        TimeSpan timeRemaining = _persistentData.PlayerData.RewardInterval - (DateTime.Now - _persistentData.PlayerData.LastRewardTime);
        if (timeRemaining <= TimeSpan.Zero)
        {
            _timerText.text = "GET!";
            _giftImage.color = Color.white;
            StartShakeAnimation();
        }
        else
        {
            _timerText.text = string.Format("{0:D2}:{1:D2}", timeRemaining.Minutes, timeRemaining.Seconds);
            StopShakeAnimation();
        }
    }

    private void OnClaimButtonClicked()
    {
        ClaimReward();
    }

    private void StartShakeAnimation()
    {
        if (_shakeTween == null || !_shakeTween.IsPlaying())
        {
            Sequence shakeSequence = DOTween.Sequence();
            shakeSequence.Append(transform.DOShakePosition(0.8f, new Vector3(8, 0, 0), 5, 0, false, true))
                          .AppendInterval(3f)
                          .SetLoops(-1, LoopType.Restart);

            _shakeTween = shakeSequence;
        }
    }

    private void StopShakeAnimation()
    {
        if (_shakeTween != null)
        {
            _shakeTween.Kill();
            _shakeTween = null;
        }
    }
}