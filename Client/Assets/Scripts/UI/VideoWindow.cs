using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TankMsg;


class VideoWindow : BaseWindow
{
    bool isVideo = true;
 
    public override void Update(float dt)
    {
        base.Update(dt);
        if (Input.GetKeyDown(KeyCode.Escape) && isVideo == true)
        {
            isVideo = false;
            UIManager.instance.Close<VideoWindow>();
            UIManager.instance.Open<LoginWindow>().Init();
        }        
    }
}

