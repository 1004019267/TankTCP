using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class AccountData
{
    public int Id;
    public string account;
    public string pwd;
    public string nickname;
    public int diamond;

    public int roomid;
    public int battleid;

    public UserToken token;

    //击杀数
    public int killCount;
    //死亡次数
    public int deathCount;
    //伤害总量
    public int hurt;
    //第几名
    public int order;

    public ETeam team;

    public EBattle battleType;
}

