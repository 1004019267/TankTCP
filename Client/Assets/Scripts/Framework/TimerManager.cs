using System;
using System.Collections.Generic;
using UnityEngine;


public class Timer
{
    // 延迟时间
    private float _delay;

    // 已经过去的时间
    private float _elapsedTime;

    // 延迟事件
    private Action _delayEvent;

    public bool end { private set; get; }

    public Timer(float delay, Action delayEvent)
    {
        _delay = delay;
        _delayEvent = delayEvent;
    }

    public void Update(float dt)
    {
        if(_elapsedTime >= _delay)
        {
            _delayEvent();
            end = true;
        }
        else
        {
            _elapsedTime += dt;
        }
    }
}

public class TimerManager : Singleton<TimerManager>
{
    private List<Timer> _timers = new List<Timer>();

    public void Update(float dt)
    {
        for(int i = 0; i < _timers.Count; i++)
        {
            Timer timer = _timers[i];
            if(timer.end)
            {
                _timers.Remove(timer);
            }
            else
            {
                timer.Update(dt);
            }
        }
    }

    /// <summary>
    /// 延迟调用一个方法
    /// </summary>
    /// <param name="delay"></param>
    /// <param name="delayEvent"></param>
    public void Invoke(float delay, Action delayEvent)
    {
        Timer timer = new Timer(delay, delayEvent);
        _timers.Add(timer);
    }

    public void Clear()
    {
        _timers.Clear();
    }
}
