using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopContent", menuName = "Shop/ShopContent")]
public class ShopContent : ScriptableObject
{
    [SerializeField] private List<HeroSkinItem> _heroSkinItems;

    public IEnumerable<HeroSkinItem> HeroSkinItems => _heroSkinItems;

    private void OnValidate()
    {
        var charaterSkinsDuplicates = _heroSkinItems.GroupBy(item => item.SkinType)
            .Where(array => array.Count() > 1);

        if (charaterSkinsDuplicates.Count() > 0)
            throw new InvalidOperationException(nameof(_heroSkinItems));
    }
}
