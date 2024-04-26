using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStop : MonoBehaviour
{
    public float stopTime = 0.1f;

    bool waiting;

    public void Stop()
    {
        if (waiting)
            return;

        Time.timeScale = 0.0f;
        StartCoroutine(Wait(stopTime)); // 传递 stopTime 参数
    }

    IEnumerator Wait(float stopDuration) // 接受 stopTime 参数
    {
        waiting = true;
        yield return new WaitForSecondsRealtime(stopDuration); // 使用 WaitForSecondsRealtime
        Time.timeScale = 1.0f;
        waiting = false;
    }
}
