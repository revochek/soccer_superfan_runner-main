using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellButton : MonoBehaviour
{
    public event Action Click;

    [SerializeField] private Image _icon;

    private bool _isLock;

    [SerializeField] private Button _button;
    [SerializeField] private Vector3 activatedScale = new Vector3(1.2f, 1.2f, 1.2f);

    [SerializeField, Range(0, 1)] private float _lockAnimationDuration = 0.4f;
    [SerializeField, Range(0.5f, 5)] private float _lockAnimationStrength = 2f;

    private void OnEnable() => _button.onClick.AddListener(OnButtonClick);
    private void OnDisable() => _button.onClick.RemoveListener(OnButtonClick);

    public void Lock()
    {
        _isLock = true;
        _icon.color = new Color(_icon.color.r, _icon.color.g, _icon.color.b, 0.1f);
    }

    public void Unlock()
    {
        _isLock = false;
        _icon.color = new Color(_icon.color.r, _icon.color.g, _icon.color.b, 1f);
    }

    private void OnButtonClick()
    {
        if (_isLock)
        {
            transform.DOShakePosition(_lockAnimationDuration, _lockAnimationStrength);
            return;
        }
        else
        {
            SoundLoader.Instance.PlaySound(SoundLoader.Instance.UseSpell);
            transform.DOScale(activatedScale, 0.2f).SetLoops(2, LoopType.Yoyo); 
        }

        Click?.Invoke();
    }
}
