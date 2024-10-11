using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRotate : MonoBehaviour
{
    public float rotateAngle = 45f;  // 每次旋转的角度
    public float rotationSpeed = 100f;  // 旋转的速度
    public bool isClockwise = true;  // 控制是否顺时针旋转

    private bool rotating = false;  // 是否正在旋转
    private float targetAngle;      // 目标旋转角度

    void Update()
    {
        // 当按下空格键时，开始旋转
        if (Input.GetKeyDown(KeyCode.Space) && !rotating)
        {
            if (isClockwise)
            {
                StartRotation(rotateAngle);  // 顺时针旋转
            }
            else
            {
                StartRotation(-rotateAngle);  // 逆时针旋转
            }
        }

        // 如果正在旋转，逐步完成旋转
        if (rotating)
        {
            RotateObject();
        }
    }

    // 开始旋转，计算目标角度
    void StartRotation(float angle)
    {
        targetAngle = transform.eulerAngles.z + angle;
        rotating = true;
    }

    // 完成旋转到目标角度
    void RotateObject()
    {
        // 逐步旋转
        float currentAngle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, currentAngle));

        // 检查是否已经到达目标角度
        if (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.z, targetAngle)) < 0.1f)
        {
            rotating = false;  // 停止旋转
        }
    }
}
