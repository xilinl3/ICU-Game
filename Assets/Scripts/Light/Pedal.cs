using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedal : MonoBehaviour
{
    public GameObject sceneLight; // 控制的灯光对象
    private bool initialLightState; // 记录灯光的初始状态
    public bool boxOnPlate = false; // 是否有箱子或玩家在踏板上

    // 添加两个Sprite参数
    public Sprite defaultSprite;  // 默认的踏板图片
    public Sprite pressedSprite;  // 踏板被按下时的图片

    private SpriteRenderer spriteRenderer; // 用于切换踏板图片的SpriteRenderer
    private AudioSource PedalSound; // 踏板音效

    private void Start()
    {
        // 获取当前物体的SpriteRenderer组件
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 获取当前物体的AudioSource组件
        PedalSound = GetComponent<AudioSource>();

        // 设置初始图片为默认的踏板图片
        if (spriteRenderer != null && defaultSprite != null)
        {
            spriteRenderer.sprite = defaultSprite;
        }

        // 记录灯光的初始状态
        if (sceneLight != null)
        {
            initialLightState = sceneLight.activeSelf;
        }

    }

    // 当有物体进入踏板区域
    private void OnTriggerStay2D(Collider2D other)
    {
        // 检测物体是否为带有pressbox标签的箱子或玩家
        if (other.CompareTag("WoodenBox") || other.CompareTag("Player") || other.CompareTag("IronBox"))
        {
            boxOnPlate = true;

            // 根据灯光的初始状态切换灯光
            if (sceneLight != null)
            {
                sceneLight.SetActive(!initialLightState); // 反转灯光状态
            }

            // 切换到按下的踏板图片
            if (spriteRenderer != null && pressedSprite != null)
            {
                spriteRenderer.sprite = pressedSprite;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("WoodenBox") || other.CompareTag("Player") || other.CompareTag("IronBox"))
        {
            PedalSound.Play();
        }
    }

    // 当物体离开踏板区域
    private void OnTriggerExit2D(Collider2D other)
    {
        // 检测物体是否为带有pressbox标签的箱子或玩家
        if (other.CompareTag("WoodenBox") || other.CompareTag("Player") || other.CompareTag("IronBox"))
        {
            boxOnPlate = false;

            // 根据灯光的初始状态切换灯光
            if (sceneLight != null)
            {
                sceneLight.SetActive(initialLightState); // 恢复灯光到初始状态
            }

            // 切换回默认的踏板图片
            if (spriteRenderer != null && defaultSprite != null)
            {
                spriteRenderer.sprite = defaultSprite;
            }
        }
    }
}
