using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;  // 使用 Light2D

public class RedLight : MonoBehaviour
{
    private Light2D redLight;   // Light2D 引用
    public GameObject redbox;   // redbox 的引用
    public Vector3 targetSize = new Vector3(0.5f, 0.5f, 1f);  // 目标大小
    public Vector3 originalSize;  // redbox 的原始大小
    public float shrinkSpeed = 2f;  // 缩小速度
    private bool isInLight = false; // 标记 redbox 是否在光照范围

    void Start()
    {
        // 获取当前对象上的 Light2D 组件
        redLight = GetComponent<Light2D>();

        // 记录 redbox 的原始大小
        originalSize = redbox.transform.localScale;
    }

    void Update()
    {
        // 如果 redbox 在光照范围内，则逐渐缩小 redbox
        if (isInLight)
        {
            //Debug.Log("Redbox is within the light's trigger area.");
            ShrinkRedbox();
        }
        else
        {
            // 当 redbox 离开光照范围时，逐渐恢复原始大小
            ResetRedboxSize();
        }
    }

    // 当 redbox 进入触发器区域时调用
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Object entered trigger: " + other.gameObject.name);
        if (other.CompareTag("Redbox"))
        {
            //Debug.Log("Redbox has entered the light's trigger area.");
            isInLight = true;  // 标记 redbox 进入光照范围
        }
    }

    // 当 redbox 离开触发器区域时调用
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == redbox)
        {
            //Debug.Log("Redbox has exited the light's trigger area.");
            isInLight = false;  // 标记 redbox 离开光照范围
        }
    }

    // 缩小 redbox 的方法
    void ShrinkRedbox()
    {
        // 逐渐将 redbox 的大小缩小到目标大小
        if (redbox.transform.localScale.x > targetSize.x && redbox.transform.localScale.y > targetSize.y)
        {
            // 使用 Lerp 进行平滑缩放
            redbox.transform.localScale = Vector3.Lerp(redbox.transform.localScale, targetSize, shrinkSpeed * Time.deltaTime);
        }
    }

    // 恢复 redbox 原始大小的方法
    void ResetRedboxSize()
    {
        // 逐渐将 redbox 恢复到原始大小
        if (redbox.transform.localScale.x < originalSize.x && redbox.transform.localScale.y < originalSize.y)
        {
            // 使用 Lerp 进行平滑恢复
            redbox.transform.localScale = Vector3.Lerp(redbox.transform.localScale, originalSize, shrinkSpeed * Time.deltaTime);
        }
    }
}

