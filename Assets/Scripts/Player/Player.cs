using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Component
    private Rigidbody2D rb;
    private Animator anim;

    // 参数
    public static int JUMPCOUNT = 1;
    public static int DASHCOUNT = 1;

    // 玩家移动参数
    [SerializeField] private float moveSpeed = 5f;
    private float xInput;
    private bool isFacingRight = true;
    private int facingDir = 1;
    private float dashTimeLeft;
    private float dashSpeed;

    // 玩家状态参数
    [SerializeField] private bool onGround;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float sequentialJumpForce = 3f;
    [SerializeField] private int remJumpCount = JUMPCOUNT;
    [SerializeField] private float dashDistance = 5f;
    [SerializeField] private float dashDuration = 0.5f;
    [SerializeField] private bool isDashing = false;
    [SerializeField] private bool isCrouching = false;
    [SerializeField] private int remDashCount = DASHCOUNT;
    private bool dashEnabled = false;  // 新增变量，控制是否允许冲刺

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (!isDashing)
        {
            rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
            HandleJump();
        }

        // 始终在Update中处理冲刺状态，确保冲刺能够结束
        if (isDashing)
        {
            Dash();
        }

        HandleAnimation();
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(xInput) > 0)
        {
            TurnCheck();
        }
    }

    //HandleDash暂时不在游戏中使用
    private void HandleDash()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && remDashCount > 0)
        {
            StartDash();
            if (!onGround)
            {
                remDashCount -= 1;
            }
        }
        if (isDashing)
        {
            Dash();
        }
    }
    private void LightDash()
    {
        // 确保可以冲刺且未在冲刺状态
        if (!isDashing && remDashCount > 0 && dashEnabled)
        {
            StartDash();
            if (!onGround)
            {
                remDashCount -= 1;
            }
        }
    }

    private void StartDash()
    {
        isDashing = true;
        dashTimeLeft = dashDuration;
        rb.gravityScale = 0;  //禁用重力

        // 处理冲刺
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
        dashEnabled = false;  // 冲刺结束后禁用冲刺，直到再次进入灯光
    }

    // 碰到灯光时触发强制冲刺
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Light"))
        {
            dashEnabled = true;  // 允许冲刺
            LightDash();  // 强制触发一次冲刺
            Debug.Log("Player entered the light's trigger zone");
        }
    }

    private void HandleJump()
    {
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
        if (Mathf.Abs(yVel) < 0.01f)
        {
            yVel = 0;
        }
        anim.SetFloat("yVelocity", yVel);
        anim.SetFloat("xVelocity", rb.velocity.x);
        anim.SetBool("onGround", onGround);
        anim.SetBool("isDashing", isDashing);
        anim.SetBool("isCrouching", isCrouching);
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
}
