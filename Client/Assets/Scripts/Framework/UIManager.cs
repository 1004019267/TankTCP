using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using System.Linq;

/// <summary>
/// 所有界面的基类
/// </summary>
public abstract class BaseWindow
{
    protected Transform _transform;

    /// <summary>
    /// 打开界面
    /// </summary>
    /// <param name="canvas"></param>
    /// <param name="windowName"></param>
    public void Open(Transform canvas, string windowName)
    {
        Object obj = Resources.Load("UI/" + windowName);
        _transform = (GameObject.Instantiate(obj) as GameObject).transform;
        _transform.parent = canvas;

        _transform.localPosition = Vector3.zero;
        _transform.localScale = Vector3.one;
        _transform.name = windowName;
    }

    /// <summary>
    /// 关闭界面
    /// </summary>
    public virtual void Close()
    {
        GameObject.Destroy(_transform.gameObject);
    }

    public virtual void Update(float dt) { }
}

/// <summary>
/// UI管理器
/// </summary>
public class UIManager : Singleton<UIManager>
{
    // 存储所有打开的界面， key-界面名称， value-窗口名称
    private Dictionary<string, BaseWindow> _windows = new Dictionary<string, BaseWindow>();
    List<BaseWindow> windows = new List<BaseWindow>();

    private Transform _canvas;

    private Camera _uiCamera;

    public Camera uiCamera { get { return _uiCamera; } }

    public UIManager()
    {
        GameObject ui = GameObject.Find("UI");
        MonoBehaviour.DontDestroyOnLoad(ui);

        _canvas = ui.transform.Find("Canvas");

        _uiCamera = ui.transform.Find("Camera").GetComponent<Camera>();
    }

    /// <summary>
    /// 打开界面
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// typeof找类的类型
    public T Open<T>() where T : BaseWindow, new()
    {
        string windowName = typeof(T).Name;
        if (_windows.ContainsKey(windowName))
        {
            return _windows[windowName] as T;
        }
        else
        {
            T wnd = new T();
            wnd.Open(_canvas, windowName);
            _windows.Add(windowName, wnd);
            return wnd;
        }
    }

    /// <summary>
    /// 关闭界面
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void Close<T>() where T : BaseWindow
    {
        string windowName = typeof(T).Name;
        if (_windows.ContainsKey(windowName))
        {
            _windows[windowName].Close();
            _windows.Remove(windowName);
        }
    }

    /// <summary>
    /// 获取一个界面
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T Get<T>() where T : BaseWindow
    {
        string windowName = typeof(T).Name;
        if (_windows.ContainsKey(windowName))
        {
            return _windows[windowName] as T;
        }
        else
        {
            Debug.LogError("这个界面没有打开");
            return null;
        }
    }

    public void Update(float dt)
    {
        //清空列表
        windows.Clear();
        foreach (BaseWindow wnd in _windows.Values)
        {
            if (wnd != null)
                windows.Add(wnd);
        }
        for (int i = 0; i < windows.Count; i++)
        {
            windows[i].Update(dt);
        }
    }

    /// <summary>
    /// 关闭所有界面
    /// </summary>
    public void CloseAll()
    {
        foreach (KeyValuePair<string, BaseWindow> wnd in _windows.ToArray())
        {
            wnd.Value.Close();
            _windows.Remove(wnd.Key);
        }
    }
}