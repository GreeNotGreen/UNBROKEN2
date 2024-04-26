using UnityEngine;
using UnityEngine.UI;

public class SkillCD : MonoBehaviour
{
    [SerializeField] bool noCDmode = false; // debug use

    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private Image filledImage;

    [SerializeField] private bool canSkill = false;
    [SerializeField] public bool Skilled = false;
    [SerializeField] private float currentSkillValue = 0f;

    private bool getCombo = false; // 标志位，用于控制是否开始增加技能值
    private float targetSkillValue = 0f; // 目标技能值

    [SerializeField] private float skillIncreaseAmount = 5f; // 增加的技能值
    [SerializeField] private float skillDecreaseSpeed = 1f; // 减少速度

    private void Start()
    {
        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager reference is not set!");
            return;
        }
    }

    private void Update()
    {
        if(noCDmode)
        {
            currentSkillValue = 100f;
        }


        // 当获取 normal 分数的时候
        if (scoreManager.skillPlus && !canSkill)
        {
            getCombo = true;
            targetSkillValue = Mathf.Min(100f, currentSkillValue + skillIncreaseAmount);
            scoreManager.skillPlus = false;
            Debug.Log("normal");
        }
        // 当获取 double 分数的时候
        else if (scoreManager.skillPlusDouble && !canSkill)
        {
            getCombo = true;
            targetSkillValue = Mathf.Min(100f, currentSkillValue + skillIncreaseAmount * 2);
            scoreManager.skillPlusDouble = false;
            Debug.Log("double");
        }
        // 当获取 triple 分数的时候
        else if (scoreManager.skillPlusTriple && !canSkill)
        {
            getCombo = true;
            targetSkillValue = Mathf.Min(100f, currentSkillValue + skillIncreaseAmount * 3);
            scoreManager.skillPlusTriple = false;
            Debug.Log("triple");
        }


        // 如果开始增加技能值
        if (getCombo)
        {
            // 每帧逐渐增加技能值直到达到目标技能值
            currentSkillValue = Mathf.MoveTowards(currentSkillValue, targetSkillValue, skillIncreaseAmount * 20 * Time.deltaTime);

            // 如果达到目标技能值，则取消增加技能值的标志
            if (currentSkillValue >= targetSkillValue)
            {
                getCombo = false;
            }
        }

        // 当技能值达到最大值
        if (currentSkillValue >= 100f)
        {
            canSkill = true;
        }
        // 开始逐渐减少
        if (canSkill)
        {
            if(Skilled) //using skill
            {
                currentSkillValue = Mathf.Max(0f, currentSkillValue - skillDecreaseSpeed * Time.deltaTime);

                if (currentSkillValue <= 0)
                {
                    // ResetSkill
                    canSkill = false;
                    getCombo = false;
                    scoreManager.skillPlus = false;
                    scoreManager.skillPlusDouble = false;
                    scoreManager.skillPlusTriple = false;
                    Skilled = false; //end skill
                }
            }
        }
        else
        {
            currentSkillValue = Mathf.Max(0f, currentSkillValue - skillDecreaseSpeed / 10 * Time.deltaTime);
        }

        // 更新技能 CD 图标
        if (filledImage != null)
        {
            filledImage.fillAmount = currentSkillValue / 100f;
        }
    }

    // 用于外部检查技能是否可用的方法
    public bool CanUseSkill()
    {
        return canSkill;
    }
}
