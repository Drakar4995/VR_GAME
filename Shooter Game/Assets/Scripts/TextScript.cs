using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextScript : MonoBehaviour
{
    public static TextScript textScript;
    public AudioSource audio;
    public Text ScoreText;
    public int score = 0;
    public int lifes = 3;

    private bool check = false;
    // Start is called before the first frame update

    private void Awake()
    {
        textScript = this;
    }
    void Start()
    {
        score = 0;
        UpdateScore();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();
        checkChickensAlive();
    }

    public void AddScore(int newscore)
    {
        score += newscore;
    }

    public void UpdateScore()
    {
        ScoreText.text = "Score: " + score;
    }

    public void AddLifes(int newlife)
    {
        if (lifes > 0)
        {
        lifes += newlife;
        }
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
