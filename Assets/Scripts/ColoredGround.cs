using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ColoredGround : MonoBehaviour
{
    private Renderer groundRenderer;
    private BoxCollider2D boxCollider2d;
    [SerializeField] private Color objectColor;
    [SerializeField] private GameObject trackedLight; // 追踪的灯光引用
    private Light2D trackedLightComponent; // 追踪的灯光组件
    private float colorTolerance = 0.02f;
    private bool isInTrigger = false; // 标志位，记录是否有光源在触发范围内

    private void SwitchTo(bool state)
    {
        Debug.Log("正在切换平台 " + state);
        groundRenderer.enabled = state;
        boxCollider2d.enabled = state;
    }

    private bool ColorsAreSimilar(Color color1, Color color2, float tolerance)
    {
        return Mathf.Abs(color1.r - color2.r) < tolerance &&
               Mathf.Abs(color1.g - color2.g) < tolerance &&
               Mathf.Abs(color1.b - color2.b) < tolerance &&
               Mathf.Abs(color1.a - color2.a) < tolerance;
    }

    void Start()
    {
        groundRenderer = GetComponent<Renderer>();
        boxCollider2d = GetComponent<BoxCollider2D>();
        objectColor = GetComponent<SpriteRenderer>().color;

        // 从 trackedLight 获取 Light2D 组件
        if (trackedLight != null)
        {
            trackedLightComponent = trackedLight.GetComponent<Light2D>();
        }
    }

    void Update()
    {
        // 只有在没有进入任何 PedalLight 或 NormalLight 的触发器时，才根据 ButtonLight 切换状态
        if (!isInTrigger && trackedLightComponent != null && trackedLight.CompareTag("ButtonLight"))
        {
            if (ColorsAreSimilar(objectColor, trackedLightComponent.color, colorTolerance))
            {
                SwitchTo(false);
            }
            else
            {
                SwitchTo(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PedalLight") || other.CompareTag("NormalLight"))
        {
            Light2D enteringLight = other.GetComponent<Light2D>();
            if (enteringLight == null) return;

            Debug.Log("进入触发器的物体：" + other.gameObject.name + "，标签：" + other.gameObject.tag);
            Debug.Log(enteringLight.color);
            Debug.Log(objectColor);
            Debug.Log(ColorsAreSimilar(objectColor, enteringLight.color, colorTolerance));

            isInTrigger = true; // 进入触发器时标志位置为 true
            if (ColorsAreSimilar(objectColor, enteringLight.color, colorTolerance))
            {
                SwitchTo(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PedalLight") || other.CompareTag("NormalLight"))
        {
            Light2D exitingLight = other.GetComponent<Light2D>();
            if (exitingLight == null) return;

            if (ColorsAreSimilar(objectColor, exitingLight.color, colorTolerance))
            {
                SwitchTo(true);
            }

            isInTrigger = false; // 离开触发器时标志位置为 false
        }
    }
}
