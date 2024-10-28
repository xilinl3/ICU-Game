using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YesButton : MonoBehaviour
{
    [SerializeField] private GameObject currentPanel; // 当前页面
    [SerializeField] private GameObject nextPanel; // 要打开的页面

    // 点击按钮时调用此方法
    public void OnYesButtonClick()
    {
        if (currentPanel != null)
        {
            currentPanel.SetActive(false); // 关闭当前页面
        }

        if (nextPanel != null)
        {
            nextPanel.SetActive(true); // 打开新的页面
        }

        // 获取带有 "Player" 标签的对象
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            // 尝试获取玩家对象上的 Fevent 脚本并调用 EnterMobile 方法
            Fevent fevent = player.GetComponent<Fevent>();
            if (fevent != null)
            {
                fevent.EnterMobile();
            }

            // 尝试获取玩家对象上的 Player_behaviors 脚本并调用 EnterMobile 方法
            player_behaviors playerBehaviors = player.GetComponent<player_behaviors>();
            if (playerBehaviors != null)
            {
                playerBehaviors.EnterMobile();
            }

            // 尝试获取玩家对象上的 SavePlayer 脚本并调用 EnterMobile 方法
            SavePlayer savePlayer = player.GetComponent<SavePlayer>();
            if (savePlayer != null)
            {
                savePlayer.EnterMobile();
            }
        }
    }
}
