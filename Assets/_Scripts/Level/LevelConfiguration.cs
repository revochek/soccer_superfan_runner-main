using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "LevelConfigurationData", menuName = "Game Data/Level Configuration Data")]
public class LevelConfiguration : ScriptableObject
{
    [SerializeField] private GameObject _stadiumPrefab;
    [SerializeField] private int _rewardCoins;

    public GameObject StadiumPrefab => _stadiumPrefab; 
    public int RewardCoins => _rewardCoins;
}
