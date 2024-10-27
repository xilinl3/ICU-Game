using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    [SerializeField] private Transform respawnPointTransform; // 复活点 Transform
    [SerializeField] private float respawnDelay = 2f; // 可调整的延迟时间
    [SerializeField] public GameObject BlackScreen; // 黑屏对象
    [SerializeField] private AudioClip deathSound; // 死亡音效

    private AudioSource audioSource;

    void Start()
    {
        BlackScreen.SetActive(false);

        // 获取或添加 AudioSource 组件
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // 将死亡音效分配给 AudioSource
        if (deathSound != null)
        {
            audioSource.clip = deathSound;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 开始协程，在延迟时间后传送玩家
            StartCoroutine(RespawnPlayerAfterDelay(collision));
        }
    }

    private IEnumerator RespawnPlayerAfterDelay(Collider2D player)
    {
        // 播放死亡音效
        if (audioSource != null && deathSound != null)
        {
            audioSource.Play();
        }

        BlackScreen.SetActive(true); // 显示黑屏
        yield return new WaitForSeconds(respawnDelay);

        // 重置玩家的速度
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            playerRb.velocity = Vector2.zero; // 重置速度
        }

        // 将玩家传送到指定的复活点位置
        player.transform.position = respawnPointTransform.position;

        BlackScreen.SetActive(false); // 隐藏黑屏
        //Debug.Log("玩家已传送到复活点：" + respawnPointTransform.position);
    }

}


