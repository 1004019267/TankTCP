using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager : Singleton<ConfigManager>
{
    ConfigParser parser = new ConfigParser();

   public Dictionary<int, ItemCfg> items = new Dictionary<int, ItemCfg>();

    public void Init()
    {
        items = parser.LoadConfig<ItemCfg>("Item");
    }
}
