using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    public event Action Click;

    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Image _icon;

    [SerializeField] private NotCoinScreen _notCoinScreen;

    [SerializeField] private Color _lockColor;
    [SerializeField] private Color _unlockColor;

    [SerializeField] private Vector3 activatedScale = new Vector3(1.2f, 1.2f, 1.2f);

    [SerializeField, Range(0, 1)] private float _lockAnimationDuration = 0.4f;
    [SerializeField, Range(0.5f, 5)] private float _lockAnimationStrength = 2f;

    private bool _isLock;

    private void OnEnable() => _button.onClick.AddListener(OnButtonClick);
    private void OnDisable() => _button.onClick.RemoveListener(OnButtonClick);

    public void UpdateText(int price) => _text.text = price.ToString();

    public void Lock()
    {
        _isLock = true;
        _text.color = _lockColor;
        _icon.color = new Color(1f, 0.67f, 0.67f);
    }

    public void Unlock()
    {
        _isLock = false;
        _text.color = _unlockColor;
        _icon.color = Color.white;
    }

    private void OnButtonClick()
    {
        if (_isLock)
        {
            transform.DOShakePosition(_lockAnimationDuration, _lockAnimationStrength);
            SoundLoader.Instance.PlaySound(SoundLoader.Instance.UnsuccessfulPurchase);
            _notCoinScreen.Open();
            return;
        }

        transform.DOScale(activatedScale, 0.1f).SetLoops(2, LoopType.Yoyo);
        SoundLoader.Instance.PlaySound(SoundLoader.Instance.SuccessfulPurchase);
        Click?.Invoke();
    }
}