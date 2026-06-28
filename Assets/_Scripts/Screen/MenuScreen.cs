using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MenuScreen : Screen
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _starsButton;
    [SerializeField] private Button _shopButton;

    [SerializeField] private StarsShopScreen _starsShopScreen;
    [SerializeField] private ShopScreen _shopScreen;

    private void OnEnable()
    {
        _starsButton.onClick.AddListener(OnStarsButtonClicked);
        _startButton.onClick.AddListener(OnStartButtonClicked);
        _startButton.transform.DOScale(1.1f, 1.4f).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo).SetUpdate(true);
        _shopButton.onClick.AddListener(OnShopButtonClicked);
    }

    private void OnDisable()
    {
        _starsButton.onClick.RemoveListener(OnStarsButtonClicked);
        _startButton.onClick.RemoveListener(OnStartButtonClicked);
    }

    public override void Close()
    {
        CanvasGroup.alpha = 0;
        Deactivate();
    }

    public override void Open()
    {
        CanvasGroup.alpha = 1;
        Activate();
    }

    private void OnShopButtonClicked()
    {
        _shopScreen.Open();
        SoundLoader.Instance.PlaySound(SoundLoader.Instance.PopupOpen);
        Close();
    }

    private void OnStarsButtonClicked()
    {
        _starsShopScreen.Open();
        SoundLoader.Instance.PlaySound(SoundLoader.Instance.PopupOpen);
        Close();
    }

    private void OnStartButtonClicked()
    {
        SoundLoader.Instance.PlaySound(SoundLoader.Instance.StartButton);
        SceneLoaderManager.Instance.LoadGameLevelScene();
    }
}
