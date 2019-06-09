using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TankMsg;
class MallHandler
{
    public void RegisterMsg(Dictionary<MsgID, Action<SocketModel>> handlers)
    {
        handlers.Add(MsgID.RspBuy, OnRspBuy);
    }

    private void OnRspBuy(SocketModel modle)
    {
        RspBuy rsp = SerializeUtil.Deserialize<RspBuy>(modle.message);

        CacheManager.instance.diamond = rsp.diamond;
        CacheManager.instance.items.Add(rsp.item);

        MessageBox.Show("购买成功");
    }
}

