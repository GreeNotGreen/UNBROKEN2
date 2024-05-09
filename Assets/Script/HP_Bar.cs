using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HP_Bar : MonoBehaviour
{
    private float _maxHealth = 100;
    private float _currentHealth;
    [SerializeField] private Image _hpBarFill;
    [SerializeField] private TextMeshProUGUI _hpText;
    [SerializeField] private float _fillSpeed;
    [SerializeField] private Gradient _color;

    //读取player hp
    [SerializeField] Health playerHP;

    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _maxHealth;
        _hpText.text = "HP : " + _currentHealth;
    }

    private void Update()
    {
        if (playerHP != null)
        {
            _currentHealth = playerHP.health;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
            _hpText.text = "HP : " + _currentHealth;
            UpdateHpBar();
        }
    }

    // 现在这个项目只用来读取 playerhp 所以不能用UpdateHP
    public void UpdateHP(float amount)
    {
        _currentHealth += amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        _hpText.text = "HP : " + _currentHealth;
        UpdateHpBar();
    }

    void UpdateHpBar()
    {
        float targetFillAmount = _currentHealth / _maxHealth;
        Color targetColor = _color.Evaluate(targetFillAmount);
        targetColor.a = 100f / 255f; // 将 alpha 值设置为 100，范围在 0 到 1 之间

        _hpBarFill.DOFillAmount(targetFillAmount, _fillSpeed);
        _hpBarFill.DOColor(targetColor, _fillSpeed);

    }
}
