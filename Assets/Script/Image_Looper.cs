using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Image_Looper : MonoBehaviour
{
    public Image[] images; // 存放所有的图片
    public float displayTime = 2f; // 每张图片的显示时间

    private int currentIndex = 0; // 当前显示的图片索引
    private float timer = 0f; // 计时器

    void Start()
    {
        // 初始化，显示第一张图片
        DisplayImage(currentIndex);
    }

    void Update()
    {
        // 更新计时器
        timer += Time.deltaTime;

        // 如果计时器超过了显示时间，则显示下一张图片
        if (timer >= displayTime)
        {
            NextImage();
            timer = 0f; // 重置计时器
        }
    }

    // 显示下一张图片
    void NextImage()
    {
        // 隐藏当前图片
        images[currentIndex].gameObject.SetActive(false);

        // 更新索引，如果超出数组范围则回到第一张图片
        currentIndex = (currentIndex + 1) % images.Length;

        // 显示下一张图片
        DisplayImage(currentIndex);
    }

    // 显示指定索引的图片
    void DisplayImage(int index)
    {
        images[index].gameObject.SetActive(true);
    }
}
