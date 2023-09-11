using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;
    Rigidbody2D bulletRigdbody;

    private void Awake()
    {
        bulletRigdbody = GetComponent<Rigidbody2D>();
    }

    public void Init(float damage, int per, Vector3 dir)
    {
        this.damage = damage;
        this.per = per;

        if (per > -1)
        {
            bulletRigdbody.velocity = dir * 15f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || per == -1 )
            return;
        // 근접무기는 실행시키지 않음

        per--;
        if (per == -1)
        {
            bulletRigdbody.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }
}
