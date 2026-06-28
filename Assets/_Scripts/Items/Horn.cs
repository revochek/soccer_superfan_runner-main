using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horn : MonoBehaviour, ICoinsHolder
{
    [SerializeField] private int _coinCount;

    public int GetCoins()
    {
        return _coinCount;
    }
}
