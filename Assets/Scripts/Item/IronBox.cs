using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronBox : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isInRed = false;
    public GameObject IronBoxInstance;   
    public Vector3 targetSize = new Vector3(2f, 2f, 2f);  // 目标大小
    public Vector3 originalSize;  
    public float shrinkSpeed = 2f;  // 缩小速度

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
        SetBoxState(BoxState.Normal);  // 初始化为常态
    }

    void Update()
    {
        // 如果 redbox 在光照范围内，则逐渐放大 redbox
        if (isInRed)
        {
            Debug.Log("RedUpdate - 放大箱子");
            ShrinkRedbox();  // 这里应该放大箱子
        }
        else
        {
            // 当 redbox 离开光照范围时，逐渐恢复原始大小
            Debug.Log("RedRest - 恢复箱子大小");
            ResetRedboxSize();
        }
    }

    private void OnEnable()
    {
        // 订阅灯光进入和离开的事件
        LightZone.IronBoxEntered += OnLightColorReceived;
        LightZone.IronBoxExited += OnLightExitReceived;
    }

    private void OnDisable()
    {
        // 取消订阅事件
        LightZone.IronBoxEntered -= OnLightColorReceived;
        LightZone.IronBoxExited -= OnLightExitReceived;
    }

    // 当接收到灯光颜色时调用
    private void OnLightColorReceived(Color lightColor)
    {
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
    }

    // 当接收到离开灯光事件时调用
    private void OnLightExitReceived()
    {
        // 离开光源时恢复到 Normal 状态
        SetBoxState(BoxState.Normal);
        Debug.Log("铁箱离开光源，恢复正常状态");
    }

    // 设置铁箱的状态
    private void SetBoxState(BoxState newState)
    {
        switch (newState)
        {
            case BoxState.Normal:
                rb.constraints = RigidbodyConstraints2D.FreezePositionX;
                isInRed = false; // 常态，不能推动
                Debug.Log("InRed = false");
                break;
            case BoxState.Light:
                rb.constraints = RigidbodyConstraints2D.None;  // 变轻，可推动
                Debug.Log("IronBox is in Light state.");
                break;
            case BoxState.Large:
                Debug.Log("IronBox is in Large state.");
                isInRed = true;  // 标记进入红光
                break;
            case BoxState.Frozen:
                rb.constraints = RigidbodyConstraints2D.FreezeAll;  // 时停
                Debug.Log("IronBox is in Frozen state.");
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
}

