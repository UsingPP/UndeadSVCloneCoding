using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;
    [Header("# Game Controll")]
    public bool isLive;
    public float gameTime;
    public float maxGameTime = 2 * 10f;

    [Header("# Player Infomation")]
    public int health;
    public int maxHealth;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };
    


    [Header("# Game Object")]
    public PoolManager pool;
    public PlayerController player;
    public LevelUp uiLevelUp;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        health = maxHealth;
        uiLevelUp.Select(0);
    }

    private void Update()
    {
        if (!isLive)
            return;
        gameTime += Time.deltaTime;
        if ( gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }

    public void GetExp()
    {
        exp++;

        if (exp == nextExp[Mathf.Min(level, nextExp.Length-1)])
        {
            level++;
            exp = 0;
            //.....
            uiLevelUp.Show();
        }
    }

    public void Stop() {
        isLive = false;
        Time.timeScale = 0;
    }

    public void Resume() {
        isLive = true;
        Time.timeScale = 1;
    }
}
