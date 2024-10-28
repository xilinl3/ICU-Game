using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class player_behaviors : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    private float stop;

    // 参数
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private Transform groundCheck_front;
    [SerializeField] private Transform groundCheck_back;
    [SerializeField] private Transform groundCheck_mid;
    [SerializeField] private float groundCheckRadius = 0.1f;
    [SerializeField] private string[] groundTag;
    [SerializeField] private TextMeshProUGUI cheeseCounterText;
    [SerializeField] private float jumpCooldown = 0.2f; // 跳跃冷却时间
    private float lastJumpTime = 0f; // 上一次跳跃的时间

    private Joystick joystick;
    private float xInput;
    private bool isFacingRight = true;
    private int facingDir = 1;

    // 玩家状态
    private bool onGround;
    private bool canMove = true;

    // 已收集的奶酪个数
    public int collectedCheese = 0;
    [SerializeField] public int totalCheese = 10;
    [SerializeField] private GameObject cheeseCollectionPanel;
    public bool isMobile = false;

    public void EnterMobile()
    {
        FindFirstObjectByType<UIJumpButton>().UpdatePlayersRef(this);
        joystick = FindFirstObjectByType<Joystick>();
        isMobile = true;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        UI uiInstance = FindObjectOfType<UI>();
        stop = uiInstance.amendment;
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
        if (isMobile)
        {
            xInput = joystick.Horizontal;

            // 确保xInput只处理左右移动，如果上下的移动被误操作导致输入，强制其为0
            if (Mathf.Abs(joystick.Vertical) > 0.8f)
            {
                xInput = 0;
            }
        }
        else
        {
            xInput = Input.GetAxisRaw("Horizontal");
        }


        rb.velocity = new Vector2(xInput * moveSpeed * stop, rb.velocity.y);
        HandleJump();
    }

    public void HandleUIJump()
    {
        if (onGround && Time.time - lastJumpTime >= jumpCooldown)
        {
            // 重置垂直速度，防止跳跃叠加
            rb.velocity = new Vector2(rb.velocity.x, 0f);

            // 添加跳跃力
            rb.AddForce(Vector2.up * jumpForce * stop, ForceMode2D.Impulse);

            // 更新上一次跳跃的时间
            lastJumpTime = Time.time;
        }
    }
    public void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onGround && Time.time - lastJumpTime >= jumpCooldown)
        {
            // 重置垂直速度，防止跳跃叠加
            rb.velocity = new Vector2(rb.velocity.x, 0f);

            // 添加跳跃力
            rb.AddForce(Vector2.up * jumpForce * stop, ForceMode2D.Impulse);

            // 更新上一次跳跃的时间
            lastJumpTime = Time.time;
        }
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck_front.position, groundCheckRadius);

        List<Collider2D> colliderList = new List<Collider2D>(colliders);

        colliderList.AddRange(Physics2D.OverlapCircleAll(groundCheck_back.position, groundCheckRadius));
        colliderList.AddRange(Physics2D.OverlapCircleAll(groundCheck_mid.position, groundCheckRadius));

        onGround = false;
        foreach (Collider2D collider in colliderList)
        {
            // 检查 collider 是否在 groundTag 列表中，并且 isTrigger 不是 true
            foreach (string gt in groundTag)
            {
                if (collider.CompareTag(gt) && !collider.isTrigger)
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
        if (stop == 1)
        {
            facingDir *= -1;
            isFacingRight = !isFacingRight;
            transform.Rotate(0, 180, 0);
        }
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
        //Debug.Log("Current Collected Cheese: " + collectedCheese);
        UpdateUI();
        StartCoroutine(ShowCheeseCollectionUI());
    }

    public int GetChessNum()
    {
        return collectedCheese;
    }

    public int GetTotalCheese()
    {
        return totalCheese;
    }

    private void UpdateUI()
    {
        if (cheeseCounterText != null)
        {
            cheeseCounterText.text = collectedCheese + "/" + totalCheese;
        }
    }

    private IEnumerator ShowCheeseCollectionUI()
    {
        // 激活 UI 面板
        cheeseCollectionPanel.SetActive(true);

        // 等待 1 秒
        yield return new WaitForSeconds(1f);

        // 关闭 UI 面板
        cheeseCollectionPanel.SetActive(false);
    }
}
