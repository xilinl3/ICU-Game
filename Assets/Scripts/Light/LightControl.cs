using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightControl : MonoBehaviour
{
    public Sprite Sprite1;
    public Sprite Sprite2;
    public ButtonRangeDetector buttonRangeDetector;
    public GameObject[] lights;
    private bool lightsOn = true;

  
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
            Debug.Log("进入碰撞体");
            ToggleLights();
        }
        else
        {
            Debug.Log("Player is not in range to toggle the lights.");
        }
    }
    public void ToggleLights()
    {
        if (lightsOn)
        {
            // 如果灯光当前是开启的，关闭所有灯光
            SetLightsActive(false);
        }
        else
        {
            // 如果灯光当前是关闭的，打开所有灯光
            SetLightsActive(true);
        }

        // 切换灯光状态
        lightsOn = !lightsOn;

    }

    private void SetLightsActive(bool isActive)
    {
        foreach (GameObject lightObj in lights)
        {
            if (lightObj != null)
            {
                lightObj.SetActive(isActive);
                Debug.Log(lightObj.name + " is " + (isActive ? "on" : "off"));
            }
        }
    }

}
