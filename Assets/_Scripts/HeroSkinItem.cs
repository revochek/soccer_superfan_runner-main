using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HeroSkinItem", menuName = "Shop/HeroSkinItem")]
public class HeroSkinItem : ShopItem
{
    [field: SerializeField] public HeroSkins SkinType { get; private set; }
}
