using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HeroData", menuName = "Game Data/Hero Character Data")]
public class HeroData : ScriptableObject
{
    [SerializeField] private float _movementSpeed = 1.5f;
    public float MovementSpeed => _movementSpeed;
}
