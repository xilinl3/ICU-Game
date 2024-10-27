using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoEndHandler : MonoBehaviour
{
    private VideoPlayer videoPlayer; // 引用 VideoPlayer 组件
    private string nextSceneName = "ending"; // 要跳转的场景名称

    void Start()
    {
        // 如果没有手动在 Inspector 中设置 VideoPlayer，则自动获取
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        // 注册视频播放完成事件
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    // 视频播放完成时的回调函数
    private void OnVideoEnd(VideoPlayer vp)
    {
        // 切换到下一个场景
        SceneManager.LoadScene(nextSceneName);
    }
}
