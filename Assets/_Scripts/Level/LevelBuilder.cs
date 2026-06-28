using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelBuilder : MonoBehaviour
{
    private PlayerData _data;
    [SerializeField] private RewardManager _rewardManager;

    public void OnSceneLoaded(LevelConfiguration config)
    {
        Instantiate(config.StadiumPrefab, config.StadiumPrefab.transform.position, Quaternion.identity, null);
        _rewardManager.Initialize(config.RewardCoins);
    }
}