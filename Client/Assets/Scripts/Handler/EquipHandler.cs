using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TankMsg;

class EquipHandler
{
    public void RegisterMsg(Dictionary<MsgID, Action<SocketModel>> handlers)
    {
        handlers.Add(MsgID.RspUseTank, OnRspUseTank);
        handlers.Add(MsgID.RspUseBullet, OnRspUseBullet);
    }
    private void OnRspUseTank(SocketModel model)
    {
        RspUseTank rsp = SerializeUtil.Deserialize<RspUseTank>(model.message);

        //CacheManager.instance.equip.tank = rsp.itemid;

        UIManager.instance.Get<SelectWeaponWindow>().UpdataTank(rsp.itemid);
    }
    private void OnRspUseBullet(SocketModel model)
    {
        RspUseBullet rsp = SerializeUtil.Deserialize<RspUseBullet>(model.message);

        //CacheManager.instance.equip.bullet = rsp.itemid;

        UIManager.instance.Get<SelectWeaponWindow>().UpdataBullet(rsp.itemid);
    }


}

