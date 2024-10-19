using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal; // 引入Light2D的命名空间

public class ButtonLight : MonoBehaviour
{
    // 颜色循环序列
    public Color32[] colorSequence = {
        new Color32(255, 255, 255, 255),  // 白色
        new Color32(255, 0, 0, 255),      // 红色
        new Color32(0, 255, 0, 255),      // 绿色
        new Color32(0, 0, 255, 255),      // 蓝色
    };

    public int currentColorIndex = 0; // 当前颜色的索引
    public SpriteRenderer Display;    // 用于显示的SpriteRenderer
    public Light2D sceneLight;        // Light2D组件
    public ButtonRangeDetector buttonRangeDetector;

    // 定义一个事件委托用于颜色变化
    public delegate void OnColorChange(Color newColor);
    public static event OnColorChange colorChangeEvent;

    // Start is called before the first frame update
    void Start()
    {
        // 获取 SpriteRenderer 组件
        Display.GetComponent<SpriteRenderer>();

        // 获取 Light2D 组件
        sceneLight.GetComponent<Light2D>();

        // 设置初始颜色为白色
        Display.color = colorSequence[currentColorIndex];
        sceneLight.color = colorSequence[currentColorIndex]; // 设置初始光源颜色
    }

    void OnEnable()
    {
        // 订阅 F 键事件
        Fevent.OnFKeyPressed += CheckLightsRange;
    }

    void OnDisable()
    {
        // 取消订阅 F 键事件，防止内存泄漏
        Fevent.OnFKeyPressed -= CheckLightsRange;
    }

    public void CheckLightsRange()
    {
        // 检查玩家是否在按钮范围内
        if (buttonRangeDetector != null && buttonRangeDetector.IsPlayerInRange())
        {
            ColorLoop();  // 切换灯光和Sprite

            // 当颜色变化时发布事件
            if (colorChangeEvent != null)
            {
                Debug.Log("change color event post");
                colorChangeEvent.Invoke(sceneLight.color); // 发布新颜色
            }
        }
    }

    void ColorLoop()
    {
        // 切换到下一个颜色
        currentColorIndex = (currentColorIndex + 1) % colorSequence.Length;

        // 设置SpriteRenderer的颜色为当前颜色
        Display.color = colorSequence[currentColorIndex];

        // 设置Light2D的颜色为当前颜色
        sceneLight.color = colorSequence[currentColorIndex];
    }
}

