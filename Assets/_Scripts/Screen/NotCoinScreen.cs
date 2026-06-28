using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotCoinScreen : Screen
{
    [SerializeField] private Button _closeButton;

    private void OnEnable()
    {
        _closeButton.onClick.AddListener(OnCloseButtonClicked);
    }

    private void OnDisable()
    {
        _closeButton.onClick.RemoveListener(OnCloseButtonClicked);
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

    private void OnCloseButtonClicked()
    {
        SoundLoader.Instance.PlaySound(SoundLoader.Instance.Click);
        SoundLoader.Instance.PlaySound(SoundLoader.Instance.PopupClose);
        Close();
    }
}
