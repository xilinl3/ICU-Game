using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Pedal : MonoBehaviour
{
    public Light2D sceneLight; // 需要控制的灯光
    private bool boxOnPlate = false; // 是否有箱子在踏板上

    void Start()
    {
        sceneLight.gameObject.SetActive(false);
    }
    // 当有物体进入踏板区域
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Object entered: " + other.gameObject.name);
        // 检测物体是否为箱子
        if (other.CompareTag("PressBox"))
        {
            boxOnPlate = true;
            sceneLight.enabled = true; // 打开灯
            sceneLight.gameObject.SetActive(true);
        }
    }

    // 当物体离开踏板区域
    private void OnTriggerExit2D(Collider2D other)
    {
        // 检测物体是否为箱子
        if (other.CompareTag("PressBox"))
        {
            boxOnPlate = false;
            sceneLight.enabled = false; // 关闭灯
            sceneLight.gameObject.SetActive(false);
        }
    }
}
