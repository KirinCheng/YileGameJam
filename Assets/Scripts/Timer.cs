using UnityEngine;
using System;

public class Timer : MonoBehaviour
{
    private bool timing;
    private float curTime;
    private float duration;
    private float targetTime;
    private Action callback;

    private void Update()
    {
        if (timing)
        {
            curTime += Time.deltaTime;
            if (curTime >= targetTime)
            {
                callback();
                timing = false;
            }
        }
    }


    public void TimerCountDown(float _duration, Action _callback)
    {
        if (timing)
            return;
        timing = true;
        duration = _duration;
        callback = _callback;
        curTime = Time.time;
        targetTime = Time.time + duration;
    }

    public void StopTimer()
    {
        timing = false;
        callback = null;
    }
}
