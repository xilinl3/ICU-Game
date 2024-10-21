using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ColoredGround : MonoBehaviour
{
    private Renderer groundRenderer;
    private BoxCollider2D boxCollider2d;
    [SerializeField] private Color objectColor;

    private int LightCounter = 0;
    private float colorTolerance = 0.02f;

    private void OnEnable()
    {
        // 订阅 ButtonLight 的颜色变化事件
        ButtonLight.ButtonLightColorChanged += LightColorChange;
    }

    private void OnDisable()
    {
        // 取消订阅 ButtonLight 的颜色变化事件
        ButtonLight.ButtonLightColorChanged -= LightColorChange;
    }

    void LightColorChange(Color newColor)
    {
        Debug.Log("检测到");
        // 处理颜色变化时的逻辑
        //if (ColorsAreSimilar(objectColor, newColor, colorTolerance))
        //{
        //    Debug.Log("颜色匹配，处理相应逻辑");
        //    // 可以根据需求实现其他逻辑，比如改变地面外观或属性
        //    LightCounter++;
        //}
        //else
        //{
        //    LightCounter--;
        //}

        //if (LightCounter > 0)
        //{
        //    SwitchTo(false);
        //}
        //else
        //{
        //    SwitchTo(true);
        //}
    }

    private void SwitchTo(bool state)
    {
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Light"))
        {
            Light2D light2DComponent = other.gameObject.GetComponent<Light2D>();
            if (!ColorsAreSimilar(objectColor, light2DComponent.color, colorTolerance)) { return; }

            LightCounter++;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Light"))
        {
            Debug.Log("检测到平台消失的光");
            Light2D light2DComponent = other.gameObject.GetComponent<Light2D>();
            if (!ColorsAreSimilar(objectColor, light2DComponent.color, colorTolerance)) { return; }

            LightCounter--;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        groundRenderer = GetComponent<Renderer>();
        boxCollider2d = GetComponent<BoxCollider2D>();

        objectColor = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if (LightCounter > 0)
        {
            SwitchTo(false);
        }
        else
        {
            SwitchTo(true);
        }
    }
}


