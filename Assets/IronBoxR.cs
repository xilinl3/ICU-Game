using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class IronBoxR : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject IronBoxInstance;
    public Vector3 targetSize = new Vector3(2f, 2f, 2f); // 目标大小
    public Vector3 originalSize;
    public float shrinkSpeed = 2f; // 缩小速度
    private Vector3 initialPosition; // 初始位置
    private bool isInRed = false;
    private SpriteRenderer spriteRenderer;

    public enum BoxState
    {
        Normal,  // 常态，不可被推动
        Light,   // 变轻，可被推动
        Large,   // 放大状态
        Frozen   // 时停状态
    }

    public BoxState currentState { get; private set; }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSize = IronBoxInstance.transform.localScale; // 初始化原始大小
        initialPosition = IronBoxInstance.transform.position; // 初始化初始位置
        SetBoxState(BoxState.Normal); // 初始化为常态
    }

    void Update()
    {
        //Debug.Log("Current IronBox State: " + currentState);
        // 如果 redbox 在光照范围内，则逐渐放大 redbox
        if (isInRed)
        {
            //Debug.Log("RedUpdate - 放大箱子");
            ShrinkRedbox();  // 这里应该放大箱子
        }
        else
        {
            // 当 redbox 离开光照范围时，逐渐恢复原始大小
            //Debug.Log("RedRest - 恢复箱子大小");
            ResetRedboxSize();
        }
    }

    // 设置铁箱的状态
    public void SetBoxState(BoxState newState)
    {
        currentState = newState;
        switch (newState)
        {
            case BoxState.Normal:
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                rb.gravityScale = 1f; // 恢复重力
                isInRed = false;
                //Debug.Log($"Switched to Normal state. IsInAir: {IsInAir()}");
                break;
            case BoxState.Light:
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                rb.gravityScale = 0.5f; // 轻量化的效果（示例）
                isInRed = false;
                break;
            case BoxState.Large:
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                isInRed = true;
                break;
            case BoxState.Frozen:
                rb.constraints = RigidbodyConstraints2D.FreezeAll; // 冻结所有移动和旋转
                rb.gravityScale = 0f; // 禁用重力
                isInRed = false;
                break;
        }
    }


    // 放大铁箱的方法（协程）
    void ShrinkRedbox()
    {
        // 逐渐将 Ironbox 的大小放大到目标大小
        if (IronBoxInstance.transform.localScale.x < targetSize.x && IronBoxInstance.transform.localScale.y < targetSize.y)
        {
            // 使用 Lerp 进行平滑放大
            IronBoxInstance.transform.localScale = Vector3.Lerp(IronBoxInstance.transform.localScale, targetSize, shrinkSpeed * Time.deltaTime);
        }
    }

    // 恢复 redbox 原始大小的方法
    void ResetRedboxSize()
    {
        // 逐渐将 Ironbox 恢复到原始大小
        if (IronBoxInstance.transform.localScale.x > originalSize.x && IronBoxInstance.transform.localScale.y > originalSize.y)
        {
            // 使用 Lerp 进行平滑恢复
            IronBoxInstance.transform.localScale = Vector3.Lerp(IronBoxInstance.transform.localScale, originalSize, shrinkSpeed * Time.deltaTime);
        }
    }
    // 返回当前状态的方法
    public BoxState GetCurrentState()
    {
        return currentState;
    }

    // 重置铁箱到初始位置和状态的方法
    public void ResetToInitialState()
    {
        IronBoxInstance.transform.localScale = originalSize;  // 恢复初始大小
        IronBoxInstance.transform.position = initialPosition; // 恢复初始位置
        SetBoxState(BoxState.Normal);  // 恢复常态
    }

    public void ChangeBoxSprite(Sprite newSprite)
    {
        if (spriteRenderer != null && newSprite != null)
        {
            spriteRenderer.sprite = newSprite;
        }
    }

    private bool IsInAir()
    {
        // 从箱子底部向下发射一条短射线，检测是否有地面
        float rayLength = 0.1f;
        Vector2 rayOrigin = new Vector2(transform.position.x, transform.position.y - 0.5f);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, rayLength);

        // 如果射线没有击中地面，则认为在空中
        return hit.collider == null;
    }
}

