using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public struct Vector3
{
    public float x;
    public float y;
    public float z;
}
public struct Color
{
    public float r;
    public float g;
    public float b;
}

public class Tank
{
    public int uid;
    public int hp;
    public Color color;
    public Vector3 pos;
    public Vector3 rot;
}

class CacheManager : Singleton<CacheManager>
{
    //保存已连接的客户端
    List<UserToken> tokens = new List<UserToken>();

    List<Tank> tanks = new List<Tank>();

    public void AddToken(UserToken token)
    {
        tokens.Add(token);
    }

    public void RemoveToken(UserToken token)
    {
        tokens.Remove(token);
    }

    public UserToken GetToken(int id)
    {
        for (int i = 0; i < tokens.Count; i++)
        {
            if (tokens[i].accountid==id)
            {
                return tokens[i];
            }
        }
        return null;
    }

    public List<UserToken>GetAllTokens()
    {
        return tokens;
    }

    public void AddTank(Tank t)
    {
        tanks.Add(t);
    }

    public void RemoveTank(int account)
    {
        tanks.Remove(GetTank(account));
    }
    public Tank GetTank(int accountid)
    {
        for (int i = 0; i < tanks.Count; i++)
        {
            if (tanks[i].uid==accountid)
            {
                return tanks[i];
            }
        }
        return null;
    }
    public List<Tank>GetALLTanks()
    {
        return tanks;
    }
}
