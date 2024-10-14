using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStop : MonoBehaviour
{
    private bool isTimeStopped = false;
    void Update()
    {
        // 当按下H键时切换时间状态
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (isTimeStopped)
            {
                ResumeTime();
            }
            else
            {
                StopTime();
            }
        }
    }

    // 暂停时间
    public void StopTime()
    {
        Time.timeScale = 0f;  // 将时间缩放设置为0，暂停所有时间相关行为
        Time.fixedDeltaTime = 0.02f * Time.timeScale;  // 更新物理帧率
        isTimeStopped = true;
        Debug.Log("Time Stopped");
    }

    // 恢复时间
    public void ResumeTime()
    {
        Time.timeScale = 1f;  // 将时间缩放恢复为1，恢复时间流动
        Time.fixedDeltaTime = 0.02f * Time.timeScale;  // 恢复物理帧率
        isTimeStopped = false;
        Debug.Log("Time Resumed");
    }
}
