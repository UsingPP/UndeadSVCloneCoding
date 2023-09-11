using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    public Vector2 inputVec;
    public float movementSpeed = 10f;
    //public float runAniSpeed = 16f;
    private bool isRun = false;
    public Scanner playerScanner;
    public Hand[] playerHands;


    private Rigidbody2D playerRigidbody;
    private SpriteRenderer playerRenderer;
    private Animator playerAnimator;

    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerRenderer = GetComponent<SpriteRenderer>();
        playerScanner = GetComponent<Scanner>();
        playerHands = GetComponentsInChildren<Hand>(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Gamemanager.instance.isLive)
            return;
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
        playerAnimator.SetBool("Run", isRun);
        
    }

    private void FixedUpdate()
    {
        if (!Gamemanager.instance.isLive)
            return;
        Vector2 nextVec = inputVec.normalized * movementSpeed * Time.fixedDeltaTime;
        playerRigidbody.MovePosition( playerRigidbody.position + nextVec);
    }

    private void LateUpdate()
    {
        if (!Gamemanager.instance.isLive)
            return;

        if (inputVec.x != 0) {
            isRun = true;
            if (inputVec.x < 0) {
                playerRenderer.flipX = true;
            }
            else if (inputVec.x > 0) {
                playerRenderer.flipX = false;
            }
        }

        else if (inputVec.y != 0)
        {
            isRun = true;
        }

        else
        {
            isRun = false;
        }

    }


    public void Die() {
        playerAnimator.SetTrigger("Dead");
            
    }
}
