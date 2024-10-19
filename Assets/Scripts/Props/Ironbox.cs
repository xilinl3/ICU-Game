using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Ironbox : MonoBehaviour
{
    public Sprite normalSprite;  // 白色光时的 Sprite
    public Sprite lightSprite;   // 绿色光时的 Sprite
    public Sprite smallSprite;   // 红色光时的 Sprite
    public Sprite frozenSprite;  // 蓝色光时的 Sprite

    private SpriteRenderer spriteRenderer;

    public enum BoxState
    {
        Normal,    // 白色光：常态，不可被推
        Light,     // 绿色光：变轻，可被推
        Small,     // 红色光：变小
        Frozen     // 蓝色光：时停
    }

    private void Start()
    {
        // 获取 SpriteRenderer 组件
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            // 获取 SpriteRenderer 的当前颜色
            Color currentColor = spriteRenderer.color;

            // 根据当前颜色设置 sprite 或颜色
            HandleLightColor(currentColor);
        }
    }

    private void OnEnable()
    {
        // 订阅 ButtonLight 的颜色变化事件
        ButtonLight.colorChangeEvent += OnLightColorChanged;
    }

    private void OnDisable()
    {
        // 取消订阅，防止内存泄漏
        ButtonLight.colorChangeEvent -= OnLightColorChanged;
    }

    // 当颜色变化时调用该方法
    private void OnLightColorChanged(Color newColor)
    {
        // 根据新的颜色判断箱子的状态
        HandleLightColor(newColor);
    }

    // 当铁箱进入光源的触发器区域时调用
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("铁箱检测到灯光trigger");
        // 检查碰撞的物体是否是灯光
        if (other.CompareTag("ColorLight"))
        {
            // 获取 Light2D 组件
            Light2D light = other.GetComponent<Light2D>();
            if (light != null)
            {
                Debug.Log("获取灯的颜色");

                // 获取灯光的颜色
                Color lightColor = light.color;

                // 处理灯光颜色
                HandleLightColor(lightColor);
            }
        }
    }

    // 当铁箱离开光源的触发器区域时调用
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ColorLight"))
        {
            Debug.Log("铁箱离开灯光trigger，恢复白色");

            // 恢复为白色状态
            HandleLightColor(Color.white);
        }
    }

    // 处理灯光颜色的方法
    public void HandleLightColor(Color lightColor)
    {
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer is missing!");
            return;
        }

        // 根据灯光颜色判断箱子的状态并设置对应的 sprite 或颜色
        if (lightColor == Color.white)
        {
            Debug.Log("白色");

            // 如果有指定的 normalSprite，则设置为 normalSprite，否则调整颜色
            if (normalSprite != null)
            {
                spriteRenderer.sprite = normalSprite;
            }
            else
            {
                spriteRenderer.color = new Color(1f, 1f, 1f, 1f); // 白色
            }
        }
        else if (lightColor == Color.green)
        {
            Debug.Log("绿色");

            // 如果有指定的 lightSprite，则设置为 lightSprite，否则调整颜色
            if (lightSprite != null)
            {
                spriteRenderer.sprite = lightSprite;
            }
            else
            {
                spriteRenderer.color = new Color(0f, 1f, 0f, 1f); // 绿色
            }
        }
        else if (lightColor == Color.red)
        {
            Debug.Log("红色");

            // 如果有指定的 smallSprite，则设置为 smallSprite，否则调整颜色
            if (smallSprite != null)
            {
                spriteRenderer.sprite = smallSprite;
            }
            else
            {
                spriteRenderer.color = new Color(1f, 0f, 0f, 1f); // 红色
            }
        }
        else if (lightColor == Color.blue)
        {
            Debug.Log("蓝色");

            // 如果有指定的 frozenSprite，则设置为 frozenSprite，否则调整颜色
            if (frozenSprite != null)
            {
                spriteRenderer.sprite = frozenSprite;
            }
            else
            {
                spriteRenderer.color = new Color(0f, 0f, 1f, 1f); // 蓝色
            }
        }
        else
        {
            Debug.Log("未知颜色");

            // 如果颜色未知，默认设置为灰色
            spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f, 1f); // 灰色
        }
    }
}



