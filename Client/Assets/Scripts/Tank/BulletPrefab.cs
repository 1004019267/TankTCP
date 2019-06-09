using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TankMsg;
public class BulletPrefab : MonoBehaviour
{

    public Vector3 dir;

    const float speed = 20f;

    //是否已经命中
    bool hit = false;
    void Update()
    {
        if (!hit)
            transform.position += dir * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tank"))
        {
            hit = true;

            ReqHit req = new ReqHit();
            req.enemy = other.gameObject.name;
            req.bulletPos = Tools.ToVec_3(transform.position);
            NetClient.instance.Send<ReqHit>((int)MsgID.ReqHit, req);
        }
        else
        {
            var go = Resources.Load("CompleteShellExplosion");
            var exp = (GameObject.Instantiate(go) as GameObject).transform;
            exp.position = transform.position;
            exp.localScale = Vector3.one;
            Destroy(exp.gameObject, 1.5f);
        }
        Destroy(gameObject);
    }
}
