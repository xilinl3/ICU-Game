using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DeadZone : MonoBehaviour
{
    [SerializeField] private Transform respawnPointTransform; // 复活点 Transform
    [SerializeField] private float respawnDelay = 2f; // 可调整的延迟时间
    [SerializeField] public GameObject BlackScreen; // 黑屏对象
    [SerializeField] private Light2D Light;
    [SerializeField] private Color initialLightColor = Color.white;

    void Start()
    {
        BlackScreen.SetActive(false);

        Light = GameObject.Find("DisplayLight").GetComponent<Light2D>();
        initialLightColor = Light.color;
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
        BlackScreen.SetActive(true);// 显示黑屏
        player.transform.position = respawnPointTransform.position;// 将玩家传送到指定的复活点位置
        // 等待指定的延迟时间
        yield return new WaitForSeconds(respawnDelay);

        Light.color = initialLightColor; // 重置灯光颜色

        IronBox[] ironBoxes = FindObjectsOfType<IronBox>();// 获取所有的箱子
        foreach (IronBox ironBox in ironBoxes)
        {
            ironBox.SetIgnoreLightChanges(true);// 忽略光照变化
            ironBox.ResetToInitialState();// 重置所有箱子的状态
        }

        ButtonLight buttonLight = FindObjectOfType<ButtonLight>();// 获取按钮灯光
        if(buttonLight != null)
        {
            buttonLight.ResetColor();// 重置按钮灯光的状态
        }

        foreach (IronBox ironBox in ironBoxes)
        {
            ironBox.SetIgnoreLightChanges(false);// 不再忽略光照变化
        }

        BlackScreen.SetActive(false);// 隐藏黑屏
        Debug.Log("玩家已传送到复活点：" + respawnPointTransform.position);
    }
}

