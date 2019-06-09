using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankMsg;
public struct Color
{
    public float r;
    public float g;
    public float b;
}


class CacheManager : Singleton<CacheManager>
{
    //保存已连接的客户端
    List<AccountData> tokens = new List<AccountData>();

    public Queue<UserToken> offlineTokens = new Queue<UserToken>();

    List<Room> rooms = new List<Room>();

    public List<Arena> arenas = new List<Arena>();

    List<Survival> survivals = new List<Survival>();

    Dictionary<string, List<ItemData>> items = new Dictionary<string, List<ItemData>>();

    Dictionary<string, EquipData> equips = new Dictionary<string, EquipData>();

    int roomidCounter = 1001;

    int battleidCounter = 1001;

    public void LoadAccountAll(string account)
    {
        //载入装备
        string sql = string.Format("SELECT * FROM equip WHERE account = '{0}'", account);
        EquipData equip = MysqlManager.instance.ExecQuery<EquipData>(sql)[0];
        equips.Add(account, equip);

        //载入仓库数据
        sql = string.Format("SELECT * FROM warehouse WHERE account = '{0}'", account);
        List<ItemData> accItem = MysqlManager.instance.ExecQuery<ItemData>(sql);
        items.Add(account, accItem);
        //载入好友数据

        //载入工会数据
    }

    public void WriteAccountAll(string account)
    {
        if (!equips.ContainsKey(account)) return;
        //写入装备数据
        EquipData equip = equips[account];
        string sql = string.Format("UPDATE equip SET tank = {0},bullet = {1} WHERE account = '{2}'", equip.tank, equip.bullet, account);
        MysqlManager.instance.ExecNonQuery(sql);

        //写入仓库数据
        sql = string.Format("DELETE FROM warehouse WHERE account = '{0}'", account);
        MysqlManager.instance.ExecNonQuery(sql);

        List<ItemData> accItem = items[account];
        for (int i = 0; i < accItem.Count; i++)
        {
            ItemData item = accItem[i];
            sql = string.Format("INSERT INTO warehouse (account,itemid,count,slot) VALUES ('{0}',{1},{2},{3})", item.account, item.itemid, item.count, item.slot);
            MysqlManager.instance.ExecNonQuery(sql);
        }
    }

    public void Update(float dt)
    {
        List<Arena> arenas = CacheManager.instance.arenas;
        for (int i = 0; i < arenas.Count; i++)
        {
            Arena a = arenas[i];
            a.Update(dt);

            if (a.elapsedTime >= Arena.battleTime)
            {
                //通知战斗结束
                NotifyArenaEnd notify = new NotifyArenaEnd();
                notify.blueKillCount = a.blueKillCount;
                notify.redKillCount = a.redKillCount;

                if (notify.blueKillCount > notify.redKillCount)
                {
                    notify.winteam = (int)ETeam.Blue;
                }
                else if (notify.blueKillCount < notify.redKillCount)
                {
                    notify.winteam = (int)ETeam.Red;
                }
                else
                {
                    notify.winteam = (int)ETeam.None;
                }

                for (int j = 0; j < a.accounts.Count; j++)
                {
                    AccountData acc = a.accounts[j];
                    TankKillData d = new TankKillData();
                    d.account = acc.account;
                    d.deathCount = acc.deathCount;
                    d.hurt = acc.hurt;
                    d.kill = acc.killCount;
                    d.team = (int)acc.team;
                    notify.datas.Add(d);

                    acc.killCount = 0;
                    acc.team = ETeam.None;
                    acc.hurt = 0;
                    acc.deathCount = 0;
                }
                MsgSender.SendAll<NotifyArenaEnd>(a.accounts, (int)MsgID.NotifyArenaEnd, notify);

                arenas.Remove(a);
            }
        }
    }
    public void AddAccount(AccountData token)
    {
        tokens.Add(token);
    }

    public void RemoveAccount(string acc)
    {
        tokens.Remove(GetAccount(acc));
    }

    public AccountData GetAccount(string acc)
    {
        for (int i = 0; i < tokens.Count; i++)
        {
            if (tokens[i].account == acc)
            {
                return tokens[i];
            }
        }
        return null;
    }

    public List<AccountData> GetAllAccount()
    {
        return tokens;
    }

    public Room GetWaitRoom(EBattle type, int limitNum)
    {
        Room r = null;
        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i].type == type && rooms[i].accounts.Count < limitNum)
            {
                r = rooms[i];
            }
        }
        if (r == null)
        {
            r = new Room();
            r.type = type;
            r.roomid = roomidCounter++;

            r.limtNum = type == EBattle.Survival ? 4 : limitNum;
            rooms.Add(r);
        }
        return r;
    }

    public void AddRoom(Room m)
    {
        rooms.Add(m);
    }

    public void RemoveRoom(Room m)
    {
        rooms.Remove(m);
    }

    public void RemoveRoom(int matchid)
    {
        rooms.Remove(GetMatch(matchid));
    }

    public Room GetMatch(int matchid)
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i].roomid == matchid)
            {
                return rooms[i];
            }
        }
        return null;
    }

    public Battle CreateSurvival(int limitNum, List<AccountData> accs)
    {
        Survival survival = new Survival();
        survival.accounts = accs;
        survival.battleid = battleidCounter++;
        survival.limitNum = limitNum;
        survival.type = EBattle.Survival;

        //真随机
        Random r = new Random(System.Guid.NewGuid().GetHashCode());
        //为每个账号创建一个坦克
        for (int i = 0; i < accs.Count; i++)
        {
            AccountData a = accs[i];
            a.battleid = survival.battleid;

            Tank t = new Tank();
            t.uid = a.account;
            t.nickName = a.nickname;
            t.hp = 100;

            t.color = new Color
            {
                r = (float)r.NextDouble(),
                g = (float)r.NextDouble(),
                b = (float)r.NextDouble(),
            };
            t.pos = SpawnPoint.point[r.Next(0, 5)];
            a.battleType = EBattle.Survival;

            survival.AddTank(t);
        }
        survivals.Add(survival);

        return survival;
    }

    public Arena CreateArena(int limitNum, List<AccountData> accs)
    {
        Arena arena = new Arena();
        arena.accounts = accs;
        arena.battleid = battleidCounter++;
        arena.limitNum = limitNum;
        arena.type = EBattle.Arena;


        //为每个账号创建一个坦克
        for (int i = 0; i < accs.Count; i++)
        {
            AccountData a = accs[i];
            a.battleid = arena.battleid;

            Tank t = new Tank();
            t.uid = a.account;
            t.hp = 100;
            t.nickName = a.nickname;
            t.team = i % 2 == 0 ? ETeam.Red : ETeam.Blue;

            //真随机
            Random r = new Random(System.Guid.NewGuid().GetHashCode());
            t.color = t.team == ETeam.Red ? new Color { r = 1, g = 0, b = 0 } : new Color { r = 0, g = 0, b = 1 };

            t.pos = SpawnPoint.point[r.Next(0, 5)];

            a.team = t.team;
            a.battleType = EBattle.Arena;

            arena.AddTank(t);
        }

        arenas.Add(arena);

        return arena;
    }

    public Battle GetBattle(int battleid, EBattle battleType)
    {
        if (battleType == EBattle.Arena)
        {
            for (int i = 0; i < arenas.Count; i++)
            {
                if (arenas[i].battleid == battleid)
                {
                    return arenas[i];
                }
            }
            return null;
        }
        else
        {
            for (int i = 0; i < survivals.Count; i++)
            {
                if (survivals[i].battleid == battleid)
                {
                    return survivals[i];
                }
            }
            return null;
        }
    }

    public void AddItem(string account, ItemData data)
    {
        if (items.ContainsKey(account))
        {
            items[account].Add(data);
        }
    }

    //获取该账号下所有物品
    public List<ItemData> GetAccountItemData(string account)
    {
        return items[account];
    }

    //移除该账号下所有物品
    public void RemoveAccountItem(string account)
    {
        items.Remove(account);
    }

    //获取该账号下所有装备
    public EquipData GetAccountEquip(string account)
    {
        return equips[account];
    }

    //移除该账号下所有装备
    public void RemoveAccountEquip(string account)
    {
        equips.Remove(account);
    }

    //切换坦克
    public void ChangeEquipTank(string account, int tank)
    {
        int currEquipTank = equips[account].tank;

        equips[account].tank = tank;

        List<ItemData> accItems = items[account];
        for (int i = 0; i < accItems.Count; i++)
        {
            if (accItems[i].itemid == tank)
            {
                accItems[i].itemid = currEquipTank;
            }
        }
    }
    //切换子弹
    public void ChangeEquipBullet(string account, int bullet)
    {
        int currEquipBullet = equips[account].bullet;

        equips[account].bullet = bullet;

        List<ItemData> accItems = items[account];
        for (int i = 0; i < accItems.Count; i++)
        {
            if (accItems[i].itemid == bullet)
            {
                accItems[i].itemid = currEquipBullet;
            }
        }
    }
}
