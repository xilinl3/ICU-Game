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
    [SerializeField] private bool isStanding;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float sequentialJumpForce = 3f;
    [SerializeField] private int remJumpCount = JUMPCOUNT;
    [SerializeField] private float dashDistance = 5f;
    [SerializeField] private float dashDuration = 0.5f;
    [SerializeField] private bool isDashing = false;
    [SerializeField] private bool isCrouching = false;
    [SerializeField] private int remDashCount = DASHCOUNT;
    private bool dashEnabled = false;  // 控制是否允许冲刺

    [SerializeField] private bool isFirstTimeDash = true;
    [SerializeField] private GameObject LightPanel;
    private bool isTimeStopped = false;

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
        HandleBite();
        HandleStandups();
        HandleSitdowns();

        // 检查是否按下 F 键关闭 LightPanel 并恢复时间
        if (isTimeStopped && Input.GetKeyDown(KeyCode.F))
        {
            LightPanel.SetActive(false);
            ResumeTime();
        }
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(xInput) > 0)
        {
            TurnCheck();
        }
    }

    private void HandleStandups()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (isStanding || !onGround || xInput != 0) { return; }
            else
            {
                isStanding = true;
            }
        }
    }
    
    private void HandleSitdowns()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            if (!isStanding) { return; }
            else { isStanding = false; }
        }
    }

    private void HandleBite()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isTimeStopped)
        {
            anim.SetBool("Bite", true);
        }
    }

    public void ResetBite()
    {
        anim.SetBool("Bite", false);  // 将Bite设为false
    }

    private void LightDash()
    {
        if (isFirstTimeDash)
        {
            StopTime();
            LightPanel.SetActive(true);  // 启用 LightPanel
            isFirstTimeDash = false;
            isTimeStopped = true;  // 标记时间已停止
        }

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

    private void StopTime()
    {
        Time.timeScale = 0f;  // 将时间缩放设置为0，暂停所有时间相关行为
        Time.fixedDeltaTime = 0.02f * Time.timeScale;  // 更新物理帧率
        isTimeStopped = true;
    }

    private void ResumeTime()
    {
        Time.timeScale = 1f;  // 将时间缩放恢复为1，恢复时间流动
        Time.fixedDeltaTime = 0.02f * Time.timeScale;  // 恢复物理帧率
        isTimeStopped = false;
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
        anim.SetBool("isStanding", isStanding);
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

