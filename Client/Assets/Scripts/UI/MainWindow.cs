using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TankMsg;
public class MainWindow : BaseWindow
{

    Button btnWareHouse; 

    Button btnStore;

    Button btnArena;
    Button btnSurvival;

    public void Init()
    {
        btnStore = _transform.Find("BtnStore").GetComponent<Button>();
        btnStore.onClick.AddListener(OnBtnStoreClick);

        btnWareHouse = _transform.Find("BtnWareHouse").GetComponent<Button>();
        btnWareHouse.onClick.AddListener(OnBtnWareHouse);

        btnArena = _transform.Find("BtnArena").GetComponent<Button>();
        btnArena.onClick.AddListener(OnBtnArenaClick);

        btnSurvival = _transform.Find("BtnSurvival").GetComponent<Button>();
        btnSurvival.onClick.AddListener(OnBtnSurvivalClick);
    }

    private void OnBtnWareHouse()
    {
        UIManager.instance.Open<SelectWeaponWindow>().Init();
    }

    void OnBtnStoreClick()
    {
        UIManager.instance.Open<StoreWindow>().Init();
    }
    private void OnBtnArenaClick()
    {
        ReqEnterRoom req = new ReqEnterRoom();
        req.battleType = (int)EBattle.Arena;
        req.limitNumber = 4;

        NetClient.instance.Send<ReqEnterRoom>((int)MsgID.ReqEnterRoom, req);

        btnArena.interactable = false;
        btnSurvival.interactable = false;
    }
    private void OnBtnSurvivalClick()
    {
        ReqEnterRoom req = new ReqEnterRoom();
        req.battleType = (int)EBattle.Survival;
        req.limitNumber = 4;

        NetClient.instance.Send<ReqEnterRoom>((int)MsgID.ReqEnterRoom, req);

        btnArena.interactable = false;
        btnSurvival.interactable = false;
    }
}
