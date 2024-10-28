using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePlayer : MonoBehaviour
{
    private Rigidbody2D rb;
    private float stuckCheckTime = 2f; // 卡住检测时间间隔
    private Vector2 lastPosition;
    private bool isStuck;


    public void EnterMobile()
    {
         FindFirstObjectByType<PlayerReload>().UpdatePlayersRef(this);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastPosition = rb.position;
        StartCoroutine(CheckIfStuck()); // 开始卡住检测的协程
    }

    void Update()
    {
        // 允许玩家在任何时候按下"R"键来尝试脱离卡点
        if (Input.GetKeyDown(KeyCode.R))
        {
            // 在按下"R"键时手动检测是否卡住
            CheckIfStillStuck();
            if (isStuck)
            {
                UnstuckPlayer();
            }
        }
    }

    public void UICheck()
    {
        CheckIfStillStuck();
        if (isStuck)
        {
            UnstuckPlayer();
        }
    }

    // 定期检测玩家是否卡住
    private IEnumerator CheckIfStuck()
    {
        while (true)
        {
            yield return new WaitForSeconds(stuckCheckTime);
            if (Vector2.Distance(rb.position, lastPosition) < 0.1f && rb.velocity.magnitude < 0.1f)
            {
                isStuck = true;
                //Debug.Log("Player is stuck! Press 'R' to get unstuck.");
            }
            else
            {
                isStuck = false;
            }

            lastPosition = rb.position;
        }
    }

    // 脱离卡点的方法
    private void UnstuckPlayer()
    {
        // 尝试将玩家轻微向上或向一侧移动，避免再次卡住
        Vector2 unstuckPosition = new Vector2(rb.position.x, rb.position.y + 1f);
        rb.position = unstuckPosition;

        // 重置速度
        rb.velocity = Vector2.zero;
        isStuck = false;

        //Debug.Log("Player unstuck and repositioned.");
    }

    // 手动检测是否仍然卡住
    private void CheckIfStillStuck()
    {
        if (Vector2.Distance(rb.position, lastPosition) < 0.1f && rb.velocity.magnitude < 0.1f)
        {
            isStuck = true;
            //Debug.Log("Player is still stuck! Try moving to another location.");
        }
        else
        {
            isStuck = false;
            lastPosition = rb.position;
        }
    }
}

