using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TankMsg;
using System;

public class ArenaEndWindow : BaseWindow
{
    Transform grid;

    Transform record;

    Button btnReturn;

    Transform textRed;
    Transform textBlue;
    Transform textNone;

    public void Init(List<TankKillData> datas,int winteam)
    {
        grid = _transform.Find("Gird");
        record = _transform.Find("Record");

        textRed = _transform.Find("Red");
        textBlue = _transform.Find("Blue");
        textNone = _transform.Find("None");

        if (winteam==(int)ETeam.Red)
        {
            textRed.gameObject.SetActive(true);
        }
        else if (winteam == (int)ETeam.Blue)
        {
            textBlue.gameObject.SetActive(true);
        }
        else
        {
            textNone.gameObject.SetActive(true);
        }

        for (int i = 0; i < datas.Count; i++)
        {
            TankKillData data = datas[i];

            Transform child = (GameObject.Instantiate(record.gameObject) as GameObject).transform;
            child.SetParent(grid);
            child.localScale = Vector3.one;
            child.localPosition = Vector3.zero;
            child.gameObject.SetActive(true);
            
            //真随机
            System.Random r = new System.Random(System.Guid.NewGuid().GetHashCode());

            Image img = child.GetComponent<Image>();
            img.color = new UnityEngine.Color((float)r.NextDouble(), (float)r.NextDouble(), (float)r.NextDouble());

            child.Find("Acc").GetComponent<Text>().text = data.account.ToString();
            child.Find("Kill").GetComponent<Text>().text = data.kill.ToString();
            child.Find("Death").GetComponent<Text>().text = data.deathCount.ToString();
            child.Find("Hurt").GetComponent<Text>().text = data.hurt
                .ToString();
            ETeam team = (ETeam)data.team;
            child.Find("Team").GetComponent<Text>().text = team.ToString();
        }

        btnReturn = _transform.Find("BtnReturn").GetComponent<Button>();
        btnReturn.onClick.AddListener(OnReturn);
    }

    private void OnReturn()
    {
        UIManager.instance.Close<ArenaEndWindow>();
        UIManager.instance.Open<MainWindow>().Init();
    }
}
