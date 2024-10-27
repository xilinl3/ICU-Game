using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 引入SceneManager命名空间

public class CgTrigger : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 获取玩家收集的奶酪个数
            player_behaviors player = other.GetComponent<player_behaviors>();
            if (player != null)
            {
                int collectedCheese = player.GetChessNum(); // 获取玩家已收集的奶酪数量
                int totalCheese = player.GetTotalCheese(); // 获取玩家的奶酪总数量

                // 如果玩家收集的奶酪数量小于所需数量，切换到 EndingCg1 的场景
                if (collectedCheese < totalCheese)
                {
                    SceneManager.LoadScene("EndingCg1");
                }
                else
                {
                    SceneManager.LoadScene("EndingCg2");
                }
            }
        }

    }
}

