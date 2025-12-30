using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUi : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void Show(int finalScore)
    {
        scoreText.text = "Skor: " + finalScore.ToString();
        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }



    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void MainMenu()
    {

    }
}
