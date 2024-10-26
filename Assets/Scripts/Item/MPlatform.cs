using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPlatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    // 移动速度
    public float speed = 2f;
    // 平台的初始位置状态
    private Vector3 target;
    private bool isMoving = true; // 标记是否继续移动

    void Start()
    {
        // 初始化目标为起点
        target = pointB.position;
    }

    void Update()
    {
        if (isMoving)
        {
            // 移动平台朝向目标点
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

            // 当平台接近目标点时切换目标
            if (Vector3.Distance(transform.position, target) < 0.1f)
            {
                target = (target == pointA.position) ? pointB.position : pointA.position;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 检查碰撞物体的标签是否为 "MovePlatform"
        if (collision.gameObject.CompareTag("MovePlatform"))
        {
            // 停止移动
            isMoving = false;
            Debug.Log("Collision detected with another moving platform. Stopping movement.");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 当离开与标签为 "MovePlatform" 的物体的碰撞时，恢复移动
        if (collision.gameObject.CompareTag("MovePlatform"))
        {
            isMoving = true;
            Debug.Log("Collision exited with another moving platform. Resuming movement.");
        }
    }
}
