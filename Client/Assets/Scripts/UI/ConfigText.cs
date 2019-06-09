using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ConfigText : MonoBehaviour {
 
	// Use this for initialization
	void Start () {
        Dictionary<int, ItemCfg> items = ConfigManager.instance.items;

        foreach (var item in items.Values)
        {
            Debug.Log(item.Id+"."+item.Type+"."+item.Name+"."+item.Attribute+"."+item.AttriType);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
