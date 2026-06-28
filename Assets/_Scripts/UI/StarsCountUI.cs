using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StarsCountUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _starsCountText;
    [SerializeField] private StarsManager _starsManager;

    public void OnEnable()
    {
        _starsManager.StarsAdded += OnStarsCountAdded;
        _starsManager.StarsChanged += OnStarsCountChanged;
    }
    private void OnDisable()
    {
        _starsManager.StarsAdded -= OnStarsCountAdded;
        _starsManager.StarsChanged -= OnStarsCountChanged;
    }

    public void OnStarsCountChanged(int currentStarsCount)
    {
        _starsCountText.text = currentStarsCount.ToString();
    }

    public void OnStarsCountAdded()
    {
        transform.DOScale(1.07f, 0.1f).OnComplete(() => {
            transform.DOScale(1f, 0.15f);
        });
    }
}
