using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // 总分
    private int totalScore = 0;

    // 当前 Combo 计时器
    private float comboTimer = 0f;

    // 最高 Combo
    private int highestCombo = 0;

    // 当前 Combo
    private int currentCombo = 0;

    // Combo 计时器的初始值
    public float comboTimerMax = 3f;

    // 引用 UI Text 对象
    [SerializeField] private Text scoreText;
    [SerializeField] private Text comboText;
    [SerializeField] private Text highestComboText;
    [SerializeField] private Text beatGetText;

    // for priority
    bool beat4 = false;
    bool beat2 = false;
    bool beat1 = false;
    bool beathalf = false;
    [SerializeField] int scorebeat4 = 1000;
    [SerializeField] int scorebeat2 = 100;
    [SerializeField] int scorebeat1 = 10;
    [SerializeField] int scorebeathalf = 1;

    // for SkillUse
    public bool skillPlus = false;
    public bool skillPlusDouble = false; //双倍积分 for skill
    public bool skillPlusTriple = false; //三倍积分 for skill

    // for latestBeatGet && combo
    private int latestBeatGet = 0;
    private int latestComboGet = 0;
    private Coroutine fadeOutCoroutine;
    private Coroutine fadeOutCoroutineCombo;

    // 初始的文本透明度
    private float initialAlpha = 1f;

    void Start()
    {

        // Initialize LeanTween with a larger number of spaces
        LeanTween.init(1000); // Adjust the number based on your requirements

        // Ensure beatGetText is properly assigned
        if (beatGetText == null)
        {
            Debug.LogError("beatGetText is not assigned in the inspector!");
        }

        // 初始化 UI Text 对象的显示
        UpdateUIText();
        initialAlpha = beatGetText.color.a; // 记录初始透明度
    }

    void Update()
    {

        // for score
        if (beat4 || beat2 || beat1 || beathalf)
        {
            if (beat4)
            {
                latestBeatGet = 4;
                beat4 = false;
                beat2 = false;
                beat1 = false;
                beathalf = false;
                AddScore(scorebeat4);

                skillPlusTriple = true;  //for Skill Use
            }
            else if (beat2)
            {
                latestBeatGet = 2;
                beat2 = false;
                beat1 = false;
                beathalf = false;
                AddScore(scorebeat2);
                skillPlusDouble = true;
            }
            else if (beat1)
            {
                latestBeatGet = 1;
                beat1 = false;
                beathalf = false;
                AddScore(scorebeat1);
                skillPlus = true;
            }
            else if (beathalf)
            {
                beathalf = false;
                AddScore(scorebeathalf);
            }


            // for text beatGet
            if (fadeOutCoroutine != null)
            {
                StopCoroutine(fadeOutCoroutine);
            }
            // 启动新的淡出协程
            fadeOutCoroutine = StartCoroutine(FadeInOutText(beatGetText));
        }

        // 更新 Combo 计时器
        if (comboTimer > 0f)
        {

            if (currentCombo > highestCombo)
            {
                highestCombo = currentCombo;
            }

            comboTimer -= Time.deltaTime;
            if (comboTimer <= 0f)
            {
                // Combo 计时器结束时处理 Combo
                currentCombo = 0;
            }
            
        }

        // 更新 UI Text 对象的显示
        UpdateUIText();
    }


    // 更新 UI Text 对象的显示
    private void UpdateUIText()
    {
        scoreText.text = "Score: " + totalScore.ToString();
        //comboText.text = "Combo: " + currentCombo.ToString();
        highestComboText.text = "Highest Combo: " + highestCombo.ToString();

        // 如果 latestBeatGet 不为 0，则显示最新的拿到的分数，否则显示为空白
        if (latestBeatGet != 0 )
        {
            beatGetText.text = latestBeatGet.ToString();
        }
        else
        {
            beatGetText.text = "";
        }

        if (latestComboGet != 0)
        {
            comboText.text = currentCombo.ToString();
        }
        else
        {
            comboText.text = "";
        }
    }

    // 增加得分
    public void GetScore(int priority)
    {
        if (priority == 4)
        {
            beat4 = true;
        }
        else if (priority == 3)
        {
            beat2 = true;
        }
        else if (priority == 2)
        {
            beat1 = true;
        }
        else if (priority == 1)
        {
            beathalf = true;
        }
    }

    public void AddScore(int amount)
    {
        totalScore += amount;
        // 每次增加得分时，增加当前 Combo
        currentCombo++;
        // 重置 Combo 计时器
        comboTimer = comboTimerMax;

        if (currentCombo > 1)
        {
            //for text ComboGet
            latestComboGet = 1;
            if (fadeOutCoroutineCombo != null)
            {
                StopCoroutine(fadeOutCoroutineCombo);
            }
            // 启动新的淡出协程
            if (currentCombo >= 0)
                fadeOutCoroutineCombo = StartCoroutine(FadeInOutCombo(comboText));
        }

    }

    // 获取当前总分
    public int GetTotalScore()
    {
        return totalScore;
    }

    // 获取当前 Combo
    public int GetCurrentCombo()
    {
        return currentCombo;
    }

    // 获取最高 Combo
    public int GetHighestCombo()
    {
        return highestCombo;
    }

    // 文本渐隐效果的协程
    IEnumerator FadeInOutText(Text text)
    {
        Debug.Log("FadeIn");
        // Reset the scale to 1
        text.rectTransform.localScale = Vector3.one / 2;

        // Stop any ongoing animations on the text object
        LeanTween.cancel(text.gameObject, false);

        // Fade in effect
        LeanTween.alphaText(text.rectTransform, 1f, 0.1f);
        LeanTween.scale(text.rectTransform, Vector3.one * 1.5f, 0.1f); 
       
        // Wait for the fade in effect to complete     0.1f ^ + 0.1^
        yield return new WaitForSeconds(0.2f); // 等待足够长的时间确保淡入效果完成
        
        // Wait for 0.5 second before starting the fade out effect
        yield return new WaitForSeconds(0.5f);
        // Stop any ongoing animations on the text object
        LeanTween.cancel(text.gameObject, false);

        // Fade out effect
        LeanTween.alphaText(text.rectTransform, 0f, 0.3f);
        LeanTween.scale(text.rectTransform, Vector3.one, 0.3f);
        Debug.Log("FadeOut");

        // Wait for the fade out effect to complete
        yield return new WaitForSeconds(0.3f);

        // Reset latestBeatGet
        latestBeatGet = 0;
    }


    //for comboText Use
    IEnumerator FadeInOutCombo(Text text)
    {
        Debug.Log("FadeIn");
        // Reset the scale to 1
        text.rectTransform.localScale = Vector3.one / 2;

        // Stop any ongoing animations on the text object
        LeanTween.cancel(text.gameObject, false);

        // Fade in effect
        LeanTween.alphaText(text.rectTransform, 1f, 0.05f);
        LeanTween.scale(text.rectTransform, Vector3.one * 1.5f, 0.05f);

        // Wait for the fade in effect to complete     0.1f ^ + 0.1^
        yield return new WaitForSeconds(0.1f); // 等待足够长的时间确保淡入效果完成

        // Wait for 0.5 second before starting the fade out effect
        yield return new WaitForSeconds(1.3f);
        // Stop any ongoing animations on the text object
        LeanTween.cancel(text.gameObject, false);

        // Fade out effect
        LeanTween.alphaText(text.rectTransform, 0f, 0.1f);
        LeanTween.scale(text.rectTransform, Vector3.one, 0.1f);
        Debug.Log("FadeOut");

        // Wait for the fade out effect to complete
        yield return new WaitForSeconds(0.1f);

        // Reset latestBeatGet
        latestComboGet = 0;
    }

}
