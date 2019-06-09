using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TankMsg;

class CacheManager : Singleton<CacheManager>
{
    public string account;
    public string pwd;
    public string nickname;

    public int diamond;

    public int roomid;

    public int battleid;
    public EBattle battleType;
    public int limitNum;

    //所有物品
    public List<ItemDTO> items = new List<ItemDTO>();

    //当前装备
    public EquipDTO equip = new EquipDTO();
}

