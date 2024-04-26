using UnityEngine;
using UnityEngine.UI;

public class BeatBar : MonoBehaviour
{
    public Scrollbar scrollbar;
    public float scrollSpeed = 1.0f;

    private bool scrollingRight = true;

    void Update()
    {
        // 如果 scrollbar 为 null 或者不可见，则返回
        if (scrollbar == null || !scrollbar.gameObject.activeInHierarchy)
            return;

        // 根据滚动方向更新 scrollbar 的 value
        if (scrollingRight)
        {
            scrollbar.value += Time.deltaTime * scrollSpeed;
            if (scrollbar.value >= 1.0f)
                scrollingRight = false;
        }
        else
        {
            scrollbar.value -= Time.deltaTime * scrollSpeed;
            if (scrollbar.value <= 0.0f)
                scrollingRight = true;
        }
    }
}
