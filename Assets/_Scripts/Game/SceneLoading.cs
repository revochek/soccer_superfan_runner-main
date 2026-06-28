using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class SceneLoading : MonoBehaviour
{
    private Scene _targetScene;

    private void Awake()
    {
        Debug.Log("SceneLoaderManager.Instance.TargetScene: " + SceneLoaderManager.Instance.TargetScene);

        _targetScene = SceneLoaderManager.Instance.TargetScene;

        if (SceneLoaderManager.Instance.TargetScene == null)
        {
            _targetScene = Scene.MenuScene;
        }

        SceneManager.LoadScene(_targetScene.ToString());
    }
}
