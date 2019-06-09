using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using TankMsg;

public class GameServer
{
    NetworkManager network;

    CacheManager cache;

    HandlerCenter handler;


    public GameServer()
    {
        handler = new HandlerCenter();      

        network = new NetworkManager(10000,handler);
        network.Start(10010);
        cache = new CacheManager();

        while (true)
        {
            System.Threading.Thread.Sleep(1);
        }
    }
}
