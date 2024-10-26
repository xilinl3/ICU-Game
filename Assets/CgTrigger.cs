using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 引入SceneManager命名空间

public class CgTrigger : MonoBehaviour
{
    [SerializeField] private int cheesecheck;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 获取玩家的棋子数量
            int cheeseget = other.GetComponent<player_behaviors>().GetChessNum();
            if (cheeseget < cheesecheck)
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
