using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BoxInLight : MonoBehaviour
{
    private List<GameObject> gameObjectList; // 用于存储进入的游戏对象
    private Light2D lightComponent; // 存储 Light2D 组件
    [SerializeField] private Sprite BoxWhite;
    [SerializeField] private Sprite BoxRed;
    [SerializeField] private Sprite BoxGreen;
    [SerializeField] private Sprite BoxBlue;

    void Start()
    {
        // 初始化列表，确保它不会为空
        if (gameObjectList == null)
        {
            gameObjectList = new List<GameObject>();
        }

        // 获取灯光的 Light2D 组件
        lightComponent = GetComponent<Light2D>();
        if (lightComponent == null)
        {
            Debug.LogError("未找到 Light2D 组件，请确保该脚本挂载在拥有 Light2D 组件的对象上。");
        }
    }

    void Update()
    {
        // 输出当前光的颜色
        if (lightComponent != null)
        {
            //Debug.Log("Current Light Color: " + lightComponent.color);
            ChangeBoxState();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 检查进入触发器的对象是否是 "IronBox"
        if (other.gameObject.tag == "IronBox")
        {
            // 检查这个对象是否已经在列表中，防止重复添加
            if (!gameObjectList.Contains(other.gameObject))
            {
                gameObjectList.Add(other.gameObject);  // 将对象添加到列表中
                //Debug.Log(other.gameObject.name + " Add to list ");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // 检查离开触发器的对象是否是 "IronBox"
        if (other.gameObject.tag == "IronBox")
        {
            if (gameObjectList.Contains(other.gameObject))
            {
                gameObjectList.Remove(other.gameObject);  // 将对象从列表中移除
                //Debug.Log(other.gameObject.name + " Remove from list");

                // 获取离开的 IronBoxR 脚本
                IronBoxR ironBoxScript = other.GetComponent<IronBoxR>();
                if (ironBoxScript != null)
                {
                    // 将状态重置为 Normal，并更改箱子的图片
                    ironBoxScript.SetBoxState(IronBoxR.BoxState.Normal);
                    ironBoxScript.ChangeBoxSprite(BoxWhite);
                    //Debug.Log("IronBox state reset to Normal and size reset to original");
                }
            }
        }
    }

    private void ChangeBoxState()
    {
        // 检查当前灯光颜色，根据颜色切换状态
        foreach (GameObject box in gameObjectList)
        {
            IronBoxR ironBox = box.GetComponent<IronBoxR>();
            if (ironBox != null)
            {
                // 根据灯光的颜色来设置铁箱的状态和图片
                if (lightComponent.color == Color.green)
                {
                    ironBox.SetBoxState(IronBoxR.BoxState.Light); // 绿色光，变轻
                    ironBox.ChangeBoxSprite(BoxGreen); // 切换到绿色箱子图片
                }
                else if (lightComponent.color == Color.red)
                {
                    ironBox.SetBoxState(IronBoxR.BoxState.Large); // 红色光，放大
                    ironBox.ChangeBoxSprite(BoxRed); // 切换到红色箱子图片
                }
                else if (lightComponent.color == Color.blue)
                {
                    ironBox.SetBoxState(IronBoxR.BoxState.Frozen); // 蓝色光，时停
                    ironBox.ChangeBoxSprite(BoxBlue); // 切换到蓝色箱子图片
                }
                else
                {
                    ironBox.SetBoxState(IronBoxR.BoxState.Normal); // 其他颜色，恢复常态
                    ironBox.ChangeBoxSprite(BoxWhite); // 切换到白色箱子图片
                }
                //Debug.Log($"Changed {box.name} state to {ironBox.GetCurrentState()}");
            }
        }
    }
}

