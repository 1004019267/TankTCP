using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TankMsg;
class RoomHandler
{
    public void RegisterMsg(Dictionary<MsgID, Action<SocketModel>> handlers)
    {
        handlers.Add(MsgID.RspEnterRoom, OnRspEnterRoom);
        handlers.Add(MsgID.NotifyBattleStart, OnNotifyBattleStart);
        handlers.Add(MsgID.NotifyBattleEnd, OnNotifyBattleEnd);
    }

    private void OnNotifyBattleEnd(SocketModel model)
    {
        NotifyBattleEnd notify = SerializeUtil.Deserialize<NotifyBattleEnd>(model.message);

        UIManager.instance.Open<BattleEndWindow>().Init(notify.data);
    }

    private void OnRspEnterRoom(SocketModel model)
    {
        RspEnterRoom rsp = SerializeUtil.Deserialize<RspEnterRoom>(model.message);
        CacheManager.instance.roomid = rsp.roomid;
    }

    private void OnNotifyBattleStart(SocketModel model)
    {
        NotifyBattleStart notify = SerializeUtil.Deserialize<NotifyBattleStart>(model.message);

        CacheManager.instance.battleid = notify.battleid;
        CacheManager.instance.battleType = (EBattle)notify.battleType;
        CacheManager.instance.limitNum = notify.numberLimit;

        UIManager.instance.CloseAll();

        for (int i = 0; i < notify.tanks.Count; i++)
        {
            //修改缓存
            //CacheManager.instance.tanks.Add(notify.tanks[i]);

            BaseTank tank = null;
            if (notify.tanks[i].id == CacheManager.instance.account)
            {
                //CacheManager.instance.myTank = notify.tanks[i];
                tank = new Tank(notify.tanks[i]);
            }
            else
            {
                tank = new OtherTank(notify.tanks[i]);
            }
            TankManager.instance.AddTank(tank);
        }
    }
}


