using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextHint : MonoBehaviour
{
    [SerializeField] private GameObject panel; // 要显示和隐藏的 Panel
    [SerializeField] private float pauseDuration = 5f; // 暂停时长
    [SerializeField] public Rigidbody2D playerRigibody; // 玩家脚本

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(ShowPanelAndPause());
        }
    }

    private IEnumerator ShowPanelAndPause()
    {
        // 暂停游戏
        Time.timeScale = 0f;

        // 显示 Panel
        panel.SetActive(true);

        // 等待指定的暂停时长，但由于 Time.timeScale = 0，使用 unscaled time
        yield return new WaitForSecondsRealtime(pauseDuration);

        // 隐藏 Panel
        panel.SetActive(false);

        // 恢复游戏
        Time.timeScale = 1f;

        if (playerRigibody != null)
        {
            playerRigibody.velocity = new Vector2(playerRigibody.velocity.x, 0);
        }

        // 删除整个 GameObject，销毁触发器
        Destroy(gameObject);
    }
}
