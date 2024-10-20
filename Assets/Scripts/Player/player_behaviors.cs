using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_behaviors : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;  

    // 参数
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private Transform groundCheck_front;
    [SerializeField] private Transform groundCheck_back;
    [SerializeField] private float groundCheckRadius = 0.1f;
    [SerializeField] private string[] groundTag;

    private float xInput;
    private bool isFacingRight = true;
    private int facingDir = 1;

    // 玩家状态
    private bool onGround;
    private bool canMove = true;

    // 已收集的奶酪个数
    private int collectedCheese = 0; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            HandleMovement();
        }
        else
        {
            xInput = 0;
        }
        HandleAnimation();
    }

    private void FixedUpdate()
    {
        CheckGround();
        CheckStuck();
        if (Mathf.Abs(xInput) > 0)
        {
            TurnCheck();
        }
    }

    private void CheckStuck()
    {
        // 检查玩家是否在空中并且速度接近0
        if (!onGround && Mathf.Abs(rb.velocity.x) < 0.01f && Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            canMove = false; // 禁用移动
        }
        else
        {
            canMove = true;
        }
    }

    private void HandleMovement()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
        HandleJump();
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck_front.position, groundCheckRadius);

        List<Collider2D> colliderList = new List<Collider2D>(colliders);

        colliderList.AddRange(Physics2D.OverlapCircleAll(groundCheck_back.position, groundCheckRadius));

        onGround = false;
        foreach (Collider2D collider in colliderList)
        {
            foreach (string gt in groundTag) {
                if (collider.CompareTag(gt))
                {
                    onGround = true;
                    break;
                }
            }
            if (onGround) { break; }
        }

        Debug.DrawRay(groundCheck_front.position, Vector2.down * groundCheckRadius, Color.red);
        Debug.DrawRay(groundCheck_back.position, Vector2.down * groundCheckRadius, Color.red);
    }

    private void HandleAnimation()
    {
        float yVel = rb.velocity.y;
        if (Mathf.Abs(yVel) < 0.01f)
        {
            yVel = 0;
        }
        anim.SetFloat("yVelocity", yVel);
        anim.SetFloat("xVelocity", rb.velocity.x);
        anim.SetBool("onGround", onGround);
    }

    private void Flip()
    {
        facingDir *= -1;
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180, 0);
    }

    private void TurnCheck()
    {
        if (xInput > 0 && !isFacingRight || xInput < 0 && isFacingRight)
        {
            Flip();
        }
    }

    public void CollectCheese()
    {
        collectedCheese += 1;
        Debug.Log("Current Collected Cheese: " + collectedCheese);
    }
}