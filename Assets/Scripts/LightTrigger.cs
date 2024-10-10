using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 检查是否碰到的是玩家
        if (other.CompareTag("Player"))
        {
            // 向 Unity 控制台发送消息
            Debug.Log("player enter light");
        }
    }
}
