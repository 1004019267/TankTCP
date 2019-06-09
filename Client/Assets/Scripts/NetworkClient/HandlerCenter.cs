using System;
using System.Collections.Generic;

using UnityEngine;

public class HandlerCenter
{
    private Dictionary<MsgID, Action<SocketModel>> _handlers = new Dictionary<MsgID, Action<SocketModel>>();

    AccountHandler accountHandler = new AccountHandler();
    BattleHandler battleHandler = new BattleHandler();
    RoomHandler roomHandler = new RoomHandler();
    EquipHandler equipHandler = new EquipHandler();
    MallHandler mallHandler = new MallHandler();
    public void Initialize()
    {
        accountHandler.RegisterMsg(_handlers);
        battleHandler.RegisterMsg(_handlers);
        roomHandler.RegisterMsg(_handlers);
        equipHandler.RegisterMsg(_handlers);
        mallHandler.RegisterMsg(_handlers);
    }

    public void MessageReceive(SocketModel model)
    {
        if (_handlers.ContainsKey((MsgID)model.command))
        {
            Debug.Log((MsgID)model.command);
            Action<SocketModel> handler = _handlers[(MsgID)model.command];
            handler(model);
        }
        else
        {
            Debug.Log(model.command);
        }
    }
}