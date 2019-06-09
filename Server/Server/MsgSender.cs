using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class MsgSender
{
    /// <summary>
    /// 发送给指定所有人
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="account"></param>
    /// <param name="command"></param>
    /// <param name="message"></param>
    public static void SendAll<T>(List<AccountData> account, int command, T message)
    {
        for (int i = 0; i < account.Count; i++)
        {
            NetworkManager.Send<T>(account[i].token, command, message);
        }
    }
    /// <summary>
    /// 发送给指定的其他人
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="account"></param>
    /// <param name="token"></param>
    /// <param name="command"></param>
    /// <param name="message"></param>
    public static void SendOther<T>(List<AccountData> account, UserToken token, int command, T message)
    {
        for (int i = 0; i < account.Count; i++)
        {
            if (account[i].token.accountid != token.accountid)
            {
                NetworkManager.Send<T>(account[i].token, command, message);
            }
        }
    }
}
