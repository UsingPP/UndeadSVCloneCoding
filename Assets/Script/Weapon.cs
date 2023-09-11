using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    private float timer;

    PlayerController player;
    private void Awake()
    {
        player = Gamemanager.instance.player;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Gamemanager.instance.isLive)
            return;

        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            default:
                timer += Time.deltaTime;
                if (timer > speed)
                {
                    timer = 0f;
                    Fire();
                }
                break;
        }
    }


    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if (id == 0)
        {
            Batch();
        }

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }


    public void Init(ItemData data)
    {
        //hand Set
        Hand hand = player.playerHands[(int)data.itemType];
        hand.spriter.sprite = data.hand;
        Debug.Log(data.itemType);
        hand.gameObject.SetActive(true);

        // basic set
        name = "Weapon" + data.itemId;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;

        for (int idx = 0; idx < Gamemanager.instance.pool.prefebs.Length; idx++)
        {
            if (data.projectile == Gamemanager.instance.pool.prefebs[idx])
            {
                prefabId = idx;
                break;
            }
        }
        //property set
        id = data.itemId;
        damage = data.baseDamage;
        count = data.baseCount;

        switch (id)
        {
            case 0:
                speed = 150;
                Batch();
                break;
            default:
                speed = 0.3f;
                break;
               
        }


        

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    void Batch()
    {
        for (int index = 0; index < count; index++)
        {
            Transform bullet;
            if (index < transform.childCount)
            {
                bullet = transform.GetChild(index);
            }
            else
            {
                bullet = Gamemanager.instance.pool.Get(prefabId).transform;
                bullet.parent = transform;
            }
            

            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotVec);
            bullet.Translate(Vector3.up * 1.5f, Space.Self);

            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero); // -1 is Infinity Per
        }
    }

    void Fire()
    {
        if (!player.playerScanner.nearestTarget)
            return;
        Vector3 targetPos = player.playerScanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        Transform bullet = Gamemanager.instance.pool.Get(prefabId).transform;
        bullet.position = transform.position;

        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage, count, dir);
    }
}
