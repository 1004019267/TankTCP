using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TankMsg;
using UnityEngine;
class AccountHandler
{
    public void RegisterMsg(Dictionary<MsgID, Action<SocketModel>> handlers)
    {
        handlers.Add(MsgID.TipError, OnTipError);
        handlers.Add(MsgID.RspRegister, OnRspRegister);
        handlers.Add(MsgID.RspLogin, OnRspLogin);       
        handlers.Add(MsgID.NotifyOffline, OnNotifyOffline);
    }

    private void OnTipError(SocketModel model)
    {
        TipsError rsp = SerializeUtil.Deserialize<TipsError>(model.message);
        Debug.LogError(rsp.code);
    }

    private void OnRspRegister(SocketModel model)
    {
        RspRegister rsp = SerializeUtil.Deserialize<RspRegister>(model.message);

        UIManager.instance.Close<RegisterWindow>();
        UIManager.instance
            .Open<LoginWindow>().Init();
    }

    void OnRspLogin(SocketModel model)
    {
        RspLogin rsp = SerializeUtil.Deserialize<RspLogin>(model.message);
        CacheManager.instance.account = rsp.account;
        CacheManager.instance.pwd = rsp.pwd;
        CacheManager.instance.nickname = rsp.nickname;

        CacheManager.instance.items = rsp.items;
        CacheManager.instance.equip = rsp.equip;

        UIManager.instance.Close<LoginWindow>();
        UIManager.instance.Open<MainWindow>().Init();
    }

    void OnNotifyOffline(SocketModel model)
    {
        NotifyOffline notify = SerializeUtil.Deserialize<NotifyOffline>(model.message);
   
        //销毁坦克
        OtherTank tank = TankManager.instance.GetTank(notify.id) as OtherTank;
        if (tank != null)
            tank.Destory();

        //移除坦克对象
        TankManager.instance.RemoveTank(tank);
    }
}

