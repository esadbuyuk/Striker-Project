using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
   // public bool isGameActive;

   
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }      


    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("defender_feet_collider") || col.gameObject.CompareTag("goalkeeper"))
        {
            GameOverf();
            Debug.Log("Game Over!");
          //  stopBall = true;
        }
    }


    private void GameOverf()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        Time.timeScale = 0;
        // isGameActive = false;
    }
}

