using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankMsg;
using Luna3D;
class BattleHandler
{
    public void RegisterMsg(Dictionary<MsgID, Action<UserToken, SocketModel>> handlers)
    {
        handlers.Add(MsgID.ReqMove, OnReqMove);
        handlers.Add(MsgID.ReqFire, OnReqFire);
        handlers.Add(MsgID.ReqHit, OnReqHit);
    }
    private void OnReqMove(UserToken token, SocketModel model)
    {
        ReqMove req = SerializeUtil.Deserialize<ReqMove>(model.message);

        //保存坦克的实时坐标
        AccountData acc = CacheManager.instance.GetAccount(token.accountid);
        if (acc == null) return;

        Battle battle = CacheManager.instance.GetBattle(acc.battleid, acc.battleType);

        if (battle == null) return;

        Tank t = battle.GetTank(token.accountid);
        if (t != null && t.hp > 0)
        {
            ////校验两帧的移动距离
            //float deltaTime = Time.time * 0.001f - t.lastMoveTime;
            //float distance = deltaTime * 5;

            //float reqDistance = Luna3D.Vector3.Distance(Tools.ToLunaVec3(req.pos), t.lastPos);
            //if (reqDistance > distance + 0.2f) return;

            //t.lastMoveTime = Time.time * 0.001f;

            //修改缓存
            t.pos = new Vector3(req.pos.x, 0, req.pos.z);
            t.rot = Tools.ToLunaVec3(req.rot);

            t.lastPos = t.pos;
            //给别人发通知
            NotifyMove notify = new NotifyMove();
            notify.id = token.accountid;
            notify.pos = Tools.ToVec_3(t.pos);
            notify.rot = req.rot;
            MsgSender.SendOther<NotifyMove>(battle.accounts, token, (int)MsgID.NotifyMove, notify);
        }
    }

    private void OnReqFire(UserToken token, SocketModel model)
    {
        ReqFire req = SerializeUtil.Deserialize<ReqFire>(model.message);

        AccountData acc = CacheManager.instance.GetAccount(token.accountid);
        if (acc == null) return;
        Battle battle = CacheManager.instance.GetBattle(acc.battleid, acc.battleType);
        Tank t = battle.GetTank(token.accountid);

        if (t != null && t.hp > 0)
        {
            //校验两次攻击间隔
            if (Time.time * 0.001f - t.lastFireTime < 1) return;

            t.lastFireTime = Time.time * 0.001f;

            NotifyFire notify = new NotifyFire();
            notify.id = token.accountid;
            notify.pos = req.pos;
            notify.rot = req.rot;

            MsgSender.SendOther<NotifyFire>(battle.accounts, token, (int)MsgID.NotifyFire, notify);
        }
    }
    private void OnReqHit(UserToken token, SocketModel model)
    {
        ReqHit req = SerializeUtil.Deserialize<ReqHit>(model.message);
        if (req.bulletPos == null) return;

        Random r = new Random();
        var atk = r.Next(10, 20);

        AccountData acc = CacheManager.instance.GetAccount(token.accountid);
        AccountData enemy = CacheManager.instance.GetAccount(req.enemy);
        if (acc.battleType == EBattle.Arena && acc.team == enemy.team) return;

        Battle battle = CacheManager.instance.GetBattle(acc.battleid, acc.battleType);
        if (battle == null) return;

        Tank t = battle.GetTank(req.enemy);
        Tank attacker = battle.GetTank(acc.account);
        if (t != null && t.hp > 0 && attacker != null && attacker.hp > 0)
        {
            Console.WriteLine(Vector3.Distance(Tools.ToLunaVec3(req.bulletPos), t.pos));
            //校验子弹和敌人的坐标
            if (Vector3.Distance(Tools.ToLunaVec3(req.bulletPos), t.pos) > 2.5f) return;
            // Console.WriteLine(Time.time * 0.001f - attacker.lastHitTime);
            ////校验两次命中时间
            // if (Time.time * 0.001f - attacker.lastHitTime < 0.7f) return;

            attacker.lastHitTime = Time.time * 0.001f;

            t.hp -= atk;
            //伤害统计
            acc.hurt += atk;

            NotifyHit notify = new NotifyHit();
            notify.id = req.enemy;

            notify.damage = atk;

            MsgSender.SendAll<NotifyHit>(battle.accounts, (int)MsgID.NotifyHit, notify);
            //死亡
            if (t.hp <= 0)
            {
                t.hp = 0;
                acc.killCount++;
                enemy.deathCount++;

                //通知所有人有个坦克死了
                NotifyDeath not = new NotifyDeath();
                not.id = req.enemy;

                MsgSender.SendAll<NotifyDeath>(battle.accounts, (int)MsgID.NotifyDeath, not);
                if (battle.type == EBattle.Survival)
                {
                    SurvivalTankDeath(acc, enemy, battle);
                }
                else
                {
                    ArenaTankDeath(acc, enemy, battle);
                }
            }
        }
    }

    void ArenaTankDeath(AccountData acc, AccountData enemy, Battle battle)
    {
        Arena arena = battle as Arena;

        if (acc.team == ETeam.Blue)
        {
            arena.blueKillCount++;
        }
        else
        {
            arena.redKillCount++;
        }
        if (arena.blueKillCount >= 5 || arena.redKillCount >= 5)
        {
            //通知战斗结束
            NotifyArenaEnd notify = new NotifyArenaEnd();
            notify.blueKillCount = arena.blueKillCount;
            notify.redKillCount = arena.redKillCount;

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

            for (int i = 0; i < arena.accounts.Count; i++)
            {
                AccountData accData = arena.accounts[i];
                TankKillData d = new TankKillData();
                d.account = accData.account;
                d.deathCount = accData.deathCount;
                d.hurt = accData.hurt;
                d.kill = accData.killCount;
                d.team = (int)acc.team;

                notify.datas.Add(d);

                accData.killCount = 0;
                accData.team = ETeam.None;
                accData.hurt = 0;
                accData.deathCount = 0;
            }
            MsgSender.SendAll<NotifyArenaEnd>(arena.accounts, (int)MsgID.NotifyArenaEnd, notify);

            arena.timers.Clear();

            CacheManager.instance.arenas.Remove(arena);
        }
        else
        {
            Timer timer = new Timer(3f, () =>
                     {
                         Tank t = arena.GetTank(enemy.account);
                         if (t == null) return;
                         t.hp = 100;
                       //真随机
                       Random r = new Random(System.Guid.NewGuid().GetHashCode());
                         t.pos = SpawnPoint.point[r.Next(0, 5)];

                         NotifyReborn notify = new NotifyReborn();
                         notify.tank = new TankDTO();
                         notify.tank.id = t.uid;
                         notify.tank.hp = t.hp;
                         notify.tank.pos = Tools.ToVec_3(t.pos);
                         notify.tank.color = Tools.UC2TC(t.color);
                         notify.tank.team = (int)t.team;
                         notify.tank.nickName = t.nickName;

                         MsgSender.SendAll<NotifyReborn>(arena.accounts, (int)MsgID.NotifyReborn, notify);
                     });

            arena.timers.Add(timer);
        }
    }

    void SurvivalTankDeath(AccountData acc, AccountData enemy, Battle battle)
    {
        enemy.order = battle.GetALLTanks().Count;

        battle.RemoveTank(enemy.account);
        //发给单个人的战斗数据
        NotifyBattleEnd notifyEnd = new NotifyBattleEnd();

        TankKillData kill = new TankKillData();
        kill.account = enemy.account;
        kill.kill = enemy.killCount;
        kill.deathCount = enemy.deathCount;
        kill.hurt = enemy.hurt;
        kill.order = enemy.order;
        notifyEnd.data = kill;

        NetworkManager.Send<NotifyBattleEnd>(enemy.token, (int)MsgID.NotifyBattleEnd, notifyEnd);

        enemy.killCount = 0;
        enemy.deathCount = 0;
        enemy.hurt = 0;
        enemy.order = -1;

        //游戏结束
        if (battle.GetALLTanks().Count == 1)
        {
            NotifyBattleEnd notifyEnd2 = new NotifyBattleEnd();


            TankKillData kill2 = new TankKillData();
            kill2.account = acc.account;
            kill2.kill = acc.killCount;
            kill2.deathCount = acc.deathCount;
            kill2.hurt = acc.hurt;
            kill2.order = 1;
            notifyEnd2.data = kill2;

            NetworkManager.Send<NotifyBattleEnd>(acc.token, (int)MsgID.NotifyBattleEnd, notifyEnd2);

            enemy.killCount = 0;
            enemy.deathCount = 0;
            enemy.hurt = 0;
            enemy.order = -1;
        }
    }
}

