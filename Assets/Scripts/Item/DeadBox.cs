using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBox : MonoBehaviour
{
    [SerializeField] private Transform IronTransform; // 铁箱子的复活点 Transform
    [SerializeField] private Transform WoodenTransform; // 木箱子的复活点 Transform（可选）
    [SerializeField] private float respawnDelay = 2f; // 可调整的延迟时间

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("IronBox") || collision.CompareTag("WoodenBox"))
        {
            // 开始协程，在延迟时间后传送箱子
            StartCoroutine(RespawnBoxAfterDelay(collision));
        }
    }

    private IEnumerator RespawnBoxAfterDelay(Collider2D box)
    {
        // 等待指定的延迟时间
        yield return new WaitForSeconds(respawnDelay);

        // 判断重生点是否存在
        Transform respawnPoint = null;
        if (box.CompareTag("IronBox") && IronTransform != null)
        {
            respawnPoint = IronTransform;
        }
        else if (box.CompareTag("WoodenBox") && WoodenTransform != null)
        {
            respawnPoint = WoodenTransform;
        }

        // 如果没有找到合适的重生点，则直接返回
        if (respawnPoint == null)
        {
            Debug.LogWarning($"No valid respawn point set for {box.tag} in {gameObject.name}.");
            yield break;
        }

        // 将箱子传送到指定的复活点位置
        box.transform.position = respawnPoint.position;

        // 获取箱子的 Rigidbody2D 组件并将速度设为零
        Rigidbody2D rb = box.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f; // 如果有旋转也将其清零
        }

        Debug.Log($"{box.tag} has been respawned to the point: {respawnPoint.position}");
    }
}
