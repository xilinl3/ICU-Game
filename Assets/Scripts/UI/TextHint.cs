using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextHint : MonoBehaviour
{
    [SerializeField] private GameObject panel; // 要显示和隐藏的 Panel
    [SerializeField] private float pauseDuration = 5f; // 暂停时长
    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasTriggered) return;
        if (collision.CompareTag("Player"))
        {
            hasTriggered = true;
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
    }
}
