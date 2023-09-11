using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;

    public int level;
    float timer;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Gamemanager.instance.isLive)
            return;
        timer += Time.deltaTime;
        level = Mathf.FloorToInt(Gamemanager.instance.gameTime / 10f);
        if (timer > spawnData[level].spawnTime)
        {
            
            timer = 0f;
            Spwan();
            
        }
    }

    void Spwan() {
        GameObject enemy = Gamemanager.instance.pool.Get(0);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }
}


[System.Serializable]
public class SpawnData
{
    public int spriteType;
    public float spawnTime;
    public int health;
    public float movemetSpeed;
}