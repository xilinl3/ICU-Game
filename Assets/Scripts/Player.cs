using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 5f;  // 角色的移动速度

    private Rigidbody2D rb;
    private float moveInput;

    void Start()
    {
        // 获取角色的 Rigidbody2D 组件
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 获取水平输入 (A, D 键或左、右箭头键)
        moveInput = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        // 应用水平移动
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }
}
