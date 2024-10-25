using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPLightSp : MonoBehaviour
{
    [SerializeField] private Sprite LightOn;
    [SerializeField] private Sprite LightOff;

    [SerializeField] private GameObject TrackLight;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        // ??????? SpriteRenderer ??
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("This GameObject is missing a SpriteRenderer component.");
            return;
        }

        // ?????????
        UpdateSprite();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        // ?? TrackLight ? SetActive ????? Sprite
        if (TrackLight != null && TrackLight.activeSelf)
        {
            // ?? TrackLight ?????????? LightOn ??
            spriteRenderer.sprite = LightOn;
        }
        else
        {
            // ????? LightOff ??
            spriteRenderer.sprite = LightOff;
        }
    }
}

