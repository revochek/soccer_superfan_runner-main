using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class Screenshoter : MonoBehaviour
{
    public Camera mainCamera;
    private int counter = 1;


    private void Start()
    {
        if (!Directory.Exists("Assets/Screenshots"))
        {
            Directory.CreateDirectory("Assets/Screenshots");
            Debug.Log("Created screenshots directory at Assets/Screenshots");
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeScreenshot();
        }
    }

    private void TakeScreenshot()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        string screenshotName = $"Assets/Screenshots/Screenshot_{counter:D2}_{mainCamera.pixelWidth}x{mainCamera.pixelHeight}_SceneID_{sceneName}.png";

        ScreenCapture.CaptureScreenshot(screenshotName);
        Debug.Log($"Screenshot saved to: {screenshotName}");

        counter++;
    }
}
