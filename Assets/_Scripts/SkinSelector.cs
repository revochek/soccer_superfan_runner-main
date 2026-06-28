using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSelector : IShopItemVisitor
{
    private IPersistentData _persistentData;

    public SkinSelector(IPersistentData persistentData) => _persistentData = persistentData;

    public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

    public void Visit(HeroSkinItem characterSkinItem)
        => _persistentData.PlayerData.SelectedHeroSkin = characterSkinItem.SkinType;
}