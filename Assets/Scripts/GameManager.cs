using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;
    private int score;
    public static int highscore = 0;
    private bool routinCompleted = true;
    public TextMeshProUGUI destroyedtext;
    public GameObject rDef;
    public GameObject lDef;
    private DestroyOutOfBounds destroyOutOfBoundsL;
    private DestroyOutOfBounds destroyOutOfBoundsR;
    private int destroyedCount = 0;   
    public GameObject[] tipTexts;


    // Start is called before the first frame update
    void Start()
    {
        UpdateScore(0);       

        destroyOutOfBoundsR = rDef.GetComponent<DestroyOutOfBounds>();
        destroyOutOfBoundsL = lDef.GetComponent<DestroyOutOfBounds>();

        // SceneManager.LoadScene("MyMainMenu");

    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "SurvivalGoal")
        {
            CalculateHighscore();
        }
        

        if (routinCompleted)
        {
            StartCoroutine(AddScoreEverySecond(0.1f));
        }

        if (destroyOutOfBoundsR.destroyed)
        {
            destroyOutOfBoundsR.destroyed = false;

            destroyedCount += 1;
            if (destroyedCount > 2)
            {
                UpdateScore(50);
                destroyedtext.gameObject.SetActive(true);
                StartCoroutine(RemoveAfterSeconds(1, destroyedtext.gameObject));

            }
            
        }

        if (destroyOutOfBoundsL.destroyed)
        {
            destroyOutOfBoundsL.destroyed = false;

            destroyedCount += 1;
            if (destroyedCount > 2)
            {
                UpdateScore(50);
                destroyedtext.gameObject.SetActive(true);
                StartCoroutine(RemoveAfterSeconds(1, destroyedtext.gameObject));

            }

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

    IEnumerator AddScoreEverySecond(float seconds)
    {
        routinCompleted = false;
        yield return new WaitForSeconds(seconds);
        UpdateScore(1);
        routinCompleted = true;
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