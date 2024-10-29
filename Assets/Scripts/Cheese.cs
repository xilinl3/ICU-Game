using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheese : MonoBehaviour
{
    private AudioSource audioSource;
    private int initialChildCount;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        initialChildCount = transform.childCount; // 记录初始子对象数量
    }

    // Update is called once per frame
    void Update()
    {
        int currentChildCount = transform.childCount;

        // 检查子对象数量是否减少
        if (currentChildCount < initialChildCount)
        {
            audioSource.Play(); // 播放音效
            initialChildCount = currentChildCount; // 更新记录的子对象数量，避免重复播放
        }
    }
}

