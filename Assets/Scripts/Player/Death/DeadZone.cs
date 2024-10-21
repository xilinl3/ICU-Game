using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    [SerializeField] private Transform respawnPointTransform; // 复活点 Transform
    [SerializeField] private float respawnDelay = 2f; // 可调整的延迟时间

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
        // 等待指定的延迟时间
        yield return new WaitForSeconds(respawnDelay);

        // 将玩家传送到指定的复活点位置
        player.transform.position = respawnPointTransform.position;
        Debug.Log("玩家已传送到复活点：" + respawnPointTransform.position);
    }
}

