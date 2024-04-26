using UnityEngine;

public class TimeScaler : MonoBehaviour
{
    public float scaleFactor = 0.5f; // 时间缩放因子，1 表示正常速度，小于 1 表示变慢，大于 1 表示加快

    void Update()
    {
        Time.timeScale = scaleFactor;
    }
}
