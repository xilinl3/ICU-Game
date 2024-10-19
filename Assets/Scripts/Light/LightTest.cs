using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;  // 使用 Light2D

public class LightTest : MonoBehaviour
{
    private Light2D light2D;  // Light2D 引用

    void Start()
    {
        // 获取当前对象上的 Light2D 组件
        light2D = GetComponent<Light2D>();
    }

    // 当物体进入触发器区域时调用
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 输出进入触发器的物体的名称和Tag
        Debug.Log("Object entered trigger: " + other.gameObject.name + " with tag: " + other.gameObject.tag);
    }

    // 当物体离开触发器区域时调用
    private void OnTriggerExit2D(Collider2D other)
    {
        // 输出离开触发器的物体的名称和Tag
        Debug.Log("Object exited trigger: " + other.gameObject.name + " with tag: " + other.gameObject.tag);
    }
}

