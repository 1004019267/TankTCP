using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TankMsg;
public class BaseTank
{
    public TankDTO tankDTO;

    public Transform transfrom;

    public Slider slider;

    public Text name;

    public const int maxHp = 100;

    public bool death = false;
    public BaseTank(TankDTO dto)
    {
        tankDTO = dto;

        var go = Resources.Load("CompleteTank");
        transfrom = (GameObject.Instantiate(go) as GameObject).transform;
        transfrom.position = Tools.ToVec3(tankDTO.pos);
        transfrom.localScale = Vector3.one;
        transfrom.name = tankDTO.id.ToString();

        //坦克的颜色
        foreach (var item in transfrom.GetComponentsInChildren<MeshRenderer>())
        {
            item.material.color = Tools.TC2UC(dto.color);
        }

        //坦克血条
        slider = transfrom.Find("Canvas/Slider").GetComponent<Slider>();
        slider.value = (float)tankDTO.hp / maxHp;

        name = transfrom.Find("Canvas/Name").GetComponent<Text>();
        name.text = tankDTO.nickName.ToString();
    }

    public virtual void Move(Vector3 pos, Vector3 rot)
    {
    }

    public virtual void Fire(Vector3 pos, Vector3 rot)
    {

    }

    public void Destory()
    {
        GameObject.Destroy(transfrom.gameObject);
    }
    public virtual void Death()
    {
        death = true;
        GameObject.Destroy(transfrom.gameObject);
        //创建爆炸特效
        var go = Resources.Load("CompleteTankExplosion");
        var exp = (GameObject.Instantiate(go) as GameObject).transform;
        exp.position = transfrom.position;
        exp.localScale = Vector3.one;

        GameObject.Destroy(exp.gameObject, 1.05f);
        //创建废墟
        var go1 = Resources.Load("BustedTank");
        var exp1 = (GameObject.Instantiate(go1) as GameObject).transform;
        exp1.position = transfrom.position;
        exp1.localScale = Vector3.one;

        GameObject.Destroy(exp1.gameObject, 3f);
    }
    public virtual void Update(float dt)
    {
    }

    public virtual void Hurt(int damage)
    {
        var go = Resources.Load("CompleteShellExplosion");
        var exp = (GameObject.Instantiate(go) as GameObject).transform;
        exp.position = transfrom.position;
        exp.localScale = Vector3.one;

        GameObject.Destroy(exp.gameObject, 1.5f);

        tankDTO.hp -= damage;
        slider.value = (float)tankDTO.hp / maxHp;
    }
    public virtual void Clear()
    {
        GameObject.Destroy(transfrom.gameObject);
    }
}

