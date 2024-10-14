using UnityEngine;
using UnityEngine.UI;

public class AutoScroll : MonoBehaviour
{
    public Scrollbar scrollbar;   // 滚动条
    public float scrollSpeed = 0.1f;  // 滚动速度

    private bool isScrolling = true;  // 控制是否滚动

    void Start()
    {
        // 在开始时启动滚动
        isScrolling = true;
    }

    void Update()
    {
        if (isScrolling)
        {
            // 每帧减少Scrollbar的值，模拟向下滚动
            scrollbar.value -= scrollSpeed * Time.deltaTime;

            // 当Scrollbar的值为0时，停止滚动
            if (scrollbar.value <= 0f)
            {
                scrollbar.value = 0f;
                isScrolling = false; // 停止滚动
            }
        }
    }
}
