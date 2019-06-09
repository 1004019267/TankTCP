using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Battle
{
    public int battleid;
    public EBattle type;
    public int limitNum;

    //保存所有已加入房间的账号
    public List<AccountData> accounts = new List<AccountData>();

    List<Tank> tanks = new List<Tank>();
    
    //加入一个玩家
    public void AddAccount(AccountData acc)
    {
        accounts.Add(acc);
    }

    public void RemoveAccount(AccountData acc)
    {
        accounts.Remove(acc);
    }

    public void RemoveAccount(string account)
    {
        accounts.Remove(GetAccount(account));
    }

    public AccountData GetAccount(string account)
    {
        for (int i = 0; i < accounts.Count; i++)
        {
            if (accounts[i].account == account)
            {
                return accounts[i];
            }
        }
        return null;
    }

    public void AddTank(Tank t)
    {
        tanks.Add(t);
    }

    public void RemoveTank(string accountid)
    {
        tanks.Remove(GetTank(accountid));
    }
    public Tank GetTank(string accountid)
    {
        for (int i = 0; i < tanks.Count; i++)
        {
            if (tanks[i].uid == accountid)
            {
                return tanks[i];
            }
        }
        return null;
    }
    public List<Tank> GetALLTanks()
    {
        return tanks;
    }

}

