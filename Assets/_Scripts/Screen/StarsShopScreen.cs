using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarsShopScreen : Screen
{
    [SerializeField] private Button _closeButton;

    [SerializeField] private MenuScreen _menuScreen;

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
        CanvasGroup.alpha = 1;
        transform.localScale = new Vector3(0.8f, 0.8f, 1f);
        transform.DOScale(1f, 0.1f).SetEase(Ease.InOutQuad).SetUpdate(true);
        Activate();
    }

    private void OnCloseButtonClicked()
    {
        _menuScreen.Open();
        SoundLoader.Instance.PlaySound(SoundLoader.Instance.PopupClose);
        Close();
    }
}
