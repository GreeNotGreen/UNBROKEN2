using UnityEngine;

public class Pause : MonoBehaviour
{
    public float pauseTime = 0.1f;

    void Start()
    {
    }

    public void PauseGame()
    {
        // 将时间缩放设置为0，暂停游戏
        Time.timeScale = 0f;

        // 在 pauseTime 秒后恢复游戏
        Invoke("ResumeGame", pauseTime);
    }

    public void ResumeGame()
    {
        // 恢复游戏，将时间缩放设置回1
        Time.timeScale = 1f;
    }
}
