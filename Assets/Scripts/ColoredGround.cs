using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ColoredGround : MonoBehaviour
{
    private Renderer groundRenderer;  // 将 renderer 重命名为 groundRenderer
    private BoxCollider2D boxCollider2d;

    [SerializeField] private Color objectColor;

    private int LightCounter = 0;
    private float colorTolerance = 0.02f;

    private void SwitchTo(bool state)
    {
        groundRenderer.enabled = state;  // 使用 groundRenderer
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
            Light2D light2DComponent = other.gameObject.GetComponent<Light2D>();
            if (!ColorsAreSimilar(objectColor, light2DComponent.color, colorTolerance)) { return; }

            LightCounter--;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        groundRenderer = GetComponent<Renderer>();  // 修改为 groundRenderer
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

