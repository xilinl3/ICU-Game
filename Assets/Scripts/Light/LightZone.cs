using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightZone : MonoBehaviour
{
    private Light2D light2DComponent;  // Light2D组件

    private void Start()
    {
        // 获取Light2D组件
        light2DComponent = GetComponent<Light2D>();
    }

    // 当铁箱进入灯光区域
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("IronBox") && light2DComponent != null)
        {
            Ironbox ironbox = other.GetComponent<Ironbox>();
            if (ironbox != null)
            {
                // 调用铁箱的HandleLightColor方法，将灯光颜色传递给铁箱
                ironbox.HandleLightColor(light2DComponent.color);
            }
        }
    }

    // 当铁箱离开灯光区域
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("IronBox"))
        {
            Ironbox ironbox = other.GetComponent<Ironbox>();
            if (ironbox != null)
            {
                // 当铁箱离开光区域时，恢复到默认颜色（白色）
                ironbox.HandleLightColor(Color.white);
            }
        }
    }
}
