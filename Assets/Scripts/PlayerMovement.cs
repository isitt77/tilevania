using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 6f;
    [SerializeField] Vector2 deathKick = new Vector2(10f, 10f);

    float normalGravity;
    float ladderGravity = 0f;

    bool isAlive = true;

    Vector2 moveInput;
    Rigidbody2D rb2d;
    Animator animator;
    CapsuleCollider2D capCollider2d;
    BoxCollider2D feetCollider2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capCollider2d = GetComponent<CapsuleCollider2D>();
        feetCollider2d = GetComponent<BoxCollider2D>();
        normalGravity = rb2d.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) { return; }
        {
            Run();
            FlipSprite();
            ClimbLadder();
            PlayerDie();
        }
    }


    void OnMove(InputValue value)
    {
        if (!isAlive) { return; }
        moveInput = value.Get<Vector2>();
        //Debug.Log(moveInput);
    }

    void OnJump(InputValue value)
    {
        //if (!capCollider2d.IsTouchingLayers(LayerMask.GetMask("Ground")))
        //{
        //    return;
        //}

        if (!isAlive) { return; }
        bool onGround = feetCollider2d.IsTouchingLayers(LayerMask.GetMask("Ground"));

        if(onGround && value.isPressed)
        {
            rb2d.velocity += new Vector2(rb2d.velocity.x, jumpSpeed);
        }
    }


    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, rb2d.velocity.y);
        rb2d.velocity = playerVelocity;

        // Player must have movevment (abs value) greater than 0 (Epsilon) to initiate running animation.
        bool hasVelocityX = Mathf.Abs(rb2d.velocity.x) > Mathf.Epsilon;
        animator.SetBool("isRunning", hasVelocityX);
    }

    void FlipSprite()
    {
        // Player must have movevment (abs value) greater than 0 (Epsilon) to cause sprite flip.
        bool hasVelocityX = Mathf.Abs(rb2d.velocity.x) > Mathf.Epsilon;

        if (hasVelocityX)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb2d.velocity.x), 1f);
        }
    }

    void ClimbLadder()
    {
        bool onLadder = capCollider2d.IsTouchingLayers(LayerMask.GetMask("Climbing"));
        bool verticalMovement = Mathf.Abs(rb2d.velocity.y) > Mathf.Epsilon;

        if (onLadder)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, moveInput.y * climbSpeed);
            rb2d.gravityScale = ladderGravity;
            animator.SetBool("isClimbing", verticalMovement);
        }
        else
        {
            rb2d.gravityScale = normalGravity;
            animator.SetBool("isClimbing", false);
        }
    }

    void PlayerDie()
    {
        if (rb2d.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazard")))
        {
            isAlive = false;
            animator.SetTrigger("Die");
            rb2d.velocity = deathKick;
        }
    }
}
