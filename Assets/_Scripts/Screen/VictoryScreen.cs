using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class VictoryScreen : Screen
{
    [SerializeField] private Button _nextButton;

    [SerializeField] private LevelLoader _levelLoader;
    [SerializeField] private VictoryZone _victoryZone;

    [SerializeField] private GameObject _coinConfetti;
    private void OnEnable()
    {
        _victoryZone.HeroFinished += OnHeroVictory;
        _nextButton.onClick.AddListener(OnNextButtonClicked);
    }

    private void OnDisable()
    {
        _victoryZone.HeroFinished -= OnHeroVictory;
        _nextButton.onClick.RemoveListener(OnNextButtonClicked);
    }

    public override void Close()
    {
        CanvasGroup.alpha = 0;
        Deactivate();
    }

    public override void Open()
    {
        CanvasGroup.DOFade(1f, 0.2f).SetUpdate(true);
        Activate();
    }

    private void OnHeroVictory()
    {
        Open();
        _coinConfetti.SetActive(true);
        _levelLoader.IncrementNextLevel();
        SoundLoader.Instance.PlaySound(SoundLoader.Instance.Victory);
        Time.timeScale = 0;
    }

    private void OnNextButtonClicked()
    {
        SoundLoader.Instance.PlaySound(SoundLoader.Instance.Click);
        _levelLoader.LoadLevel();
        Time.timeScale = 1;
    }
}
