using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    private LevelConfiguration[] _config;
    [SerializeField]
    private LevelBuilder levelBuilder;

    private IDataProvider _dataProvider;
    private IPersistentData _persistentPlayerData;

    public void Initialize(IPersistentData persistentPlayerData, IDataProvider dataProvider)
    {
        _persistentPlayerData = persistentPlayerData;
        _dataProvider = dataProvider;
    }

    private void Start()
    {
        levelBuilder.OnSceneLoaded(_config[GetLevelToLoad()]);
    }

    public void LoadLevel()
    {
        SceneLoaderManager.Instance.LoadGameLevelScene();
    }

    public void IncrementNextLevel() 
    {
        _persistentPlayerData.PlayerData.Level++;
        _dataProvider.Save();

    }

    private int GetLevelToLoad()
    {
        int startLevelAfterLimit = 5;
        int endLevelAfterLimit = 11;

        int levelRange = endLevelAfterLimit - startLevelAfterLimit;
        int playerCurrentLevel = _persistentPlayerData.PlayerData.Level;

        if (playerCurrentLevel >= _config.Length) 
        {
            return (playerCurrentLevel - startLevelAfterLimit) % (levelRange + 1) + startLevelAfterLimit;
        }
        else
        {
            return playerCurrentLevel;
        }
    }
}
