using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Pedal : MonoBehaviour
{
    public Light2D sceneLight; // 需要控制的灯光
    public bool boxOnPlate = false; // 是否有箱子在踏板上

    private void Start()
    {
        sceneLight.enabled = false;
    }
    // 当有物体进入踏板区域
    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("Object entered: " + other.gameObject.name);
        // 检测物体是否为带有pressbox标签的箱子
        if (other.CompareTag("PressBox") || other.CompareTag("Player"))
        {
            boxOnPlate = true;
            sceneLight.enabled = true; // 打开灯
            //Debug.Log("Pressbox is on the plate, light is on.");
        }
    }

    // 当物体离开踏板区域
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Object Exit: " + other.gameObject.name);
        if (other.CompareTag("PressBox") || other.CompareTag("Player"))
        {
            boxOnPlate = false;
            sceneLight.enabled = false; // 关闭灯
            Debug.Log("Pressbox left the plate, light is off.");
        }
    }
}
