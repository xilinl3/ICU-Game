using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class IronBox : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isInRed = false;
    public GameObject IronBoxInstance;   
    public Vector3 targetSize = new Vector3(2f, 2f, 2f);  // 目标大小
    public Vector3 originalSize;  
    public float shrinkSpeed = 2f;  // 缩小速度
    private BoxState currentState;
    private bool isinlight = false;
    private Vector3 initialPosition;  // 初始位置
    private bool ignoreLightChanges = false;// 是否忽略光照变化
    private SpriteRenderer spriteRenderer;

    public enum BoxState
    {
        Normal,  // 常态，不可被推动
        Light,   // 变轻，可被推动
        Large,   // 放大状态
        Frozen   // 时停状态
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalSize = IronBoxInstance.transform.localScale;  // 初始化原始大小
        initialPosition = IronBoxInstance.transform.position;  // 初始化初始位置
        SetBoxState(BoxState.Normal);  // 初始化为常态
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //Debug.Log("Current IronBox State: " + currentState);
        // 如果 redbox 在光照范围内，则逐渐放大 redbox
        if (isInRed)
        {
            //Debug.Log("RedUpdate - 放大箱子");
            ShrinkRedbox();  // 这里应该放大箱子
        }
        else
        {
            // 当 redbox 离开光照范围时，逐渐恢复原始大小
            //Debug.Log("RedRest - 恢复箱子大小");
            ResetRedboxSize();
        }
    }

    //private void OnEnable()
    //{
    //    // 订阅灯光进入和离开的事件
    //    LightZone.IronBoxEntered += HandleLightEnter;
    //    LightZone.IronBoxExited += HandleLightExit;

    //    // 订阅 ButtonLight 的颜色变化事件
    //    ButtonLight.ButtonLightColorChanged += OnLightColorReceived;
    //}

    //private void OnDisable()
    //{
    //    // 取消订阅事件
    //    LightZone.IronBoxEntered -= HandleLightEnter;
    //    LightZone.IronBoxExited -= HandleLightExit;

    //    // 取消订阅 ButtonLight 的颜色变化事件
    //    ButtonLight.ButtonLightColorChanged -= OnLightColorReceived;
    //}

    private void HandleLightEnter(IronBox ironBox, Color lightColor)
    {
        if(ironBox == this && !ignoreLightChanges)// 确保事件只影响当前 IronBox 实例
        {
            isinlight = true;
            OnLightColorReceived(lightColor);
        }
    }

    private void HandleLightExit(IronBox ironBox)
    {
        if(ironBox == this && !ignoreLightChanges)// 确保事件只影响当前 IronBox 实例
        {
            isinlight = false;
            OnLightExitReceived();
        }
    }

    // 当接收到灯光颜色时调用
    private void OnLightColorReceived(Color lightColor)
    {
        if(ignoreLightChanges) return;  // 如果忽略光照变化，则直接返回
        // 根据光的颜色判断箱子的状态，忽略白光
        if (lightColor == Color.green)
        {
            SetBoxState(BoxState.Light);  // 绿色光，变轻
        }
        else if (lightColor == Color.red)
        {
            SetBoxState(BoxState.Large);  // 红色光，放大
        }
        else if (lightColor == Color.blue)
        {
            SetBoxState(BoxState.Frozen);  // 蓝色光，时停
        }
        else if (lightColor == Color.white)
        {
            SetBoxState(BoxState.Normal);  // 蓝色光，时停
        }
    }

    private void OnLightExitReceived()
    {
        if(ignoreLightChanges) return;  // 如果忽略光照变化，则直接返回
        // 离开光源时恢复到 Normal 状态
        isinlight = false;
        SetBoxState(BoxState.Normal);
        Debug.Log("OnLightExitReceived");
        //Debug.Log("铁箱离开光源，恢复正常状态");
    }

    private void OnLightEnterReceived(Color lightColor)
    {
        isinlight = true;
        Debug.Log("OnLightEnterReceived");
        OnLightColorReceived(lightColor);
    }

    // 设置铁箱的状态
    private void SetBoxState(BoxState newState)
    {
        currentState = newState;
        switch (newState)
        {
            case BoxState.Normal:
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                isInRed = false; // 常态，不能推动
                //Debug.Log("InRed = false");
                break;
            case BoxState.Light:
                //Debug.Log("绿光");
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;  // 变轻，可推动
                isInRed = false;
                break;
            case BoxState.Large:
                //Debug.Log("红光");
                isInRed = true;  // 标记进入红光
                break;
            case BoxState.Frozen:
                //Debug.Log("蓝色");
                rb.constraints = RigidbodyConstraints2D.FreezeAll | RigidbodyConstraints2D.FreezeRotation;
                isInRed = false;
                break;
        }
    }

    // 放大 redbox 的方法
    void ShrinkRedbox()
    {
        // 逐渐将 Ironbox 的大小放大到目标大小
        if (IronBoxInstance.transform.localScale.x < targetSize.x && IronBoxInstance.transform.localScale.y < targetSize.y)
        {
            // 使用 Lerp 进行平滑放大
            IronBoxInstance.transform.localScale = Vector3.Lerp(IronBoxInstance.transform.localScale, targetSize, shrinkSpeed * Time.deltaTime);
        }
    }

    // 恢复 redbox 原始大小的方法
    void ResetRedboxSize()
    {
        // 逐渐将 Ironbox 恢复到原始大小
        if (IronBoxInstance.transform.localScale.x > originalSize.x && IronBoxInstance.transform.localScale.y > originalSize.y)
        {
            // 使用 Lerp 进行平滑恢复
            IronBoxInstance.transform.localScale = Vector3.Lerp(IronBoxInstance.transform.localScale, originalSize, shrinkSpeed * Time.deltaTime);
        }
    }

    //重置铁箱到初始状态的方法
    public void ResetToInitialState()
    {
        IronBoxInstance.transform.localScale = originalSize;  // 恢复初始大小
        IronBoxInstance.transform.position = initialPosition; // 恢复初始位置
        SetBoxState(BoxState.Normal);  // 恢复常态
    }

    //设置是否忽略光的变化
    public void SetIgnoreLightChanges(bool ignore)
    {
        ignoreLightChanges = ignore;
    }

    public void ChangeBoxSprite(Sprite newSprite)
    {
        if (spriteRenderer != null && newSprite != null)
        {
            spriteRenderer.sprite = newSprite;
        }
    }
}

