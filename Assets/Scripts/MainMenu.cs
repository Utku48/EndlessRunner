using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject _optionPanel;
    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Options()
    {
        _optionPanel.SetActive(true);
    }

    public void CloseBtn()
    {
        _optionPanel.SetActive(false);
    }

}
