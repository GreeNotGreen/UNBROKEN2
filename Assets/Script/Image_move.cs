using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Image_move : MonoBehaviour
{
    public float moveSpeedX = 1f; // 移动速度
    public float moveSpeedY = 1f; // 移动速度
    public float moveDistance = 10f; // 移动距离

    private RectTransform rectTransform;
    private Vector2 initialPosition;
    private float startTime;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        initialPosition = rectTransform.anchoredPosition; // 获取初始位置
        startTime = Time.time;
    }

    void Update()
    {
        // 计算当前时间与起始时间之间的差值
        float timeElapsed = Time.time - startTime;

        // 使用 Mathf.Sin 函数计算水平方向上的偏移量
        float xOffset = Mathf.Sin(timeElapsed * moveSpeedX) * moveDistance;

        // 使用 Mathf.Sin 函数计算垂直方向上的偏移量
        float yOffset = Mathf.Sin(timeElapsed * moveSpeedY) * moveDistance;

        // 添加初始位置作为偏移的起点
        Vector2 offsetPosition = initialPosition + new Vector2(xOffset, yOffset);

        // 更新图像的位置
        rectTransform.anchoredPosition = offsetPosition;
    }
}