using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Tools
{
    public static Vector3 ToVec3(TankMsg.Vector_3 vec)
    {
        return new Vector3()
        {
            x=vec.x,
            y=vec.y,
            z=vec.z,
        };
    }
    public static TankMsg.Vector_3 ToVec_3(Vector3 vec)
    {
        return new TankMsg.Vector_3()
        {
            x = vec.x,
            y = vec.y,
            z = vec.z,
        };
    }
    public static Color TC2UC(TankMsg.Color c)
    {
       Color result = new Color
        {
            r = c.r,
            g = c.g,
            b = c.b,
        };
        return result;
    }

    public static TankMsg.Color UC2TC(Color c)
    {
        TankMsg.Color result = new TankMsg.Color
        {
            r = c.r,
            g = c.g,
            b = c.b,
        };
        return result;
    }
}

