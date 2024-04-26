using UnityEngine;

public class MousePosition : MonoBehaviour
{
    void Update()
    {
        // 获取鼠标在屏幕上的位置
        Vector2 mouseScreenPosition = Input.mousePosition;

        // 获取鼠标在世界坐标中的位置
        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

        // 将鼠标位置应用到物体的位置上
        transform.position = mouseWorldPosition;
    }
}
