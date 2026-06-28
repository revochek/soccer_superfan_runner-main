using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    private float _targetTime;
    private float _timer = 0f;

    private bool _isRunning = true;

    public event UnityAction Completed;


    public static Timer CreateInstance
    {
        get
        {
            var go = new GameObject("[Buff Timer]");
            var buffTimer = go.AddComponent<Timer>();

            return buffTimer;
        }
    }

    private void Update()
    {
        if (!_isRunning) return;

        _timer += Time.deltaTime;

        if(_timer >= _targetTime)
        {
            StopTimer();

            Completed?.Invoke();
            Destroy(gameObject);
        }
    }

    public void StartTimer(float targetTime)
    {
        _isRunning = true;

        _targetTime = targetTime;
    }

    private void StopTimer()
    {
        _isRunning = false;
    }
}
