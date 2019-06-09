using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankMsg;

class HandlerCenter : IHandlerCenter
{
    Dictionary<MsgID, Action<UserToken, SocketModel>> _handlers = new Dictionary<MsgID, Action<UserToken, SocketModel>>();

    AccountHandler accountHandler = new AccountHandler();

    BattleHandler battleHandler = new BattleHandler();

    RoomHandler roomHandler = new RoomHandler();

    EquipHandler equipHandler = new EquipHandler();

    MallHandler mallHandler = new MallHandler();

    int accountid = 1001;
    //初始化
    public void Initialize()
    {
        accountHandler.RegisterMsg(_handlers);
        battleHandler.RegisterMsg(_handlers);
        roomHandler.RegisterMsg(_handlers);
        equipHandler.RegisterMsg(_handlers);
        mallHandler.RegisterMsg(_handlers);
    }
    /// <summary>
    /// 客户端连接到服务器
    /// </summary>
    /// <param name="token"></param>
    public void ClientConnect(UserToken token)
    {
        Console.WriteLine(string.Format(token.address.ToString() + "Connect"));

        //token.accountid = accountid++;     
    }
    /// <summary>
    /// 客户端断开连接
    /// </summary>
    /// <param name="token"></param>
    /// <param name="error"></param>
    public void ClientClose(UserToken token, string error)
    {
        CacheManager.instance.offlineTokens.Enqueue(token);     
    }

    /// <summary>
    /// 服务器在收到客户端的消息之后要执行的方法
    /// </summary>
    /// <param name="token"></param>
    /// <param name="message"></param>
    public void MessageReceive(UserToken token, object message)
    {
        SocketModel model = message as SocketModel;

        //Console.WriteLine(token.accountid+","+(MsgID)model.command);
        if (_handlers.ContainsKey((MsgID)model.command))
        {
            Action<UserToken, SocketModel> handler = _handlers[(MsgID)model.command];
            handler(token, model);
        }
    }
}

