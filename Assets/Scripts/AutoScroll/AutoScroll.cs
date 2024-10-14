using UnityEngine;
using UnityEngine.UI;

public class SmoothAutoScroll : MonoBehaviour
{
    public ScrollRect scrollRect;
    public float scrollSpeed = 0.01f; // 设定每帧滚动的步长

    private bool isScrolling = true;  // 控制是否滚动

    void Start()
    {
        // 在Start方法中开始滚动
        isScrolling = true;
    }

    void Update()
    {
        if (isScrolling)
        {
            // 控制每帧减少verticalNormalizedPosition的值，使其逐渐滚动
            scrollRect.verticalNormalizedPosition -= scrollSpeed * Time.deltaTime;

            // 确保滚动条不会超出底部（0表示底部）
            if (scrollRect.verticalNormalizedPosition <= 0f)
            {
                scrollRect.verticalNormalizedPosition = 0f;
                isScrolling = false; // 停止滚动
            }
        }
    }
}

