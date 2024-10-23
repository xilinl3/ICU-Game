using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBoxControl: MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug 进入的物体的名字和标签
        Debug.Log("Object entered: " + other.gameObject.name + " with tag: " + other.gameObject.tag);
    }

    // 当有物体离开灯光范围时调用
    private void OnTriggerExit2D(Collider2D other)
    {
        // Debug 离开的物体的名字和标签
        Debug.Log("Object exited: " + other.gameObject.name + " with tag: " + other.gameObject.tag);    
    }
}
