using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Component
    private Rigidbody2D rb;
    private Animator anim;

    //PlayerADMove
    [SerializeField] private float moveSpeed = 5f; // 角色的移动速度
    private float xInput;
    private bool IsRight = true;
    private int facingDir = 1;

    [SerializeField] private bool onGround;
    public static float JUMPCOUNT = 1;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float sequentialJumpForce = 3f;
    [SerializeField] private float remJumpCount = JUMPCOUNT;

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
        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
        HandleAnimation();
        HandleJump();
    }

    private void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Horizontal") > 0)
        {
            TurnCheck();
        }
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
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            onGround = false;
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
