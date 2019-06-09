using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


class TankManager:Singleton<TankManager>
{
     List<BaseTank> tanks = new List<BaseTank>();

    public void AddTank(BaseTank t)
    {
        tanks.Add(t);
    }

    public void RemoveTank(BaseTank t)
    {
        tanks.Remove(t);
    }

    public void ClearAll()
    {
        for (int i = 0; i < tanks.Count; i++)
        {
            tanks[i].Clear();
        }
        tanks.Clear();
    }
    public BaseTank GetTank(string id)
    {
        for (int i = 0; i < tanks.Count; i++)
        {
            if (tanks[i].tankDTO.id==id)
            {
                return tanks[i];
            }
        }
        return null;
    }
    public void Update(float dt)
    {
        for (int i = 0; i < tanks.Count; i++)
        {
            tanks[i].Update(dt);
        }
    }
}

