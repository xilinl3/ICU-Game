using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] private Transform pointA;  // A点的位置
    [SerializeField] private Transform pointB;  // B点的位置
    [SerializeField] private float speed = 2f;  // 移动速度
    private Transform target;

    private Animator anim;
    private bool isRight = true;  // 当前是否朝向右侧
    private int facingDir = 1;    // 当前朝向，1表示右，-1表示左

    void Start()
    {
        target = pointB;  // 初始目标设为B点
        anim = GetComponent<Animator>();  // 获取动画组件
    }

    void Update()
    {
        //Debug.Log(Vector3.Distance(transform.position, target.position));
        // 移动 NPC
        Vector3 newPosition = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        transform.position = newPosition;

        // 检查是否到达目标点并切换目标
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            target = (target == pointB) ? pointA : pointB;
        }

        // 处理翻转和动画
        HandleAnimation();
    }

    private void HandleAnimation()
    {
        // 计算 NPC 的 X 轴速度
        float xVelocity = (target.position.x - transform.position.x);

        // 设置动画参数isMoving，如果有移动则为true，否则为false
        anim.SetBool("isMoving", Mathf.Abs(xVelocity) > 0.01f);

        // 检测方向改变，处理翻转
        if (xVelocity > 0 && !isRight)
        {
            Flip();
        }
        else if (xVelocity < 0 && isRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        // 翻转 NPC
        isRight = !isRight;
        facingDir = facingDir * -1;
        transform.Rotate(0, 180, 0);  // 水平翻转 NPC
    }
}
