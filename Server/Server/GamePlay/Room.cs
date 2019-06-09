using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Room
{
    public int roomid;
    public EBattle type;
    public int limtNum;

    //保存所有已加入房间的账号
    public List<AccountData> accounts = new List<AccountData>();
    //加入一个玩家
    public void AddAccount(AccountData acc)
    {
        acc.roomid = this.roomid;
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
}

