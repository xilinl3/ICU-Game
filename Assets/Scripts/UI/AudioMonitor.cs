using System.Collections.Generic;
using UnityEngine;

public class AudioMonitor : MonoBehaviour
{
    void Update()
    {
        // 获取场景中所有的AudioSource组件
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        
        // 创建一个列表来存储正在播放音频的组件
        List<AudioSource> activeAudioSources = new List<AudioSource>();

        foreach (AudioSource audioSource in audioSources)
        {
            // 检查组件是否在播放音频
            if (audioSource.isPlaying)
            {
                activeAudioSources.Add(audioSource);
            }
        }

        // 如果有音频正在播放，输出正在播放的组件名称
        if (activeAudioSources.Count > 0)
        {
            Debug.Log("当前正在播放音频的组件:");
            foreach (AudioSource audioSource in activeAudioSources)
            {
                Debug.Log(audioSource.gameObject.name);
            }
        }
        else
        {
            Debug.Log("当前没有音频在播放");
        }
    }
}
