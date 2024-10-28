using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTime : MonoBehaviour
{
    public TextMeshProUGUI timerText;  // 显示计时器的 TMP UI 文本
    public GameObject stopPage;        // 引用 StopPage 对象
    public int minute;                 // 用于调整分钟数的字段
    private float startTime;           // 计时器的开始时间
    private float elapsedTime;         // 记录流逝的时间
    private bool isRunning = false;    // 计时器是否正在运行
    private bool isPaused = false;     // 游戏是否暂停

    // 游戏开始时调用
    void Start()
    {
        StartTimer();
        SetTime(minute); // 在开始时设置时间
    }

    // 启动计时器
    public void StartTimer()
    {
        startTime = Time.time; // 初始化开始时间
        isRunning = true;
        isPaused = false;
        elapsedTime = 0;
    }

    // 停止计时器
    public void StopTimer()
    {
        isRunning = false;
    }

    // 暂停计时器
    public void PauseTimer()
    {
        if (isRunning && !isPaused)
        {
            elapsedTime += Time.time - startTime;
            isPaused = true;
            isRunning = false;
        }
    }

    // 恢复计时器
    public void ResumeTimer()
    {
        if (!isRunning && isPaused)
        {
            startTime = Time.time;
            isPaused = false;
            isRunning = true;
        }
    }

    // 游戏通关时调用
    public void CompleteGame()
    {
        StopTimer(); // 停止计时
    }

    // 更新计时器
    void Update()
    {
        // 检查 StopPage 是否激活，激活时暂停计时器
        if (stopPage != null && stopPage.activeSelf)
        {
            PauseTimer();
        }
        else if (isPaused && !stopPage.activeSelf)
        {
            ResumeTimer();
        }

        // 更新计时器显示
        if (isRunning)
        {
            float currentTime = elapsedTime + (Time.time - startTime);
            int minutes = Mathf.FloorToInt(currentTime / 60F);
            int seconds = Mathf.FloorToInt(currentTime % 60F);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    // 设置当前时间为指定分钟数
    public void SetTime(int minutes)
    {
        elapsedTime = minutes * 60f;  // 将分钟数转换为秒数存储
        if (isRunning)
        {
            startTime = Time.time; // 更新 startTime 以便继续计时
        }
    }

    // 获取当前计时器的分钟数
    public int GetMinutes()
    {
        float currentTime = elapsedTime + (isRunning ? (Time.time - startTime) : 0);
        return Mathf.FloorToInt(currentTime / 60F);
    }
}
