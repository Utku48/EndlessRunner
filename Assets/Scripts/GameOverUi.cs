using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUi : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public Transform _restartButton;
    public Transform _menuButton;


    void Start()
    {
        gameObject.SetActive(false);
        ButtonAnim(_restartButton);
        ButtonAnim(_menuButton);
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

    void ButtonAnim(Transform btn)
    {
        btn.DOScale(1.1f, .5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine).SetUpdate(true); ;
    }


}
