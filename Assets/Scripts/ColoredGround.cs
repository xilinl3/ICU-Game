using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ColoredGround : MonoBehaviour
{
    private Renderer groundRenderer;
    private BoxCollider2D boxCollider2d;
    [SerializeField] private Color objectColor;
    [SerializeField] private Light2D trackedLight; // 追踪的灯光引用
    private float colorTolerance = 0.02f;

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

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Start");
        groundRenderer = GetComponent<Renderer>();
        boxCollider2d = GetComponent<BoxCollider2D>();
        objectColor = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if (trackedLight != null)
        {
            //Debug.Log("我的颜色是" + objectColor + "，灯光颜色为" + trackedLight.color);
            if (ColorsAreSimilar(objectColor, trackedLight.color, colorTolerance))
            {
                SwitchTo(false);
            }
            else
            {
                SwitchTo(true);
            }
        }
        else
        {
            SwitchTo(true); // 灯光引用为空时保持可见
        }
    }
}



