using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public Action<float> onTimeTick;
    public bool IsCounting { get; set; }
    private float currentTime = 0;
    
    void Start() {
        IsCounting = true;
    }
    
    void Update()
    {
        if (IsCounting)
        {
            currentTime += Time.deltaTime;
            onTimeTick?.Invoke(currentTime);
        }
    }
}
