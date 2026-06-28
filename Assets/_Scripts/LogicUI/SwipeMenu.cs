using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SwipeMenu : MonoBehaviour
{
    
    [SerializeField] private Scrollbar scrollbar;

    //private List<ShopItem> _shopItems = new List<ShopItem>();

    public event UnityAction<ShopItemView> PreviewedIndexItemChanged;

    private float[] _elementPositions;
    private float _scrollPos;
    [SerializeField] private float _distance; // Відстань між елементами
    private int _previewedChildIndex;
    private bool _isInitalized = false;
    //public int SelectedChildIndex => _previewedChildIndex;

    public void SetStartIndexSelectedItem(int index)
    {
        _previewedChildIndex = index;
    }

    public void Initialize()
    {
        int childCount = transform.childCount;
        _elementPositions = new float[childCount];
        _distance = 1f / (childCount - 1f);

        for (int i = 0; i < childCount; i++)
        {
            _elementPositions[i] = _distance * i;
        }

        Debug.Log($"{_elementPositions.Length} | {_previewedChildIndex}");

        _scrollPos = _elementPositions[_previewedChildIndex];
        scrollbar.value = _scrollPos;

        _isInitalized = true;
    }

    void Start()
    {
        Debug.Log($"Start->GetSelectedShopItem(): {GetSelectedShopItem().Item}");
        PreviewedIndexItemChanged?.Invoke(GetSelectedShopItem());
    }


        void Update()
    {
        if (_isInitalized)
        {
            int previewedChildIndex = _previewedChildIndex;
           
            if (Input.GetMouseButton(0))
            {
                UpdateSelectedChildIndex();
                _scrollPos = scrollbar.value;
            }
            else
            {
                SnapToPosition();
                //scrollbar.value = _scrollPos;
            }

            ScaleMenuItems();

            if (previewedChildIndex != _previewedChildIndex)
            {
                Debug.Log(GetSelectedShopItem().Item.Image);
                PreviewedIndexItemChanged?.Invoke(GetSelectedShopItem());
            }
        }
    }

    private ShopItemView GetSelectedShopItem()
    {
        Transform child = transform.GetChild(_previewedChildIndex);
        return child.GetComponent<ShopItemView>();
    }

    // Оновлення індексу вибраного елемента
    private void UpdateSelectedChildIndex()
    {
        float closestDistance = Mathf.Abs(_scrollPos - _elementPositions[0]);
        _previewedChildIndex = 0;

        for (int i = 0; i < _elementPositions.Length; i++)
        {
            float distance = Mathf.Abs(_scrollPos - _elementPositions[i]);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                _previewedChildIndex = i;               
            }
        }
    }

    // Прив'язка до позиції найближчого елемента
    private void SnapToPosition()
    {
        if (_scrollPos < _elementPositions[_previewedChildIndex] + _distance / 2 && _scrollPos > _elementPositions[_previewedChildIndex] - _distance / 2)
        {
            scrollbar.value = Mathf.Lerp(scrollbar.value, _elementPositions[_previewedChildIndex], 0.1f);
        }
    }

    // Масштабування елементів меню в залежності від позиції
    private void ScaleMenuItems()
    {      
        for (int i = 0; i < _elementPositions.Length; i++)
        {
            Transform child = transform.GetChild(i);
            float scale = 1f - Mathf.Abs(_scrollPos - _elementPositions[i]) * 0.9f; // Менша відстань - більший масштаб
            scale = Mathf.Clamp(scale, 0.5f, 1f); // Обмеження масштабу
            child.localScale = Vector2.Lerp(child.localScale, new Vector2(scale, scale), 0.1f);
        }
    }
}
