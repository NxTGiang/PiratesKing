using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Parameters")]
    [SerializeField] private float moveSpeed = 7f; 
    [SerializeField] private float jumpForce = 18f;

    [Header("Coyote Time")]
    [SerializeField] private float coyoteTime;
    private float coyoteCounter;

    [Header("Multiple Jump")]
    [SerializeField]private int extraJump;
    private int jumpCounter;

    [Header("Layers")]
    [SerializeField] private LayerMask groundLayer;

    [Header("Effect")]
    [SerializeField] private float speedBoostDuration;
    [SerializeField] private float speedBoosts;

    [Header("Item")]
    [SerializeField] private float cooldownTimer1;
    [SerializeField] private float cooldownTimer2;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private Health health;
    private float speedBoostTimer;
    private float healthTimer;
    private float critBoostTimer;
    private bool isSpeedBoostActive = false;
    private ItemCollector itemCollector;
    private PlayerAttack playerAttack;
    private bool healthAble = false;
    private bool critBoostAble = false;




    private float horizontalInput;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        health = GetComponent<Health>();
        itemCollector = GetComponent<ItemCollector>();
        playerAttack = GetComponent<PlayerAttack>();
    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        var currentVelocity = body.velocity.y;
        

        //flip 
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);


        //set parameter
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", IsGrounded());
        if (IsGrounded()) currentVelocity = 0f;
        if (currentVelocity < 0f)
            anim.SetBool("falling", true);
        else
            anim.SetBool("falling", false);


        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
        if (Input.GetKeyDown(KeyCode.Alpha1) && !healthAble && itemCollector.getNumOfHealth()>0)
        {
            itemCollector.showNumberOfItem(1, healthTimer);
            UseHealthItem();
            healthAble = true;
            healthTimer = 5f;
        }
        if (healthAble)
        {
            healthTimer -= Time.deltaTime;
            if (healthTimer <= 0)
            {
                healthAble = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && !isSpeedBoostActive && itemCollector.getNumOfSpeed() > 0)
        {
            itemCollector.showNumberOfItem(2, speedBoostTimer);
            moveSpeed += speedBoosts;
            isSpeedBoostActive = true;
            speedBoostTimer = speedBoostDuration;
        }
        if (isSpeedBoostActive)
        {
            speedBoostTimer -= Time.deltaTime;
            if (speedBoostTimer <= 0)
            {
                moveSpeed -= speedBoosts;
                isSpeedBoostActive = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && !critBoostAble && itemCollector.getNumOfStrenth() > 0)
        {
            itemCollector.showNumberOfItem(3, critBoostTimer);
            playerAttack.addDamage(75);
            Debug.Log("asdad");
            critBoostAble = true;
            critBoostTimer = cooldownTimer2;
        }
        if (critBoostAble)
        {
            critBoostTimer -= Time.deltaTime;
            if (critBoostTimer <= 0)
            {
                Debug.Log("asdad2");
                playerAttack.minusDamage(75);
                critBoostAble = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) && body.velocity.y > 0)
        {
            body.velocity = new Vector2(body.velocity.x, body.velocity.y/2);
        }

        body.velocity = new Vector2(horizontalInput * moveSpeed, body.velocity.y);

        if (IsGrounded())
        {
            coyoteCounter = coyoteTime;
            jumpCounter = extraJump;
        }
        else
        {
            coyoteCounter -= Time.deltaTime;
        }
    }

    private void Jump()
    {
        if (coyoteCounter <= 0 && jumpCounter <= 0) return;
        if(IsGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpForce);
            //anim.SetTrigger("jump");
        }
        else
        {
            if(coyoteCounter > 0)
                body.velocity = new Vector2(body.velocity.x, jumpForce);
            else
            {
                if(jumpCounter > 0)
                {
                    body.velocity = new Vector2(body.velocity.x, jumpForce);
                    jumpCounter--;
                }
                    
            }
        }
        coyoteCounter = 0;



    }



    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size,0 , Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
        
    }

    public void UseHealthItem()
    {
        health.AddHealth(200f); 
    }



    //public bool canAttack()
    //{
    //    return horizontalInput == 0 && IsGrounded();
    //}
}
