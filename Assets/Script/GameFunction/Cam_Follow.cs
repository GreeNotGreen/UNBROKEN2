using UnityEngine;

public class Cam_Follow : MonoBehaviour
{
    public float FollowSpeed = 2f;
    private float oriSpeed;
    public Transform target;
    private TeleportAbility teleportAbility; // 对 TeleportAbility 脚本的引用

    private float initialYOffset; // 初始 Y 偏移量
    public float yOffsetMultiplier = 1f; // Y 偏移量乘数

    void Start()
    {
        initialYOffset = transform.position.y - target.position.y;
        // 获取 TeleportAbility 脚本的引用
        teleportAbility = target.GetComponent<TeleportAbility>();
        oriSpeed = FollowSpeed;
    }

    void LateUpdate()
    {
        if (target != null)
        {

            Vector3 newPos = new Vector3(target.position.x, target.position.y + initialYOffset * yOffsetMultiplier, -10f);

            transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);

            // 根据 isTeleporting 的值调整 FollowSpeed
            if (teleportAbility != null && teleportAbility.isTeleporting)
            {
                FollowSpeed = 10;
            }
            else
            {
                FollowSpeed = oriSpeed;
            }
        }
    }
}
