using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoEndHandler : MonoBehaviour
{
    private VideoPlayer videoPlayer; // 引用 VideoPlayer 组件
    private AudioSource audioSource; // 引用 AudioSource 组件
    public string nextSceneName = "Ending"; // 要跳转的场景名称

    void Start()
    {
        // 获取 VideoPlayer 组件
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        // 获取或附加 AudioSource 组件（若没有则添加一个）
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // 将 VideoPlayer 的音频输出链接到 AudioSource
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.SetTargetAudioSource(0, audioSource);

        // 开始播放视频
        videoPlayer.Play();
        audioSource.Play();

        // 注册视频播放完成事件
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    // 视频播放完成时的回调函数
    private void OnVideoEnd(VideoPlayer vp)
    {
        // 停止音频播放
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        // 切换到下一个场景
        SceneManager.LoadScene(nextSceneName);
    }
}
