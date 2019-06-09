using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankMsg;

class MallHandler
{
    public void RegisterMsg(Dictionary<MsgID, Action<UserToken, SocketModel>> handlers)
    {
        handlers.Add(MsgID.ReqBuy, OnReqBuy);
    }

    private void OnReqBuy(UserToken token, SocketModel model)
    {
        ReqBuy req = SerializeUtil.Deserialize<ReqBuy>(model.message);
        AccountData acc = CacheManager.instance.GetAccount(token.accountid);
        ItemCfg cfg = ConfigManager.instance.items[req.itemid];
        if (acc.diamond < cfg.Price)
        {
            TipsError tips = new TipsError();
            tips.code = (int)ECode.EBuyError;
            NetworkManager.Send<TipsError>(token, (int)MsgID.TipError, tips);
        }
        else
        {
            acc.diamond -= cfg.Price;

            ItemData item = new ItemData();
            item.account = token.accountid;
            item.itemid = req.itemid;
            item.count = 1;           
            CacheManager.instance.AddItem(token.accountid, item);

            RspBuy rsp = new RspBuy();
            rsp.diamond = acc.diamond;
            rsp.item = new ItemDTO();
            rsp.item.id = item.Id;
            rsp.item.account = item.account;
            rsp.item.itemid = item.itemid;
            rsp.item.count = item.count;
            rsp.item.slot = item.slot;

            NetworkManager.Send<RspBuy>(token, (int)MsgID.RspBuy, rsp);
        }
    }
}

