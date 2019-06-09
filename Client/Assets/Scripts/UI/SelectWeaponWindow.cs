using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TankMsg;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

class SelectWeaponWindow : BaseWindow
{
    Button btnReturn;

    RawImage tankRawImage;
    Image imgEquipBullet;
    Text textAttr;
    Text tankName;
    Text bulletName;

    Button btnTank;
    Button btnBullet;

    Transform tankScrolView;
    Transform bulletScrolView;

    Transform tankContent;
    Transform bulletContent;
    public void Init()
    {
        List<ItemDTO> items = CacheManager.instance.items;
        EquipDTO equip = CacheManager.instance.equip;

        btnReturn = _transform.Find("Top/BtnReturn").GetComponent<Button>();
        btnReturn.onClick.AddListener(OnBtnReturn);

        tankRawImage = _transform.Find("Left/TankRawImage").GetComponent<RawImage>();
        imgEquipBullet = _transform.Find("Left/EquipBullet").GetComponent<Image>();
        textAttr = _transform.Find("Left/Attr").GetComponent<Text>();

        //设置坦克名
        tankName = _transform.Find("Left/TankName").GetComponent<Text>();
        ItemCfg tankCfg = ConfigManager.instance.items[equip.tank];
        tankName.text = tankCfg.Name;

        //设置子弹名
        bulletName = _transform.Find("Left/BulletName").GetComponent<Text>();
        ItemCfg bulletCfg = ConfigManager.instance.items[equip.bullet];
        bulletName.text = bulletCfg.Name;

        btnTank = _transform.Find("Right/BtnTank").GetComponent<Button>();
        btnTank.onClick.AddListener(OnBtnTank);


        btnBullet = _transform.Find("Right/BtnBullet").GetComponent<Button>();
        btnBullet.onClick.AddListener(OnBtnBullet);

        tankScrolView = _transform.Find("Right/TankScrollView");
        bulletScrolView = _transform.Find("Right/BulletScrollView");
        Button btnTankItem = _transform.Find("Right/TankScrollView/Viewport/BtnTank").GetComponent<Button>();
         tankContent = _transform.Find("Right/TankScrollView/Viewport/Content");

        Button btnBulletItem = _transform.Find("Right/BulletScrollView/Viewport/BtnBullet").GetComponent<Button>();
        bulletContent = _transform.Find("Right/BulletScrollView/Viewport/Content");

        for (int i = 0; i < items.Count; i++)
        {
            ItemDTO dto = items[i];
            ItemCfg cfg = ConfigManager.instance.items[dto.itemid];

            Transform child = null;
            if (cfg.Type == 1)
            {
                child = (GameObject.Instantiate(btnTankItem.gameObject) as GameObject).transform;
                child.SetParent(tankContent);
                child.name = cfg.Id.ToString();

                UIEventListener.Get(child.gameObject).onPointerClick = OnEquipTank;
            }
            else if (cfg.Type == 2)
            {
                child = (GameObject.Instantiate(btnBulletItem.gameObject) as GameObject).transform;
                child.SetParent(bulletContent);
                child.name = cfg.Id.ToString();

                UIEventListener.Get(child.gameObject).onPointerClick = OnEquipBullet;
            }
            child.localPosition = Vector3.zero;
            child.localScale = Vector3.one;
            child.gameObject.SetActive(true);
            child.Find("Text").GetComponent<Text>().text = cfg.Name;
            child.name = cfg.Id.ToString();

           
        }
    }

    void OnEquipTank(PointerEventData ped)
    {
        ReqUseTank req = new ReqUseTank();
        req.itemid = int.Parse(ped.selectedObject.name);

        NetClient.instance.Send((int)MsgID.ReqUseTank, req);    
    }

    void OnEquipBullet(PointerEventData ped)
    {
        ReqUseBullet req = new ReqUseBullet();
        req.itemid = int.Parse(ped.selectedObject.name);

        NetClient.instance.Send((int)MsgID.ReqUseBullet, req);
    }
    public void UpdataTank(int itemid)
    {
        ItemCfg cfg = ConfigManager.instance.items[itemid];
        tankName.text = cfg.Name;

        Transform tank=null;
        for (int i = 0; i < tankContent.childCount; i++)
        {
            Transform child = tankContent.GetChild(i);
            if (int.Parse(child.name)==itemid)
            {
                tank = child;
            }
        }

        ItemCfg equipCfg = ConfigManager.instance.items[CacheManager.instance.equip.tank];
        tank.name = equipCfg.Id.ToString();
        tank.Find("Text").GetComponent<Text>().text = equipCfg.Name;

        CacheManager.instance.equip.tank = itemid;
    }

    public void UpdataBullet(int itemid)
    {
        ItemCfg cfg = ConfigManager.instance.items[itemid];
        bulletName.text = cfg.Name;

        Transform bullet = null;
        for (int i = 0; i < bulletContent.childCount; i++)
        {
            Transform child = bulletContent.GetChild(i);
            if (int.Parse(child.name) == itemid)
            {
                bullet = child;
            }
        }

        ItemCfg equipCfg = ConfigManager.instance.items[CacheManager.instance.equip.bullet];
        bullet.name = equipCfg.Id.ToString();
        bullet.Find("Text").GetComponent<Text>().text = equipCfg.Name;

        CacheManager.instance.equip.bullet= itemid;
    }
    private void OnBtnBullet()
    {
        tankScrolView.gameObject.SetActive(false);
        bulletScrolView.gameObject.SetActive(true);
    }

    private void OnBtnTank()
    {
        tankScrolView.gameObject.SetActive(true);
        bulletScrolView.gameObject.SetActive(false);
    }

    private void OnBtnReturn()
    {
        UIManager.instance.Close<SelectWeaponWindow>();
    }
}

