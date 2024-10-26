using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    // 移动速度
    public float speed = 2f;
    // 平台的初始位置状态
    private Vector3 target;
    private bool isStopped = false;

    void Start()
    {
        // 初始化目标为起点
        target = pointB.position;
    }

    void Update()
    {
        if (!isStopped)
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
        Debug.Log("撞到其他物体");
        // 如果碰撞的对象有“MovingPlatform”组件，则停止平台
        if (collision.gameObject.CompareTag("Ground"))
        {
            isStopped = true;
            Debug.Log($"{gameObject.name} stopped due to collision with {collision.gameObject.name}");
        }
    }
}
