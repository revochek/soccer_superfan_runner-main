using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StarsItem", menuName = "Shop/StarsItem")]
public class StarsItem : ScriptableObject
{
    [field: SerializeField, Range(0, 10000)] public int Stars { get; private set; }
    [field: SerializeField, Range(0, 10000)] public int Price { get; private set; }
}

