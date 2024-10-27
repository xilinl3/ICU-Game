using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedal : MonoBehaviour
{
    public GameObject sceneLight; // 控制的灯光对象
    private bool initialLightState; // 记录灯光的初始状态
    private int objectCountOnPlate = 0; // 用于记录在踏板上的物体数量

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 检测物体是否为带有特定标签的箱子或玩家
        if (other.CompareTag("WoodenBox") || other.CompareTag("Player") || other.CompareTag("IronBox"))
        {
            if (objectCountOnPlate == 0)
            {
                // 播放音效并切换到按下状态
                PedalSound.Play();
                if (spriteRenderer != null && pressedSprite != null)
                {
                    spriteRenderer.sprite = pressedSprite;
                }

                // 切换灯光状态
                if (sceneLight != null)
                {
                    sceneLight.SetActive(!initialLightState); // 反转灯光状态
                }
            }

            // 增加物体计数
            objectCountOnPlate++;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // 检测物体是否为带有特定标签的箱子或玩家
        if (other.CompareTag("WoodenBox") || other.CompareTag("Player") || other.CompareTag("IronBox"))
        {
            // 减少物体计数
            objectCountOnPlate--;

            // 如果所有物体都离开了踏板，则恢复状态
            if (objectCountOnPlate <= 0)
            {
                objectCountOnPlate = 0; // 确保计数不小于零

                // 恢复灯光到初始状态
                if (sceneLight != null)
                {
                    sceneLight.SetActive(initialLightState);
                }

                // 切换回默认的踏板图片
                if (spriteRenderer != null && defaultSprite != null)
                {
                    spriteRenderer.sprite = defaultSprite;
                }
            }
        }
    }
}
