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

    void Start()
    {
        // 初始化目标为起点
        target = pointB.position;
    }

    void Update()
    {
        // 移动平台朝向目标点
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // 当平台接近目标点时切换目标
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            target = (target == pointA.position) ? pointB.position : pointA.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collide");
        if (other.CompareTag("Ground"))
        {
            Debug.Log("Enter Ground");
            speed = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            Debug.Log("Exit Ground");
            speed = 2f;
        }
    }

}
