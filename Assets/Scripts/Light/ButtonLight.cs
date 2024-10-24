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
    public Light2D sceneLight;     // Light2D组件
    public ButtonRangeDetector buttonRangeDetector;

    public delegate void OnButtonLightColorChange(Color newColor);
    public static event OnButtonLightColorChange ButtonLightColorChanged;

    // Start is called before the first frame update
    void Start()
    {
        // 获取 Light2D 组件
        sceneLight.GetComponent<Light2D>();

        // 如果当前灯光颜色存在于颜色序列中，则设定相应的索引
        SetCurrentColorIndex(sceneLight.color);

        // 初始化灯光颜色
        sceneLight.color = colorSequence[currentColorIndex];
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
            ColorLoop();  // 切换灯光颜色
        }
    }

    void ColorLoop()
    {
        // 切换到下一个颜色
        currentColorIndex = (currentColorIndex + 1) % colorSequence.Length;

        // 设置Light2D的颜色为当前颜色
        sceneLight.color = colorSequence[currentColorIndex];

        if (ButtonLightColorChanged != null)
        {
            ButtonLightColorChanged.Invoke(sceneLight.color);  // 通知监听者颜色变化
        }
    }

    // 通过比较当前颜色与颜色序列中的颜色来设置初始索引
    private void SetCurrentColorIndex(Color currentColor)
    {
        for (int i = 0; i < colorSequence.Length; i++)
        {
            if (IsColorSimilar(currentColor, colorSequence[i], 0.01f))
            {
                currentColorIndex = i;
                break;
            }
        }
    }

    // 判断两个颜色是否相似（防止精度问题导致的误差）
    private bool IsColorSimilar(Color a, Color b, float tolerance)
    {
        return Mathf.Abs(a.r - b.r) < tolerance &&
               Mathf.Abs(a.g - b.g) < tolerance &&
               Mathf.Abs(a.b - b.b) < tolerance &&
               Mathf.Abs(a.a - b.a) < tolerance;
    }

    public void ResetColor()
    {
        currentColorIndex = 0;
        //Display.color = colorSequence[currentColorIndex];
        sceneLight.color = colorSequence[currentColorIndex];
    }
}
