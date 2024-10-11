using UnityEngine;
using System.Collections.Generic;

public class LightMove : MonoBehaviour
{
    public List<Transform> points;  // 移动路径上的点
    public float moveSpeed = 5f;    // 移动速度
    public bool repeat = true;      // 是否循环往返

    private int currentPointIndex = 0; // 当前目标点索引
    private bool movingForward = true; // 是否在正向移动

    void Update()
    {
        if (points.Count == 0) return; // 如果没有点，直接返回

        // 移动到当前目标点
        Transform targetPoint = points[currentPointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);

        // 如果到达目标点
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.01f)
        {
            // 正向移动
            if (movingForward)
            {
                // 到达最后一个点
                if (currentPointIndex >= points.Count - 1)
                {
                    if (repeat)
                    {
                        movingForward = false; // 切换为反向移动
                    }
                    else
                    {
                        currentPointIndex = 0; // 如果不需要返回，重置到第一个点
                    }
                }
                else
                {
                    currentPointIndex++; // 移动到下一个点
                }
            }
            // 反向移动
            else
            {
                // 到达第一个点
                if (currentPointIndex <= 0)
                {
                    movingForward = true; // 切换为正向移动
                }
                else
                {
                    currentPointIndex--; // 移动到上一个点
                }
            }
        }
    }
}

