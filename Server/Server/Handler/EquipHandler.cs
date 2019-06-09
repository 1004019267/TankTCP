using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankMsg;

class EquipHandler
{
    public void RegisterMsg(Dictionary<MsgID, Action<UserToken, SocketModel>> handlers)
    {
        handlers.Add(MsgID.ReqUseTank, OnReqUseTank);
        handlers.Add(MsgID.ReqUseBullet, OnReqUseBullet);
    }

    private void OnReqUseTank(UserToken token, SocketModel model)
    {
        ReqUseTank req = SerializeUtil.Deserialize<ReqUseTank>(model.message);

        CacheManager.instance.ChangeEquipTank(token.accountid, req.itemid);

        RspUseTank rsp = new RspUseTank();
        rsp.itemid = req.itemid;
        NetworkManager.Send<RspUseTank>(token, (int)MsgID.RspUseTank, rsp);
    }
    private void OnReqUseBullet(UserToken token, SocketModel model)
    {
        ReqUseBullet req = SerializeUtil.Deserialize<ReqUseBullet>(model.message);

        CacheManager.instance.ChangeEquipBullet(token.accountid, req.itemid);

        RspUseBullet rsp = new RspUseBullet();
        rsp.itemid = req.itemid;
        NetworkManager.Send<RspUseBullet>(token, (int)MsgID.RspUseBullet, rsp);
    }
}

