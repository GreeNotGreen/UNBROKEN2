using UnityEngine;

public class PressedStart : MonoBehaviour
{
    public KeyCode startKey;
    [SerializeField] private GameObject targetObject;
    [SerializeField] private AudioSource audioObject;

    // Update is called once per frame
    void Update()
    {
        // 检查是否按下了指定按键
        if (Input.GetKeyDown(startKey))
        {
            // 启用目标游戏对象
            if (targetObject != null)
            {
                targetObject.SetActive(true);
            }

            // 检查音频源是否存在且准备好播放
            if (audioObject.clip != null)// && audioObject.clip.loadState == AudioDataLoadState.Loaded)
            {
                // 播放音频
                audioObject.Play();
                Debug.Log("play");
            }
        }
    }
}
