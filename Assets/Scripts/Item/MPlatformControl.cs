using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPlatformControl : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 检查是否碰撞到带有 "Player" 标签的对象
        if (collision.gameObject.CompareTag("Player"))
        {
            // 将玩家的父对象设置为当前移动平台
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 当玩家离开平台时，取消父子关系
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
