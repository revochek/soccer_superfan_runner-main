using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShopItemVisitor
{
    void Visit(ShopItem shopItem);
    void Visit(HeroSkinItem characterSkinItem);
}