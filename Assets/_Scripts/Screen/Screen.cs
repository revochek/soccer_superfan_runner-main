using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public abstract class Screen : MonoBehaviour
{
    protected CanvasGroup CanvasGroup;
    [SerializeField] protected TMP_Text _titleText;
    [SerializeField] protected string _screenName;

    private void Start()
    {
        CanvasGroup = GetComponent<CanvasGroup>();
        //Debug.Log($"{gameObject}: CanvasGroup is {CanvasGroup!=null}");
    }

    public abstract void Open();
    public abstract void Close();

    protected void Activate()
    {
        if(_titleText!=null)
        _titleText.text = _screenName;

        CanvasGroup.interactable = true;
        CanvasGroup.blocksRaycasts = true;
    }
    protected void Deactivate()
    {
        CanvasGroup.interactable = false;
        CanvasGroup.blocksRaycasts = false;
    }
}
