using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class SceneLoaderManager : MonoBehaviour
{
    public static SceneLoaderManager Instance { get; private set; }
    private Scene _targetScene;
    public Scene TargetScene => _targetScene;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Debug.Log("TargetScene: " + TargetScene);
    }

    public void LoadMainMenuScene()
    {
        _targetScene = Scene.MenuScene;
        LoadLoadingScene();

    }

    public void LoadGameLevelScene()
    {
        _targetScene = Scene.GameScene;
        LoadLoadingScene();

    }

    private void LoadLoadingScene()
    {
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }
}
public enum Scene
{
    MenuScene,
    GameScene,
    LoadingScene
}