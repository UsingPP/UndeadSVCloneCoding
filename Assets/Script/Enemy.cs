using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public float enemyMoveSpeed;
    public float health;
    public float maxHealth;
    WaitForFixedUpdate wait;

    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;

    bool isLive;

    Rigidbody2D enemyRigidbody;
    Collider2D enemyColider;
    SpriteRenderer enemySpriteRenderer;
    Animator enemyAnimator;

    private void Awake()
    {
        wait = new WaitForFixedUpdate();
        enemyRigidbody = GetComponent<Rigidbody2D>();
        enemySpriteRenderer = GetComponent<SpriteRenderer>();
        enemyAnimator = GetComponent<Animator>();
        enemyColider = GetComponent<Collider2D>();
    }




    private void OnEnable()
    {
        target = Gamemanager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        health = maxHealth;

        isLive = true;
        enemyColider.enabled = true;
        enemyRigidbody.simulated = true;
        enemyAnimator.SetBool("Dead", false);
        enemySpriteRenderer.sortingOrder = 2;

    }




    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!Gamemanager.instance.isLive)
            return;

        if (!isLive || enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;

        Vector2 dirVec = target.position - enemyRigidbody.position;
        Vector2 nextVec = dirVec.normalized * enemyMoveSpeed * Time.fixedDeltaTime;

        enemyRigidbody.MovePosition(enemyRigidbody.position + nextVec);
        enemyRigidbody.velocity = Vector2.zero;
        
    }




    private void LateUpdate()
    {
        if (!Gamemanager.instance.isLive)
            return;

        enemySpriteRenderer.flipX = target.position.x < enemyRigidbody.position.x;
    }


    public void Init(SpawnData data)
    {
        enemyAnimator.runtimeAnimatorController = animCon[data.spriteType];
        enemyMoveSpeed = data.movemetSpeed;
        maxHealth = data.health;
        health = data.health;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") || !isLive)
            return;

        health -= collision.GetComponent<Bullet>().damage;
        StartCoroutine(KnockBack());

        if (health > 0)
        {
            enemyAnimator.SetTrigger("Hit");
        }

        else
        {
            //Die
            isLive = false;
            enemyColider.enabled = false;
            enemyRigidbody.simulated = false;
            enemyAnimator.SetBool("Dead", true);
            enemySpriteRenderer.sortingOrder = 1;

            Gamemanager.instance.kill++;
            Gamemanager.instance.GetExp();
        }
    }

    IEnumerator KnockBack()
    {
        yield return wait;
        Vector3 playerPos = Gamemanager.instance.player.transform.position;
        Vector3 dir = transform.position - playerPos;
        enemyRigidbody.AddForce(dir.normalized * 3f, ForceMode2D.Impulse);
    }

    void Dead()
    {
        gameObject.SetActive(false);
        isLive = false;
    }
}

