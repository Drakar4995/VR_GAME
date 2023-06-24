using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextScript : MonoBehaviour
{
    public static TextScript textScript;
    public Text pointstoShowText;
    public AudioSource audio;
    public Text ScoreText;
    public int score = 0;
    private int pointsToShow;

    private bool check = false;
    // Start is called before the first frame update

    private void Awake()
    {
        textScript = this;
    }
    void Start()
    {
        score = 0;
        pointstoShowText.text = "";
        UpdateScore();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();
        checkChickensAlive();
    }

    public void PointsToShow(int points)
    {
        pointstoShowText.text = "+" + points;
        Invoke("ResetPoints",0.5f);
    }

    void ResetPoints()
    {
        pointstoShowText.text = "";
    }
    public void AddScore(int newscore)
    {
        score += newscore;
        PointsToShow(newscore);
    }

    public void UpdateScore()
    {
        ScoreText.text = "Score: " + score;
    }

    public void checkChickensAlive()
    {

        if (GameObject.FindWithTag("Chicken") == null && !check)
        {
            audio.Stop();
            GameManager.Instance.intValueToPass = score;
            SceneManager.LoadScene("EndScene", LoadSceneMode.Single);
            check = true;
        }
    }
}
