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
        // 计算 NPC 与 pointA 和 pointB 的距离，选择更远的点作为初始目标
        float distanceToPointA = Vector3.Distance(transform.position, pointA.position);
        float distanceToPointB = Vector3.Distance(transform.position, pointB.position);

        // 如果 NPC 离 B 点更远，则先去 B 点，否则先去 A 点
        target = (distanceToPointA > distanceToPointB) ? pointA : pointB;

        anim = GetComponent<Animator>();  // 获取动画组件
    }

    void Update()
    {
        // 移动 NPC
        Vector3 newPosition = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        transform.position = newPosition;

        // 计算到目标点的距离
        float distanceToTarget = Vector3.Distance(transform.position, target.position);


        // 检查是否到达目标点并切换目标
        if (distanceToTarget < 1f)
        {
            target = (target == pointB) ? pointA : pointB;
        }

        // 处理翻转和动画
        HandleAnimation();
    }

    private void HandleAnimation()
    {
        // 计算 NPC 的 X 轴速度
        float xVelocity = target.position.x - transform.position.x;

        // 设置动画参数 isMoving，如果有移动则为 true，否则为 false
        anim.SetBool("isMoving", Mathf.Abs(xVelocity) > 0.01f);

        // 检测方向改变，处理翻转
        if (xVelocity > 0.1f && !isRight)  // 增加精度容差，避免浮点误差
        {
            Flip();
        }
        else if (xVelocity < -0.1f && isRight)  // 增加精度容差，避免浮点误差
        {
            Flip();
        }
    }

    private void Flip()
    {
        // 翻转 NPC
        isRight = !isRight;
        facingDir *= -1;
        transform.Rotate(0, 180, 0);  // 水平翻转 NPC
    }
}

