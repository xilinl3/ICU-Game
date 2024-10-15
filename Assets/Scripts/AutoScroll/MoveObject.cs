using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MoveObject : MonoBehaviour
{
    public Scrollbar scrollbar;  // 滚动条
    public float WaitingTime;  //结束等待时间
    private Transform objectToMove; // 需要移动的GameObject
    private Vector3 startPosition;  // 起始位置
    private Vector3 endPosition;    // 目标位置
    private bool isAtDestination = false; // 是否到达目标位置

    void Start()
    {
        objectToMove = GetComponent<Transform>(); // 获取 GameObject 的 Transform 组件
        // 初始化起始位置和目标位置
        startPosition = objectToMove.position;
        endPosition = startPosition + new Vector3(17.67f, 0, 0);
    }

    void Update()
    {
        // 将 GameObject 的位置与 Scrollbar 的值同步
        float progress = scrollbar.value; // Scrollbar 的值为 0-1 范围
        objectToMove.position = Vector3.Lerp(startPosition, endPosition, 1 - progress); // Lerp 插值

        if(scrollbar.value <= 0f && !isAtDestination)
        {
            isAtDestination = true;
            StartCoroutine(ReturnToMainMenu());
        }
    }

    IEnumerator ReturnToMainMenu()
    {
        yield return new WaitForSeconds(WaitingTime);
        SceneManager.LoadScene(0);
    }

}


