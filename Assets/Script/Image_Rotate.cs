using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Image_Rotate : MonoBehaviour
{
    public float rotationSpeed = 50f; // 旋转速度

    void Update()
    {
        // 通过修改Transform的rotation.z实现Z轴旋转
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}

