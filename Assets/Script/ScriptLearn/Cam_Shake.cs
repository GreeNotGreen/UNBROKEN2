﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam_Shake : MonoBehaviour
{
    private PlayerAttack2 playerAttack;

    // 抖动的幅度
    public float shakeAmount = 0.1f;
    // 抖动的持续时间
    public float shakeDuration = 0.5f;

    // 记录初始相机位置
    private Vector3 originalPosition;

    void Start()
    {
        // 获取场景中的 PlayerAttack2 脚本的实例
        playerAttack = FindObjectOfType<PlayerAttack2>();
    }

    void Update()
    {
        // 检查玩家的攻击状态，并在攻击时执行屏幕抖动
        if (playerAttack != null && playerAttack.attacking)
        {
            ShakeScreen();
        }
    }

    void ShakeScreen()
    {
        // 记录当前相机位置作为抖动的起始位置
        originalPosition = transform.position;

        // 生成一个随机的偏移量
        Vector3 shakeOffset = Random.insideUnitSphere * shakeAmount;

        // 将相机位置设置为起始位置加上偏移量
        transform.position = originalPosition + shakeOffset;

        // 在指定的持续时间后恢复相机位置
        Invoke("ResetPosition", shakeDuration);
    }

    // 恢复相机位置的方法
    void ResetPosition()
    {
        // 将相机位置恢复为起始位置
        transform.position = originalPosition;
    }
}
