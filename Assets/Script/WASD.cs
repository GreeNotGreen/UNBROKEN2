using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASD : MonoBehaviour
{
    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;

    public float moveSpeed = 5f;

    void Update()
    {
        float horizontalInput = 0f;
        float verticalInput = 0f;

        if (Input.GetKey(up))
            verticalInput = 1f;
        if (Input.GetKey(down))
            verticalInput = -1f;
        if (Input.GetKey(left))
            horizontalInput = -1f;
        if (Input.GetKey(right))
            horizontalInput = 1f;

        //calculate
        Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0f).normalized;
        //move
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}

