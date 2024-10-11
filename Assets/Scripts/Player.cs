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
    public bool IsRight = true;
    private int facingDir = 1;

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
    }
    private void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Horizontal") > 0)
        {
            TurnCheck();
        }
    }

    private void HandleAnimation()
    {
        anim.SetFloat("xVelocity", rb.velocity.x);
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
