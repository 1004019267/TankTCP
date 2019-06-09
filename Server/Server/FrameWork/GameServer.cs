using System;
using System.Collections.Generic;
using System.Threading;
using TankMsg;
using System.Reflection;
using System.IO;

public class GameServer
{
    // 游戏循环线程
    private Thread _gameloopThread;

    private bool _gameloopRunning = false;

    private const int World_Sleep_Const = 50;

    private Time _timerHelper;

    Thread _dataWriterThread;

    public GameServer()
    {
        _timerHelper = new Time();
        Time.Init();

        // 载入配置文件
        ConfigManager.instance.Init();


        // 连接到数据库
        MysqlManager.instance.Connect();

        // 服务器初始化
        //NetworkManager ss = new NetworkManager(9000, new HandlerCenter());
        //ss.Start(6650);
        HandlerCenter handler = new HandlerCenter();
        NetworkManager network = new NetworkManager(10000, handler);
        network.Start(10010);



        _gameloopRunning = true;
        _gameloopThread = new Thread(Run);
        _gameloopThread.Start();

        _dataWriterThread = new Thread(Write);
        _dataWriterThread.Start();
    }

    void Write()
    {
        while (true)
        {
            Queue<UserToken> offelineTokens = CacheManager.instance.offlineTokens;

            for (int i = 0; i < offelineTokens.Count; i++)
            {
                UserToken token = offelineTokens.Dequeue();

                Console.WriteLine(string.Format(token.accountid + "Disconnet..."));

                AccountData user = CacheManager.instance.GetAccount(token.accountid);
                if (user == null) return;
                //if (string.IsNullOrEmpty(token.accountid)) continue;
                CacheManager.instance.WriteAccountAll(token.accountid);

                //移除坦克
                AccountData acc = CacheManager.instance.GetAccount(token.accountid);
                if (acc == null) return;

                Room room = CacheManager.instance.GetMatch(acc.roomid);
                if (room != null)
                    room.RemoveAccount(acc.account);

                Battle battle = CacheManager.instance.GetBattle(acc.battleid,acc.battleType);
                if (battle != null)
                {
                    battle.RemoveTank(acc.account);
                    battle.RemoveAccount(acc.account);
                }

                //移除账号
                CacheManager.instance.RemoveAccount(token.accountid);

                //移除账号所有物品和装备
                CacheManager.instance.RemoveAccountItem(token.accountid);
                CacheManager.instance.RemoveAccountEquip(token.accountid);

                NotifyOffline notify = new NotifyOffline();
                notify.id = token.accountid;
                MsgSender.SendOther<NotifyOffline>(CacheManager.instance.GetAllAccount(), token, (int)MsgID.NotifyOffline, notify);

                token.accountid = null;
            }
            Thread.Sleep(10);
        }
    }
    private void Tick(float dt)
    {
        //TimerManager.instance.Update(dt);
        CacheManager.instance.Update(dt);
    }


    private void Run()
    {
        // 当前时间
        uint realCurrTime = 0;

        // 上一次更新开始的时间
        uint realPrevTime = Time.time;

        // 上一次更新Sleep的时间
        uint prevSleepTime = 0;

        while (_gameloopRunning)
        {
            // 获取当前时间
            realCurrTime = Time.time;

            // 更新增量时间
            uint diff = Time.GetMSTimeDiff(realPrevTime, realCurrTime);
            Time.deltaTime = diff;

            float framedt = diff / 1000f;

            Tick(framedt);


            realPrevTime = realCurrTime;

            if (diff <= World_Sleep_Const + prevSleepTime)
            {
                prevSleepTime = World_Sleep_Const + prevSleepTime - diff;
                Thread.Sleep((int)prevSleepTime);
            }
            else
            {
                prevSleepTime = 0;
            }
        }
    }
}
