using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankMsg;

class AccountHandler
{
    public void RegisterMsg(Dictionary<MsgID, Action<UserToken, SocketModel>> handlers)
    {
        handlers.Add(MsgID.ReqRegister, OnReqRegister);
        handlers.Add(MsgID.ReqLogin, OnReqLogin);
    }

    private void OnReqRegister(UserToken token, SocketModel model)
    {
        ReqRegister req = SerializeUtil.Deserialize<ReqRegister>(model.message);

        string sql = string.Format("SELECT * FROM account WHERE account = '{0}' ", req.account);
        List<Account> accounts = MysqlManager.instance.ExecQuery<Account>(sql);
        if (accounts.Count > 0)
        {
            TipsError tip = new TipsError();
            tip.code = (int)ECode.ERegisterError;
            NetworkManager.Send<TipsError>(token, (int)MsgID.TipError, tip);
        }
        else
        {
            RspRegister rsp = new RspRegister();
            rsp.account = req.account;
            rsp.pwd = req.pwd;
            rsp.nickname = req.nickname;
            NetworkManager.Send(token, (int)MsgID.RspRegister, rsp);

            //插入账号数据
            sql = string.Format("INSERT INTO account (account,pwd,nickname,diamond) VALUES ('{0}','{1}','{2}',{3})", req.account, req.pwd, req.nickname, 10000);
            MysqlManager.instance.ExecNonQuery(sql);

            //插入装备数据
            sql = string.Format("INSERT INTO equip (account,tank,bullet) VALUES ('{0}',{1},{2})", req.account, 1001, 1006);
            MysqlManager.instance.ExecNonQuery(sql);
        }
    }

    void OnReqLogin(UserToken token, SocketModel model)
    {
        ReqLogin req = SerializeUtil.Deserialize<ReqLogin>(model.message);

        Console.WriteLine(req.account + "请求登录");

        string sql = string.Format("SELECT * FROM account WHERE account = '{0}' AND pwd = '{1}'", req.account, req.pwd);
        List<Account> accounts = MysqlManager.instance.ExecQuery<Account>(sql);
        if (accounts.Count > 0)
        {
            if (CacheManager.instance.GetAccount(req.account) == null)
            {
                token.accountid = req.account;

                AccountData acc = new AccountData();
                acc.account = req.account;
                acc.pwd = req.pwd;
                acc.nickname = accounts[0].nickname;
                acc.Id = accounts[0].Id;
                acc.token = token;
                acc.diamond = accounts[0].diamond;

                CacheManager.instance.AddAccount(acc);
                CacheManager.instance.LoadAccountAll(acc.account);

                RspLogin rsp = new RspLogin();
                rsp.account = req.account;
                rsp.diamond = acc.diamond;
                rsp.nickname = acc.nickname;

                List<ItemData> accItems = CacheManager.instance.GetAccountItemData(req.account);
                for (int i = 0; i < accItems.Count; i++)
                {
                    ItemData item = accItems[i];
                    ItemDTO dto = new ItemDTO();
                    dto.id = item.Id;
                    dto.account = item.account;
                    dto.itemid = item.itemid;
                    dto.count = item.count;
                    dto.slot = item.slot;                    

                    rsp.items.Add(dto);
                }

                EquipData equip = CacheManager.instance.GetAccountEquip(req.account);

                rsp.equip = new EquipDTO();
                rsp.equip.id = equip.Id;
                rsp.equip.account = equip.account;
                rsp.equip.tank = equip.tank;
                rsp.equip.bullet = equip.bullet;


                NetworkManager.Send<RspLogin>(token, (int)MsgID.RspLogin, rsp);
            }
            else
            {
                TipsError tips = new TipsError();
                tips.code = (int)ECode.ELoginError;

                NetworkManager.Send<TipsError>(token, (int)MsgID.TipError, tips);
            }
        }
        else
        {
            TipsError tips = new TipsError();
            tips.code = (int)ECode.ELoginError;

            NetworkManager.Send<TipsError>(token, (int)MsgID.TipError, tips);
        }


        //for (int i = 0; i < CacheManager.instance.GetAllAccount().Count; i++)
        //{
        //    Console.WriteLine(CacheManager.instance.GetAllAccount()[i].account); 
        //}
    }
}
