using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TankMsg;
public class GameManager : MonoBehaviour
{
    const string ip = "192.168.15.18";
    const int port = 10010;
    HandlerCenter handlerCenter = new HandlerCenter();

    // Use this for initialization
    void Start()
    {
        ConfigManager.instance.Init();

        NetClient.instance.ConnectServer(ip, port, handlerCenter);

        UIManager.instance.Open<VideoWindow>();

        Screen.SetResolution(1280, 720, false);
    }

    // Update is called once per frame
    void Update()
    {        
        NetClient.instance.Update();

        float dt = Time.deltaTime;
        TankManager.instance.Update(dt);
        UIManager.instance.Update(dt);
    }

    void OnApplicationQuit()
    {
        NetClient.instance.Disconnect();
    }
}
