using UnityEngine;
using UnityEngine.EventSystems;

public class TouchAreaController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject targetObject; // 需要控制状态的对象
    public float delayTime = 0.5f; // 延迟时间

    private bool isPointerOver = false; // 记录鼠标或触摸是否在触摸区域上

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerOver = true;
        Invoke("ToggleTargetObject", delayTime);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerOver = false;
        CancelInvoke("ToggleTargetObject");
        targetObject.SetActive(false);
    }

    private void ToggleTargetObject()
    {
        targetObject.SetActive(isPointerOver);
    }
}
