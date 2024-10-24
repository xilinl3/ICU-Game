using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBox : MonoBehaviour
{
    [SerializeField] private Transform respawnPointTransform; // 复活点 Transform
    [SerializeField] private float respawnDelay = 2f; // 可调整的延迟时间

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("IronBox"))
        {
            // 开始协程，在延迟时间后传送箱子
            StartCoroutine(RespawnBoxAfterDelay(collision));
        }
    }

    private IEnumerator RespawnBoxAfterDelay(Collider2D box)
    {
        // 等待指定的延迟时间
        yield return new WaitForSeconds(respawnDelay);

        // 将箱子传送到指定的复活点位置
        box.transform.position = respawnPointTransform.position;

        // 获取箱子的 Rigidbody2D 组件并将速度设为零
        Rigidbody2D rb = box.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f; // 如果有旋转也将其清零
        }

        Debug.Log("IronBox has been respawned to the point: " + respawnPointTransform.position);
    }
}
