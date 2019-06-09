using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TankMsg;
using UnityEngine;
class OtherTank : BaseTank
{
    public OtherTank(TankDTO dto) : base(dto)
    {
    }

    public override void Fire(Vector3 pos, Vector3 rot)
    {
        var go = Resources.Load("CompleteShell");

        var bullet = (GameObject.Instantiate(go) as GameObject).transform;
        bullet.eulerAngles = rot;
        bullet.position = pos+bullet.forward*2;

        bullet.gameObject.AddComponent<OtherBulletPrefab>().dir = bullet.forward;
    }
    public override void Move(Vector3 pos,Vector3 rot)
    {
        transfrom.position = pos;
        transfrom.eulerAngles = rot;
    }
}

