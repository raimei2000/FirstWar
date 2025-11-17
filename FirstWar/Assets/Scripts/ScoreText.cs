using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    /// <summary>
    /// update score.
    /// </summary>
    /// <param name="delta">The player earns score as the monster's hp when monster died.</param>
    public void UpdateScore(int delta)
    {
        // 게임 종료시 점수 갱신 안 함.
        if (GameManager.Instance.isGameOver == false)
        {
            score += delta;
            scoreText.text = "Score: " + score.ToString();
        }
    }
}
