using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Component
    private Rigidbody2D rb;
    private Animator anim;

    // param
    public static int JUMPCOUNT = 1;
    public static int DASHCOUNT = 1;

    //PlayerADMove
    [SerializeField] private float moveSpeed = 5f; // 角色的移动速度
    private float xInput;
    private bool IsRight = true;
    private int facingDir = 1;
    private float dashTimeLeft;
    private float dashSpeed;


    [SerializeField] private bool onGround;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float sequentialJumpForce = 3f;
    [SerializeField] private int remJumpCount = JUMPCOUNT;
    [SerializeField] private float dashDistance = 5f;
    [SerializeField] private float dashDuration = 0.5f;
    [SerializeField] private bool isDashing = false;
    [SerializeField] private bool isCrouching = false;
    [SerializeField] private int remDashCount = DASHCOUNT;

    void Start()
    {
        // 获取角色的 Rigidbody2D 组件
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // 获取水平输入 (A, D 键或左、右箭头键)
        xInput  = Input.GetAxisRaw("Horizontal");

        if (!isDashing)
        {
            rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
            HandleJump();
        }
        HandleDash();
        HandleAnimation();
    }

    private void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Horizontal") > 0)
        {
            TurnCheck();
        }
    }

    private void HandleDash()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && remDashCount > 0)
        {
            StartDash();
            if(!onGround)
            {
                remDashCount -= 1;
            }
        }

        if (isDashing)
        {
            Dash();
        }
    }

    private void StartDash()
    {
        isDashing = true;
        dashTimeLeft = dashDuration;
        rb.gravityScale = 0;  //禁用重力

        // handle dash
        dashSpeed = dashDistance / dashDuration;
        rb.velocity = new Vector2(facingDir * dashSpeed, 0);
    }

    private void Dash()
    {
        if (dashTimeLeft > 0)
        {
            dashTimeLeft -= Time.deltaTime;  // 更新冲刺剩余时间
        }
        else
        {
            EndDash();  // 冲刺结束
        }
    }

    private void EndDash()
    {
        isDashing = false;
        rb.gravityScale = 1;  // 恢复重力
        rb.velocity = Vector2.zero;  // 停止冲刺时将速度重置
    }

    private void HandleJump()
    {
        //if (Input.GetKeyDown(KeyCode.Space) && onGround)
        //{
        //    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        //}
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (onGround)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                remJumpCount = JUMPCOUNT;
            }
            else if (remJumpCount != 0)
            {
                remJumpCount -= 1;
                rb.AddForce(Vector2.up * sequentialJumpForce, ForceMode2D.Impulse);
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            onGround = true;
            remJumpCount = JUMPCOUNT;
            remDashCount = DASHCOUNT;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            onGround = false;
            remDashCount = DASHCOUNT;
            remJumpCount = JUMPCOUNT;
        }
    }
    private void HandleAnimation()
    {
        float yVel = rb.velocity.y;
        if (Mathf.Abs(yVel) < 0.01f)  // 如果yVelocity小于0.01，则将其视为0
        {
            yVel = 0;
        }
        anim.SetFloat("yVelocity", yVel);
        anim.SetFloat("xVelocity", rb.velocity.x);
        anim.SetFloat("yVelocity", yVel);
        anim.SetBool("onGround", onGround);
        anim.SetBool("isDashing", isDashing);
        anim.SetBool("isCrouching", isCrouching);
    }
    private void Flip()
    {
        facingDir = facingDir * -1;
        transform.Rotate(0, 180, 0);
    }
    private void TurnCheck()
    {
        if (Input.GetAxis("Horizontal") > 0 && !IsRight || Input.GetAxis("Horizontal") < 0 && IsRight)
        {
            Flip();
            Trun();
        }
    }

    public void Trun()
    {
        if (IsRight)
        {
            //Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            //transform.rotation = Quaternion.Euler(rotator);
            IsRight = !IsRight;

            //tirn the camera follow object
            //_cameraFollowObject.CallTurn();
        }
        else
        {
            //Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
            //transform.rotation = Quaternion.Euler(rotator);
            IsRight = !IsRight;

            //_cameraFollowObject.CallTurn();
        }
    }
}
