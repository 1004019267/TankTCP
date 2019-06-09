using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Luna3D;


public class Tank
{
    public string uid;
    public int hp;
    public string nickName;
    public Color color;
    public Vector3 pos;
    public Vector3 rot;

    public float lastFireTime;
    public float lastHitTime;
    public float lastMoveTime;
    public Vector3 lastPos;

    public ETeam team = ETeam.None;
}

