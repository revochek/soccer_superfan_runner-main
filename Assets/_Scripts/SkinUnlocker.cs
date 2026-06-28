using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinUnlocker : IShopItemVisitor
{
    private IPersistentData _persistentData;

    public SkinUnlocker(IPersistentData persistentData) => _persistentData = persistentData;

    public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

    public void Visit(HeroSkinItem characterSkinItem)
        => _persistentData.PlayerData.OpenHeroSkin(characterSkinItem.SkinType);
}