using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f; // 移动速度
    private Vector3 target;
    private bool isStopped = false; // 用于标记平台是否停止
    // Start is called before the first frame update
    void Start()
    {
        target = pointB.position;
    }

    // Update is called once per frame
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
        // 打印碰撞信息
        //Debug.Log($"Collision detected with: {collision.gameObject.name}, tag: {collision.gameObject.tag}");
        if (collision.gameObject.CompareTag("MovePlatform"))
        {
            isStopped = true;
            //Debug.Log($"{gameObject.name} stopped due to collision with {collision.gameObject.name}");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 打印碰撞结束信息
        //Debug.Log($"Collision exit detected with: {collision.gameObject.name}, tag: {collision.gameObject.tag}");

        // 如果两个平台分开，则恢复平台的移动
        if (collision.gameObject.CompareTag("MovePlatform"))
        {
            isStopped = false;
            //Debug.Log($"{gameObject.name} resumed moving after separating from {collision.gameObject.name}");
        }
    }

}
