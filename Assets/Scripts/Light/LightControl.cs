using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightControl : MonoBehaviour
{
    public Sprite switchon;  // 开灯时的图片
    public Sprite switchoff;  // 关灯时的图片
    public ButtonRangeDetector buttonRangeDetector;
    public GameObject[] lights;
    public bool lightsOn;  // 控制灯光开关的状态
    private SpriteRenderer spriteRenderer;  // 用于控制当前的Sprite

    void Start()
    {
        // 获取SpriteRenderer组件
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 设置初始的Sprite和灯光状态
        spriteRenderer.sprite = lightsOn ? switchon : switchoff;
        SetLightsActive(lightsOn);
    }

    void OnEnable()
    {
        // 订阅 F 键事件
        Fevent.OnFKeyPressed += CheckToggleLightsRange;
    }

    void OnDisable()
    {
        // 取消订阅 F 键事件，防止内存泄漏
        Fevent.OnFKeyPressed -= CheckToggleLightsRange;
    }

    public void CheckToggleLightsRange()
    {
        // 检查玩家是否在按钮范围内
        if (buttonRangeDetector != null && buttonRangeDetector.IsPlayerInRange())
        {
            ToggleLightsAndSprite();  // 切换灯光和Sprite
        }
    }

    public void ToggleLightsAndSprite()
    {
        // 切换Sprite
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = lightsOn ? switchoff : switchon;
        }

        // 切换灯光状态
        SetLightsActive(!lightsOn);
        lightsOn = !lightsOn;
    }

    private void SetLightsActive(bool isActive)
    {
        foreach (GameObject lightObj in lights)
        {
            if (lightObj != null)
            {
                lightObj.SetActive(isActive);
                //Debug.Log(lightObj.name + " is " + (isActive ? "on" : "off"));
            }
        }
    }
}
