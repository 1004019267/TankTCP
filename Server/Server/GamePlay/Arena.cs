using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Arena : Battle
{
    //当前时间
    public float elapsedTime = 0;
    //红队杀人数
    public int redKillCount;

    public int blueKillCount;

    public const float battleTime = 120;

    public List<Timer> timers = new List<Timer>();

    public void Update(float dt)
    {
        for (int i = 0; i < timers.Count; i++)
        {
            Timer timer = timers[i];
            if (timer.end)
            {
                timers.Remove(timer);
            }
            else
            {
                timer.Update(dt);
            }
        }

        elapsedTime += dt;
    }
}

