using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    public static Volume instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 确保切换场景时不销毁
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 设置音量
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("gameVolume", volume); // 保存音量设置
        PlayerPrefs.Save();
    }

    // 获取当前音量
    public float GetVolume()
    {
        return PlayerPrefs.GetFloat("gameVolume", 1f); // 默认音量为1
    }

    private void Start()
    {
        // 每次启动时恢复之前的音量设置
        AudioListener.volume = GetVolume();
    }
}
