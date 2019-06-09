﻿using System.Collections;
using System.Collections.Generic;


public class Singleton<T> where T : class, new()
{

    static T _instance = default(T);
    static readonly object syslock = new object();

    public static T instance
    {
        get
        {
            if (_instance == null)
            {
                lock (syslock)
                {
                    _instance = new T();
                }
            }
            return _instance;
        }
    }
}
