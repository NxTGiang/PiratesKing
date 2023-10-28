using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField]private float jumpForce = 14f;
    [SerializeField]private float moveSpeed =7f;

    private float horizontalInput;


    private enum MovementState { idle, running, jumping, falling}

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * moveSpeed, body.velocity.y);

        if(horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < 0.01f)
            transform.localScale = new Vector3(-1, 1, 1);


        if (Input.GetKey(KeyCode.Space) && IsGrounded())
            Jump();

        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", IsGrounded());
    }

    private void Jump()
    {
        
        body.velocity = new Vector2(body.velocity.x, jumpForce);
        anim.SetTrigger("jump");
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }


    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size,0 , Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
        
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && IsGrounded();
    }
}
