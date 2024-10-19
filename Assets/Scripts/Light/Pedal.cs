using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Pedal : MonoBehaviour
{
<<<<<<< Updated upstream
    public Light2D sceneLight; // 需要控制的灯光
    public bool boxOnPlate = false; // 是否有箱子在踏板上

    private void Start()
    {
        sceneLight.enabled = false;
=======
    public GameObject sceneLight;  // 控制的灯光对象
    public Sprite defaultSprite;   // 默认的踏板图片
    public Sprite pressedSprite;   // 踏板被按下时的图片

    private SpriteRenderer spriteRenderer;  // SpriteRenderer组件

    private void Start()
    {
        // 初始关闭灯光
        sceneLight.SetActive(false);

        // 获取当前物体的SpriteRenderer组件
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 设置初始图片为默认的踏板图片
        if (spriteRenderer != null && defaultSprite != null)
        {
            spriteRenderer.sprite = defaultSprite;
        }
>>>>>>> Stashed changes
    }
    // 当有物体进入踏板区域
    private void OnTriggerEnter2D(Collider2D other)
    {
<<<<<<< Updated upstream
        Debug.Log("Object entered: " + other.gameObject.name);
        // 检测物体是否为带有pressbox标签的箱子
        if (other.CompareTag("PressBox") || other.CompareTag("Player"))
        {
            boxOnPlate = true;
            sceneLight.enabled = true; // 打开灯
            Debug.Log("Pressbox is on the plate, light is on.");
=======
        if (other.CompareTag("WoodenBox") || other.CompareTag("Player") || other.CompareTag("IronBox"))
        {
            Debug.Log("Object entered: " + other.gameObject.name);

            // 打开灯光
            sceneLight.SetActive(true);

            // 切换到按下的踏板图片
            if (spriteRenderer != null && pressedSprite != null)
            {
                spriteRenderer.sprite = pressedSprite;
            }

            // 如果是铁箱，修改其颜色
            if (other.CompareTag("IronBox"))
            {
                Ironbox ironbox = other.GetComponent<Ironbox>();
                if (ironbox != null)
                {
                    Light2D light2DComponent = sceneLight.GetComponent<Light2D>(); // 获取灯光的颜色
                    if (light2DComponent != null)
                    {
                        ironbox.HandleLightColor(light2DComponent.color); // 根据灯光颜色修改铁箱颜色
                    }
                }
            }
>>>>>>> Stashed changes
        }
    }

    // 当物体离开踏板区域
    private void OnTriggerExit2D(Collider2D other)
    {
<<<<<<< Updated upstream
        Debug.Log("Object Exit: " + other.gameObject.name);
        if (other.CompareTag("PressBox") || other.CompareTag("Player"))
        {
            boxOnPlate = false;
            sceneLight.enabled = false; // 关闭灯
            Debug.Log("Pressbox left the plate, light is off.");
        }
    }
}
=======
        if (other.CompareTag("WoodenBox") || other.CompareTag("Player") || other.CompareTag("IronBox"))
        {
            Debug.Log("Object exited: " + other.gameObject.name);

            // 关闭灯光
            sceneLight.SetActive(false);

            // 切换回默认的踏板图片
            if (spriteRenderer != null && defaultSprite != null)
            {
                spriteRenderer.sprite = defaultSprite;
            }

            // 如果是铁箱，恢复默认颜色
            if (other.CompareTag("IronBox"))
            {
                Ironbox ironbox = other.GetComponent<Ironbox>();
                if (ironbox != null)
                {
                    ironbox.HandleLightColor(Color.white); // 恢复到白色
                }
            }
        }
    }
}


>>>>>>> Stashed changes
