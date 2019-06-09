using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TankMsg;


class TimeWindow : BaseWindow
{
    Text textTime;
    float time = 120;
    public void Init()
    {
        textTime = _transform.Find("Time").GetComponent<Text>();
    }
    public override void Update(float dt)
    {
        base.Update(dt);       
        time -= dt;
        textTime.text = ((int)time).ToString();
    }
}

