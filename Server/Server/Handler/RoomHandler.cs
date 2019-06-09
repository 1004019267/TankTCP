using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankMsg;

class RoomHandler
{
    public void RegisterMsg(Dictionary<MsgID, Action<UserToken, SocketModel>> handlers)
    {
        handlers.Add(MsgID.ReqEnterRoom, OnReqEnterRoom);
    }

    private void OnReqEnterRoom(UserToken token, SocketModel model)
    {
        ReqEnterRoom req = SerializeUtil.Deserialize<ReqEnterRoom>(model.message);
        AccountData acc = CacheManager.instance.GetAccount(token.accountid);
        if (acc.roomid != 0) return;  

        Room r = CacheManager.instance.GetWaitRoom((EBattle)req.battleType, req.limitNumber);

        r.AddAccount(acc);

        RspEnterRoom rsp = new RspEnterRoom();
        rsp.roomid = r.roomid;
        NetworkManager.Send<RspEnterRoom>(token, (int)MsgID.RspEnterRoom, rsp);

        //房间满
        if (r.accounts.Count >= r.limtNum)
        {
            Battle b = null;
            if (req.battleType==(int)EBattle.Arena)
            {
                b = CacheManager.instance.CreateArena(r.limtNum, r.accounts);
            }
            else
            {
                b = CacheManager.instance.CreateSurvival(r.limtNum, r.accounts);
            }
           
            //清空roomid
            for (int i = 0; i < r.accounts.Count; i++)
            {
                r.accounts[i].roomid = 0;
            }

            //通知战斗开始
            NotifyBattleStart notify = new NotifyBattleStart();
            notify.battleid = b.battleid;           
            notify.battleType = req.battleType;
            notify.numberLimit = req.limitNumber;

            List<Tank> tanks = b.GetALLTanks();

            for (int i = 0; i < tanks.Count; i++)
            {
                Tank t1 = tanks[i];
                TankDTO dto = new TankDTO();
                dto.id = t1.uid;
                dto.hp = t1.hp;
                dto.nickName = t1.nickName;
                dto.pos = Tools.ToVec_3(t1.pos);
                dto.color = Tools.UC2TC(t1.color);
                dto.team = (int)t1.team;
                notify.tanks.Add(dto);
            }
            MsgSender.SendAll<NotifyBattleStart>(r.accounts, (int)MsgID.NotifyBattleStart, notify);
            CacheManager.instance.RemoveRoom(r.roomid);
        }
    }
}
