using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogBox : MonoBehaviour
{
    public Transform box;
    public CanvasGroup background;
    public float x = 0; 
    public float y = 0;

    private void OnEnable()
    {
        background.alpha = 0;
        background.LeanAlpha(1, 0.5f);

        box.localPosition = new Vector2(x, -Screen.height);
        box.LeanMoveLocalY(y, 0.5f).setEaseInOutExpo().delay = 0.1f;

    }

    public void CloseDialog()
    {
        background.LeanAlpha(0, 0.5f);
        box.LeanMoveLocalY(-Screen.height, 0.5f).setEaseInExpo().setOnComplete(OnComplete);
    }

    void OnComplete()
    {
        gameObject.SetActive(false);
    }
}
