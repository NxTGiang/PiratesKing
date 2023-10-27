using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer rbSprite;
    private Animator animator;
    private BoxCollider2D boxCollider2D;
    private float dirX =0f;
    private int countJump = 0;

    [SerializeField] private LayerMask jumpableGround;
    [SerializeField]private float jumpForce = 14f;
    [SerializeField]private float moveSpeed =7f;

    private float horizontalInput;

    private enum MovementState { idle, running, jumping, falling}
     // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rbSprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(0,0,0);
        dirX = Input.GetAxisRaw("Horizontal");  
        rb.velocity = new Vector2 (dirX*moveSpeed, rb.velocity.y);
        if (Input.GetButtonDown("Jump") && countJump < 1)
        {
            countJump++;
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0);
        }
        if (IsGround()) countJump = 0;
        UpdateAnimationState();
        
    }

    private void UpdateAnimationState()
    {
        MovementState state;
        if (dirX > 0f)
        {
            state = MovementState.running;
            rbSprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            rbSprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if(rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if(rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }
        animator.SetInteger("state", (int)state);

    }

    private bool IsGround()
    {
        return Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
        
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && IsGround();
    }
}
