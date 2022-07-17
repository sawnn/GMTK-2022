using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public GameObject GameOverUI;
    public Text textScore; //scoreValue
    public Dice dice;

    void Start()
    {

    }

    public void GameOverScreen()
    {
        GameOverUI.SetActive(true);
        textScore.text = dice.score.ToString();
        Time.timeScale = 0f;
    }

    public void NewGame()
    {
        GameOverUI.SetActive(false);
        SceneManager.LoadScene(1); //Main Menu index
    }

    public void MainMenu()
    {
        GameOverUI.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(0); //Main Menu index
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
