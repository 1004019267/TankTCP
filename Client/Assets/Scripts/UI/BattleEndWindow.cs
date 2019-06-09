using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TankMsg;
class BattleEndWindow:BaseWindow
{
    Button btnWitnessBattle;
    Button btnExit;

    Text textKill;
    Text textDeath;
    Text textHurt;
    Text textOrder;

    Transform textOne;
    Transform textTwo;
    public void Init(TankKillData data)
    {       
        textOne = _transform.Find("One");
        textTwo = _transform.Find("Two");
        if (data.order==1)
        {          
            textOne.gameObject.SetActive(true);          
            textTwo.gameObject.SetActive(false);
        }
        btnExit = _transform.Find("BtnExit").GetComponent<Button>();
        btnExit.onClick.AddListener(OnBtnExit);

        textKill = _transform.Find("TextKill").GetComponent<Text>();
        textKill.text = data.kill.ToString();

        textDeath = _transform.Find("TextDeath").GetComponent<Text>();
        textDeath.text = data.deathCount.ToString();

        textHurt = _transform.Find("TextHurt").GetComponent<Text>();
        textHurt.text = data.hurt.ToString();

        textOrder = _transform.Find("TextOrder").GetComponent<Text>();
        textOrder.text = data.order.ToString();
    }

    void OnBtnExit()
    {
        TankManager.instance.ClearAll();
        UIManager.instance.CloseAll();
        UIManager.instance.Open<MainWindow>().Init();
    }
}

