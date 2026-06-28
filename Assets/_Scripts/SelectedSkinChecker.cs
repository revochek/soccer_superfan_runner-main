using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedSkinChecker : IShopItemVisitor
{
    private IPersistentData _persistentData;

    public bool IsSelected { get; private set; }

    public SelectedSkinChecker(IPersistentData persistentData) => _persistentData = persistentData;

    public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

    public void Visit(HeroSkinItem heroSkinItem)
        => IsSelected = _persistentData.PlayerData.SelectedHeroSkin == heroSkinItem.SkinType;
}