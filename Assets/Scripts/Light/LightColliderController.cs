using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal; // 引入Light2D的命名空间

public class LightColliderController : MonoBehaviour
{
    private Light2D spotLight;  // 自动获取Light2D组件
    private PolygonCollider2D polygonCollider;   // 引用PolygonCollider2D
    private float rotationAngle;
    private float angleOffset = 0f;  // 可调节的角度偏移量

    void Awake()
    {
        // 获取PolygonCollider2D组件
        polygonCollider = GetComponent<PolygonCollider2D>();
        // 自动获取Light2D组件
        spotLight = GetComponent<Light2D>();

        if (spotLight == null)
        {
            Debug.LogError("Light2D component not found on this GameObject. Please attach a Light2D component.");
            return; // 终止后续代码的执行
        }

        // 获取Z轴旋转角度
        rotationAngle = spotLight.transform.eulerAngles.z;
        if (rotationAngle > 180f)
        {
            rotationAngle -= 360f;
        }

        CalangleOffset();
    }

    void Start()
    {
        // 在游戏开始时一次性更新碰撞体形状
        UpdateColliderShape();
    }

    private void CalangleOffset()
    {
        if (rotationAngle < 0) { angleOffset = 90 - rotationAngle; }
        if (rotationAngle > 0) { angleOffset = 90 - rotationAngle; }
        if (rotationAngle == 0) { angleOffset = 90; }
    }

    private void UpdateColliderShape()
    {
        if (spotLight != null && polygonCollider != null)
        {
            // 获取灯光的Z轴旋转角度（只在Start中获取一次）
            float rotationAngle = spotLight.transform.eulerAngles.z;

            // 处理旋转角度大于180的情况，将其转换为负角度
            if (rotationAngle > 180f)
            {
                rotationAngle -= 360f;
            }

            // 根据灯光的Spot Angle和Outer Radius计算碰撞体的顶点
            float outerRadius = spotLight.pointLightOuterRadius;
            float spotAngle = spotLight.pointLightOuterAngle;  // 实时获取灯光的角度
            int numberOfPoints = 10;  // 你可以调整这个值以增加精度

            // 创建一个List来存储顶点
            List<Vector2> points = new List<Vector2>();
            points.Add(Vector2.zero);  // 添加灯光中心点作为第一个顶点

            // 计算每个点的角度和位置，应用Z轴的旋转角度并加上偏移量
            for (int i = 0; i <= numberOfPoints; i++)
            {
                // 计算每个点相对于Z轴的旋转角度
                float angle = -spotAngle / 2 + (spotAngle / numberOfPoints) * i;

                // 应用额外的偏移量（可调节）
                float radian = (angle + rotationAngle + angleOffset) * Mathf.Deg2Rad;
                Vector2 point = new Vector2(Mathf.Cos(radian) * outerRadius, Mathf.Sin(radian) * outerRadius);
                points.Add(point);
            }

            // 更新PolygonCollider2D的顶点
            polygonCollider.SetPath(0, points.ToArray());
        }
    }
}
