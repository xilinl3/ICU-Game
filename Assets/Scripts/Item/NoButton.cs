using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoButton : MonoBehaviour
{
    [SerializeField] private GameObject currentPanel; // 当前页面

    // 点击按钮时调用此方法
    public void OnNoButtonClick()
    {
        if (currentPanel != null)
        {
            currentPanel.SetActive(false); // 关闭当前页面
        }
    }
}