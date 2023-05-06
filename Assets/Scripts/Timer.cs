using UnityEngine;
using System;

public class Timer : MonoBehaviour
{
    private bool timing;
    private float curTime;
    private float duration;
    private Action callback;

    private void Update()
    {
        if (timing)
        {
            curTime += Time.deltaTime;
            if (curTime >= duration)
            {
                callback();
                timing = false;
            }
        }
    }


    public void TimerCountDown(float _duration, Action _callback)
    {
        timing = true;
        duration = _duration;
        callback = _callback;
        curTime = Time.time;
    }
}
