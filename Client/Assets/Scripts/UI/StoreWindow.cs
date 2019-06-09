using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TankMsg;
public class StoreWindow : BaseWindow
{
    Button btnReturn;

    public void Init()
    {
        btnReturn = _transform.Find("BtnReturn").GetComponent<Button>();
        btnReturn.onClick.AddListener(OnReturn);

        Transform content = _transform.Find("Scroll View/Viewport/Content");
        Transform item = _transform.Find("Scroll View/Viewport/Item");
        Dictionary<int, ItemCfg> items = ConfigManager.instance.items;
        foreach (var cfg in items.Values)
        {
            Transform child = (GameObject.Instantiate(item.gameObject) as GameObject).transform;
            child.SetParent(content);
            child.localScale = Vector3.one;
            child.localPosition = Vector3.zero;
            child.gameObject.SetActive(true);
            child.name = cfg.Id.ToString();

            UIEventListener.Get(child.gameObject).onPointerClick = OnBuy;

            child.Find("Id").GetComponent<Text>().text = cfg.Name;
        }
    }

    private void OnBuy(PointerEventData ped)
    {
        int itemid = int.Parse(ped.selectedObject.name);
        ReqBuy req = new ReqBuy();
        req.itemid = itemid;

        NetClient.instance.Send<ReqBuy>((int)MsgID.ReqBuy, req);
    }

    private void OnReturn()
    {
        UIManager.instance.Close<StoreWindow>();
    }
}
