
using System.Collections;
using System.Collections.Generic;
using TankMsg;
using UnityEngine;
class Tank : BaseTank
{
    //跟随相机
    Camera camera;

    //相机和坦克的距离
    Vector3 offset;

    const float speed = 5;
    //上次开火时间
    float lastFireTime;
    //普攻冷却
    float cd = 1f;
    public Tank(TankDTO dto) : base(dto)
    {
        if (CacheManager.instance.battleType == EBattle.Arena)
        {
          UIManager.instance.Open<TimeWindow>().Init();
        }
        camera = Camera.main;
        camera.transform.position = transfrom.position + new Vector3(0, 4, -5);
        offset = camera.transform.position - transfrom.position;
    }

    public override void Hurt(int damage)
    {
        base.Hurt(damage);
    }

    public override void Fire(Vector3 pos, Vector3 rot)
    {
        var go = Resources.Load("CompleteShell");

        var bullet = (GameObject.Instantiate(go) as GameObject).transform;

        Vector3 delta = transfrom.forward * 2;
        bullet.position = transfrom.position + new Vector3(0, 1.6f, 0) + delta;
        bullet.eulerAngles = transfrom.eulerAngles;
        bullet.gameObject.AddComponent<BulletPrefab>().dir = transfrom.forward;
    }

    public override void Update(float dt)
    {
        UpDateInput(dt);
        camera.transform.position = transfrom.position + offset;
    }
    public void UpDateInput(float dt)
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        if (x != 0 || y != 0)
        {
            Vector3 dir = new Vector3(x, 0, y);

            //移动的趋势
            Vector3 dest = transfrom.position + dir * dt * speed;

            RaycastHit hit1;
            RaycastHit hit2;
            Vector3 origin = transfrom.position + new Vector3(0, 0.2f, 0);

            Vector3 direction = (dest - transfrom.position).normalized;
            if (Physics.Raycast(origin, transfrom.forward, out hit1, 1f) && Physics.Raycast(origin, direction, out hit2, 1f))
            {
            }
            else
            {
                //方向
                transfrom.forward = direction;
                //坐标
                transfrom.position = dest;

                ReqMove req = new ReqMove();
                req.pos = Tools.ToVec_3(transfrom.position);
                req.rot = Tools.ToVec_3(transfrom.eulerAngles);
                NetClient.instance.Send<ReqMove>((int)MsgID.ReqMove, req);
            }
        }
        //开火
        if (Input.GetMouseButton(0))
        {
            if (Time.time - lastFireTime >= cd)
            {
                //发射
                Fire(transfrom.position, transfrom.eulerAngles);

                ReqFire req = new ReqFire();
                req.pos = Tools.ToVec_3(transfrom.position + new Vector3(0, 1, 0));
                req.rot = Tools.ToVec_3(transfrom.eulerAngles);

                NetClient.instance.Send<ReqFire>((int)MsgID.ReqFire, req);
                lastFireTime = Time.time;
            }
        }
    }
}

