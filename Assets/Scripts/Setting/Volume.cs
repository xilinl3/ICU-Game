using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volume : MonoBehaviour
{
    public static Volume Instance { get; private set; }
    private float initialVolume;
    public bool volumeChanged = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            // 从 PlayerPrefs 获取初始音量
            initialVolume = GetVolume();
            AudioListener.volume = initialVolume;

            // 确保初始状态下 volumeChanged 为 false
            volumeChanged = false;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetVolume(float volume)
    {
        // 检查音量是否与初始音量不同
        if (!Mathf.Approximately(initialVolume, volume))
        {
            volumeChanged = true;
            AudioListener.volume = volume;

            // 存储新的音量值
            PlayerPrefs.SetFloat("gameVolume", volume);
            PlayerPrefs.Save();
        }
    }

    public float GetVolume()
    {
        return PlayerPrefs.GetFloat("gameVolume", 1f);
    }

    public void ResetVolumeChange()
    {
        volumeChanged = false; // 重置音量变化标志
    }

    // 当应用程序退出时调用
    private void OnApplicationQuit()
    {
        ExitGame();
    }

    // 退出游戏的方法
    private void ExitGame()
    {
        ResetVolumeChange(); // 重置音量变化标志
        PlayerPrefs.DeleteAll(); // 清除所有存档数据
        Application.Quit(); // 退出应用程序
    }
}
