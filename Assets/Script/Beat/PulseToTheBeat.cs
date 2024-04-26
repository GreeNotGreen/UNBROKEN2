using System.Collections;
using UnityEngine;

public class PulseToTheBeat : MonoBehaviour
{
    //for priority
    [SerializeField] int priority = 0; // 
    [SerializeField] float hitdelay = 0.1f; // 
    //

    //[SerializeField] KeyCode HitKey = KeyCode.Return; // 触发瞬移的按键
    //[SerializeField] int HitButton = 1; // 触发瞬移的按键

    [SerializeField] bool _OnBeat;
    [SerializeField] bool _OnPressed;

    public bool _Scored;
    [SerializeField] bool _CheckHit;
    [SerializeField] float _pulseSize = 1.15f;
    [SerializeField] float _returnSpeed = 5f;
    private Vector2 _startSize;

    void Start()
    {
        _startSize = transform.localScale;
    }


    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector2.Lerp(transform.localScale, _startSize, Time.deltaTime * _returnSpeed);

        if (_CheckHit)
        {
            // 检查 PlayerAttack2 实例是否存在
            PlayerAttack2 playerAttack = FindObjectOfType<PlayerAttack2>();
            if (playerAttack != null && playerAttack._OnHit)
            {
                _OnPressed = true;
                StartCoroutine(ResetOnPressed());
            }

            // 这里判断 OnBeat ( 可以拉长）   跟 OnPressed(可以提前）   
            if (_OnBeat && _OnPressed)
            {
                if (playerAttack != null)
                {
                    playerAttack._OnHit = false;
                }
                _OnPressed = false;
                _Scored = true;
            }
        }

        if (_Scored)
        {
            FindObjectOfType<ScoreManager>().GetScore(priority);
            _Scored = false;
        }
    }

    public void Pulse()
    {
        transform.localScale = _startSize * _pulseSize;

        GameObject Beat2 = GameObject.Find("Beat_2"); //Beat_2 (Object Name)
        GameObject Beat4 = GameObject.Find("Beat_4"); //Beat_4 (Object Name)

        // 检查是否成功获取了对象的引用
        if (Beat2 != null && Beat4 != null)
        {
            if (Beat4.GetComponent<PulseToTheBeat>()._OnBeat || Beat2.GetComponent<PulseToTheBeat>()._OnBeat)
            {
                _OnBeat = false;
            }
            else
            {
                _OnBeat = true;
            }
        }

        StartCoroutine(ResetOnBeat());
    }

    public void Pulse2()
    {
        transform.localScale = _startSize * _pulseSize;

        GameObject Beat4 = GameObject.Find("Beat_4"); //Beat_4 (Object Name)

        if (Beat4 != null)
        {
            if (Beat4.GetComponent<PulseToTheBeat>()._OnBeat)
            {
                _OnBeat = false;
            }
            else
            {
                _OnBeat = true;
            }
        }
        StartCoroutine(ResetOnBeat());
    }

    public void Pulse4()
    {
        transform.localScale = _startSize * _pulseSize;
        _OnBeat = true;
        StartCoroutine(ResetOnBeat());
    }

    IEnumerator ResetOnBeat()
    {
        yield return new WaitForSeconds(0.1f);
        _OnBeat = false;
    }

    IEnumerator ResetOnPressed()
    {
        yield return new WaitForSeconds(hitdelay);
        _OnPressed = false;
    }
}
