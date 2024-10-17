using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonRangeDetector : MonoBehaviour
{
    private bool isPlayerInRange = false;  // 标记玩家是否在按钮范围内

    void OnTriggerEnter2D(Collider2D other)
    {
        // 如果玩家进入按钮范围
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // 如果玩家离开按钮范围
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    // 返回玩家是否在范围内
    public bool IsPlayerInRange()
    {
        return isPlayerInRange;
    }
}
