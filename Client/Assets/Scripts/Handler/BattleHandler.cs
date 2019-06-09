using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TankMsg;

class BattleHandler
{
    public void RegisterMsg(Dictionary<MsgID, Action<SocketModel>> handlers)
    {
        handlers.Add(MsgID.NotifyMove, OnNotifyMove);
        handlers.Add(MsgID.NotifyFire, OnNotifyFire);
        handlers.Add(MsgID.NotifyHit, OnNotifyHit);
        handlers.Add(MsgID.NotifyDeath, OnNotifyDeath);
        handlers.Add(MsgID.NotifyReborn, OnNotifyReborn);
        handlers.Add(MsgID.NotifyArenaEnd, OnNotifyArenaEnd);
    }

    private void OnNotifyArenaEnd(SocketModel model)
    {
        NotifyArenaEnd notify = SerializeUtil.Deserialize<NotifyArenaEnd>(model.message);

        UIManager.instance.Open<ArenaEndWindow>().Init(notify.datas,notify.winteam);
        TankManager.instance.ClearAll();
    }

    private void OnNotifyReborn(SocketModel model)
    {
        NotifyReborn notify = SerializeUtil.Deserialize<NotifyReborn>(model.message);
   
        BaseTank t = TankManager.instance.GetTank(notify.tank.id);
        if (t!=null)
        {
            TankManager.instance.RemoveTank(t);
            t.Clear();
        }
        BaseTank tank = null;
        if (notify.tank.id == CacheManager.instance.account)
        {          
            tank = new Tank(notify.tank);
        }
        else
        {
            tank = new OtherTank(notify.tank);
        }
        TankManager.instance.AddTank(tank);
    }

    void OnNotifyMove(SocketModel model)
    {
        NotifyMove notify = SerializeUtil.Deserialize<NotifyMove>(model.message);

        BaseTank tank = TankManager.instance.GetTank(notify.id);
        if (tank != null)
            tank.Move(Tools.ToVec3(notify.pos), Tools.ToVec3(notify.rot));
    }
    void OnNotifyFire(SocketModel model)
    {
        NotifyFire notify = SerializeUtil.Deserialize<NotifyFire>(model.message);

        OtherTank tank = TankManager.instance.GetTank(notify.id) as OtherTank;
        if (tank != null)
            tank.Fire(Tools.ToVec3(notify.pos), Tools.ToVec3(notify.rot));
    }
    void OnNotifyHit(SocketModel model)
    {
        NotifyHit notify = SerializeUtil.Deserialize<NotifyHit>(model.message);

        BaseTank tank = TankManager.instance.GetTank(notify.id);
        if (tank != null)
            tank.Hurt(notify.damage);
    }
    void OnNotifyDeath(SocketModel model)
    {
        NotifyDeath notify = SerializeUtil.Deserialize<NotifyDeath>(model.message);

        BaseTank tank = TankManager.instance.GetTank(notify.id);
        if (tank != null)
        {
            tank.Death();
            TankManager.instance.RemoveTank(tank);
        }
    }
}

