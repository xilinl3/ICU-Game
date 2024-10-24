using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ButtonLightSprite : MonoBehaviour
{
    [SerializeField] private Sprite LightWhite;
    [SerializeField] private Sprite LightRed;
    [SerializeField] private Sprite LightGreen;
    [SerializeField] private Sprite LightBlue;
    [SerializeField] private GameObject TrackLight;
    private Light2D TrackLight2DComponent;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        TrackLight2DComponent = TrackLight.GetComponent<Light2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Assuming this script is on the GameObject with the SpriteRenderer you want to change
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSpriteBasedOnLightColor();
    }

    private void UpdateSpriteBasedOnLightColor()
    {
        if (TrackLight2DComponent == null || spriteRenderer == null)
        {
            Debug.LogWarning("Missing components for changing the sprite based on light color.");
            return;
        }

        // Check the color of the tracked light and change the sprite accordingly
        Color lightColor = TrackLight2DComponent.color;

        if (lightColor == Color.white)
        {
            spriteRenderer.sprite = LightWhite;
        }
        else if (lightColor == Color.red)
        {
            spriteRenderer.sprite = LightRed;
        }
        else if (lightColor == Color.green)
        {
            spriteRenderer.sprite = LightGreen;
        }
        else if (lightColor == Color.blue)
        {
            spriteRenderer.sprite = LightBlue;
        }
        else
        {
            spriteRenderer.sprite = LightWhite;
        }
    }
}
