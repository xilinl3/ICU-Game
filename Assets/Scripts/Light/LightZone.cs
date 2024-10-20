using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightZone : MonoBehaviour
{
    public delegate void OnIronBoxEnterLight(Color lightColor);
    public static event OnIronBoxEnterLight IronBoxEntered;

    public delegate void OnIronBoxExitLight();
    public static event OnIronBoxExitLight IronBoxExited;  // 定义新的事件

    private Light2D light2DComponent;

    private void Start()
    {
        // 获取 Light2D 组件
        light2DComponent = GetComponent<Light2D>();
    }

    // 当有物体进入灯光范围时调用
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug 进入的物体的名字和标签
        //Debug.Log("Object entered: " + other.gameObject.name + " with tag: " + other.gameObject.tag);

        if (other.CompareTag("IronBox"))
        {
            // 如果是铁箱，发布灯光颜色事件
            if (IronBoxEntered != null)
            {
                //Debug.Log("发布颜色事件: " + light2DComponent.color);
                IronBoxEntered.Invoke(light2DComponent.color);
            }
        }
    }

    // 当有物体离开灯光范围时调用
    private void OnTriggerExit2D(Collider2D other)
    {
        // Debug 离开的物体的名字和标签
        //Debug.Log("Object exited: " + other.gameObject.name + " with tag: " + other.gameObject.tag);

        if (other.CompareTag("IronBox"))
        {
            // 如果是铁箱，发布退出事件通知恢复状态
            if (IronBoxExited != null)
            {
                //Debug.Log("发布退出事件，铁箱重置到正常状态");
                IronBoxExited.Invoke();  // 通知铁箱重置为 Normal 状态
            }
        }
    }
}


