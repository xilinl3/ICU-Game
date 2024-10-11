using UnityEngine;

public class DrawBox : MonoBehaviour
{
    public Vector3 boxSize = new Vector3(5, 5, 5); // 方框大小
    public GameObject childObject; // 子对象

    void OnDrawGizmos()
    {
        // 设置Gizmos颜色
        Gizmos.color = Color.green;

        // 绘制一个方框
        Gizmos.DrawWireCube(transform.position, boxSize);

        // 将子对象放在方框中心
        if (childObject != null)
        {
            childObject.transform.position = transform.position;
        }
    }
}
