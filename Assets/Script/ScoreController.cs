using UnityEngine;

public class ScoreController : MonoBehaviour
{
    private bool scoredThisBeat = false;

    public void AddScoreOnce(int score)
    {
        if (!scoredThisBeat)
        {
            FindObjectOfType<ScoreManager>().AddScore(score);
            scoredThisBeat = true;
        }
    }

    public void ResetScoredStatus()
    {
        scoredThisBeat = false;
    }
}
