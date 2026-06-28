using System.Collections;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SoundButton : MonoBehaviour
{
    public event Action Click;

    [SerializeField]
    private Image _soundImage;

    [SerializeField] private Sprite _soundOn;
    [SerializeField] private Sprite _soundOff;

    private bool _isLock;

    [SerializeField] private Button _button;

    private void OnEnable() => _button.onClick.AddListener(OnButtonClick);
    private void OnDisable() => _button.onClick.RemoveListener(OnButtonClick);

    private void OnButtonClick()
    {
        SoundLoader.Instance.PlaySound(SoundLoader.Instance.Click);
        Click?.Invoke();
    }

    public void ChangeSoundIcon(bool isAudionOn)
    {
        if(isAudionOn == true)
        _soundImage.sprite = _soundOn;
        else _soundImage.sprite = _soundOff;
    }
}
