using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour
{
    public Sprite switchonpic;  // 开灯时的图片
    public Sprite switchoffpic;  // 关灯时的图片
    public ButtonRangeDetector buttonRangeDetector;
    public GameObject[] lights;
    public bool defaultswithon;  // 控制灯光开关的初始状态
    private SpriteRenderer spriteRenderer;  // 用于控制当前的Sprite
    private AudioSource audioSource;

    void Start()
    {
        // 获取SpriteRenderer组件
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 设置初始的Sprite，不改变灯光的初始状态
        SetInitialSpriteState();
        audioSource = GetComponent<AudioSource>();
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
            audioSource.Play();
            ToggleLightsAndSprite();  // 切换灯光和Sprite
        }
    }

    public void ToggleLightsAndSprite()
    {
        // 切换Sprite
        if (spriteRenderer != null)
        {
            bool currentState = spriteRenderer.sprite == switchonpic;
            spriteRenderer.sprite = currentState ? switchoffpic : switchonpic;
        }

        // 切换灯光状态
        SetLightsActive();
    }

    private void SetLightsActive()
    {
        foreach (GameObject lightObj in lights)
        {
            if (lightObj != null)
            {
                // 切换灯光的相反状态
                lightObj.SetActive(!lightObj.activeSelf);
            }
        }
    }

    // 设置初始Sprite状态的方法（不改变灯光的状态）
    private void SetInitialSpriteState()
    {
        bool anyLightOn = false;

        // 检查是否有任何灯光是开启状态
        foreach (GameObject lightObj in lights)
        {
            if (lightObj != null && lightObj.activeSelf)
            {
                anyLightOn = true;
                break;
            }
        }

        // 根据灯光的状态来设置初始Sprite
        spriteRenderer.sprite = anyLightOn ? switchonpic : switchoffpic;
    }
}


