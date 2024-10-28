using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Fevent : MonoBehaviour
{
    public static event Action OnFKeyPressed;

    public void EnterMobile()
    {
        FindFirstObjectByType<UIInteraction>().UpdatePlayersRef(this);
    }

    void Update()
    {
        // 检测玩家是否按下了 F 键
        if (Input.GetKeyDown(KeyCode.F))
        {
            // 触发事件，通知所有订阅者
            if (OnFKeyPressed != null)
            {
                OnFKeyPressed.Invoke();
            }
        }
    }

    public void UIInteraction()
    {
        if (OnFKeyPressed != null)
        {
            OnFKeyPressed.Invoke();
        }
    }
}
