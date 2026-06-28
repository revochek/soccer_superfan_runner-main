using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayScreen : Screen
{
    [SerializeField] private Button _backButton;

    private void OnEnable()
    {
        _backButton.onClick.AddListener(OnBackButtonClicked);
    }

    private void OnDisable()
    {
        _backButton.onClick.RemoveListener(OnBackButtonClicked);
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

    private void OnBackButtonClicked()
    {
        SoundLoader.Instance.PlaySound(SoundLoader.Instance.Click);
        SceneLoaderManager.Instance.LoadMainMenuScene();
    }
}
