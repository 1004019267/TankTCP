using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherBulletPrefab : MonoBehaviour
{
    public Vector3 dir;

    const float speed = 20f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
