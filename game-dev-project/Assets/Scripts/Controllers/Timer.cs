using System;
using UnityEngine;

public class Timer : Singleton<Timer>
{
    public Action<float> onTimeTick;
    public float CurrentTime => currentTime;
    private float currentTime = 0;
    private bool isCounting;

    private void Start() {
        isCounting = true;
    }

    public void ResetTimer()
    {
        currentTime = 0;
    }

    public void StartTimer()
    {
        isCounting = true;
    }

    public void StopTimer()
    {
        isCounting = false;
    }
    
    private void Update()
    {
        if (isCounting)
        {
            currentTime += Time.deltaTime;
            onTimeTick?.Invoke(currentTime);
        }
    }
}
