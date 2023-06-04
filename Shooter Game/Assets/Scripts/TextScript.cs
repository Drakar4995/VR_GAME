using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour
{
    public Text ScoreText;
    public int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();
    }

    public void AddScore(int newscore)
    {
        score += newscore;
    }

    public void UpdateScore()
    {
        ScoreText.text = "Score: " + score;
    }
}
