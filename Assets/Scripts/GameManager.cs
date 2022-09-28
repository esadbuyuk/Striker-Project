using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI highscoreText;
    private int score;
    public static int highscore = 0;
    [SerializeField]
    private TextMeshProUGUI destroyedtext;
    [SerializeField]
    private GameObject rDef;
    [SerializeField]
    private GameObject lDef;
    private int destroyedCount = 0;
    [SerializeField]
    private GameObject[] tipTexts;
    private GameObject timerManager;
    private Coroutine coroutine;
    [SerializeField]
    private TextMeshProUGUI gameOverText;
    [SerializeField]
    private Button restartButton;
    [SerializeField]
    private TextMeshProUGUI nutmegtext;
    [SerializeField]
    private TextMeshProUGUI goaltext;

    // Start is called before the first frame update


    private void Awake()
    {
        timerManager = GameObject.Find("TimerManager");
        coroutine = timerManager.GetComponent<Coroutine>();
    }
    void Start()
    {
        UpdateScore(0);
        StartCoroutine(AddScoreEverySecond(0.1f)); // kontrol edilmedi.
        // SceneManager.LoadScene("MyMainMenu");
        // coroutine.StartFunctionTimer(UpdateScore(1), 1); // dont know why tis dont work.        

    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "SurvivalGoal")
        {
            CalculateHighscore();
        }
              
        if (SceneManager.GetActiveScene().name == "SurvivalTutorial")
        {
            if (Time.timeScale == 0)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    Time.timeScale = 1;

                    foreach (GameObject tipText in tipTexts)
                    {
                        tipText.SetActive(false);
                    }
                }
            }
        }
    }

    public void DefenderDestroyed() // kontrol edilmedi.
    {
        destroyedCount += 1;
        if (destroyedCount > 2)
        {
            UpdateScore(50);
            destroyedtext.gameObject.SetActive(true);
            StartCoroutine(RemoveAfterSeconds(1, destroyedtext.gameObject));
        }
    }    

    public void Nutmeg()
    {
        UpdateScore(50);
        nutmegtext.gameObject.SetActive(true);
        StartCoroutine(RemoveAfterSeconds(1, nutmegtext.gameObject)); // 0.0f de�i�tirilebilir!
    }

    public void Goal()
    {
        UpdateScore(500);
        goaltext.gameObject.SetActive(true);
        StartCoroutine(RemoveAfterSeconds(1.5f, goaltext.gameObject));

        // daha sonradan oyun zorlaştıkça sadece savunmacıların hızı artıcak
        if (Time.timeScale < 2)
        {
            Time.timeScale += 0.2f;
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void CalculateHighscore()
    {
        if (score > highscore)
        {
            highscore = score;            
        }
        highscoreText.text = "Highscore: " + highscore;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;                
    }

    public void ResetGame()
    {
        Time.timeScale = 1;
    }

    public void ContinueGame()
    {        
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        highscoreText.gameObject.SetActive(true);

        Time.timeScale = 0;
    }

    IEnumerator AddScoreEverySecond(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        UpdateScore(1);
        StartCoroutine(AddScoreEverySecond(0.1f));
    }

    public IEnumerator RemoveAfterSeconds(float seconds, GameObject obj)
    {
        if (obj.activeInHierarchy)
        {
            yield return new WaitForSeconds(seconds);
            obj.SetActive(false);
        }        
    }
}