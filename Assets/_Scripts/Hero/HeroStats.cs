using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroStats : MonoBehaviour
{
    [SerializeField] private HeroData _baseHeroStats;

    public float MovementSpeed = 1.5f;

    public void ResetToBase()
    {
        MovementSpeed = _baseHeroStats.MovementSpeed;
    }
}
