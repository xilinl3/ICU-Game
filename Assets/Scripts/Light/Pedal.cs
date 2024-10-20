using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedal : MonoBehaviour
{
    public GameObject sceneLight; // 控制的灯光对象
    public bool boxOnPlate = false; // 是否有箱子或玩家在踏板上

    // 添加两个Sprite参数
    public Sprite defaultSprite;  // 默认的踏板图片
    public Sprite pressedSprite;  // 踏板被按下时的图片

    private SpriteRenderer spriteRenderer; // 用于切换踏板图片的SpriteRenderer

    private void Start()
    {
        // 初始关闭灯光对象
        sceneLight.SetActive(false);

        // 获取当前物体的SpriteRenderer组件
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 设置初始图片为默认的踏板图片
        if (spriteRenderer != null && defaultSprite != null)
        {
            spriteRenderer.sprite = defaultSprite;
        }
    }

    // 当有物体进入踏板区域
    private void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log("Object entered: " + other.gameObject.name);

        // 检测物体是否为带有pressbox标签的箱子或玩家
        if (other.CompareTag("WoodenBox") || other.CompareTag("Player"))
        {
            boxOnPlate = true;
            sceneLight.SetActive(true); // 打开灯

            // 切换到按下的踏板图片
            if (spriteRenderer != null && pressedSprite != null)
            {
                spriteRenderer.sprite = pressedSprite;
            }
        }
    }

    // 当物体离开踏板区域
    private void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log("Object Exit: " + other.gameObject.name);

        // 检测物体是否为带有pressbox标签的箱子或玩家
        if (other.CompareTag("WoodenBox") || other.CompareTag("Player"))
        {
            boxOnPlate = false;
            sceneLight.SetActive(false); // 关闭灯

            // 切换回默认的踏板图片
            if (spriteRenderer != null && defaultSprite != null)
            {
                spriteRenderer.sprite = defaultSprite;
            }

            //Debug.Log("Pressbox left the plate, light is off.");
        }
    }
}


