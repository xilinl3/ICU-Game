using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronBox : MonoBehaviour
{
    private Rigidbody2D rb;

    public enum BoxState
    {
        Normal,  // 常态，不可被推动
        Light,   // 变轻，可被推动
        Small,   // 缩小状态
        Frozen   // 时停状态
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetBoxState(BoxState.Normal);  // 初始化为常态
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
            SetBoxState(BoxState.Small);  // 红色光，缩小
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
                rb.constraints = RigidbodyConstraints2D.FreezeAll;  // 常态，不能推动
                Debug.Log("IronBox is in Normal state.");
                break;
            case BoxState.Light:
                rb.constraints = RigidbodyConstraints2D.None;  // 变轻，可推动
                Debug.Log("IronBox is in Light state.");
                break;
            case BoxState.Small:
                transform.localScale = new Vector3(0.5f, 0.5f, 1f);  // 缩小
                Debug.Log("IronBox is in Small state.");
                break;
            case BoxState.Frozen:
                rb.constraints = RigidbodyConstraints2D.FreezeAll;  // 时停
                Debug.Log("IronBox is in Frozen state.");
                break;
        }
    }
}
