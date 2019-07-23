using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public static readonly int gameOverScreenIndex = 2;
    private readonly int StartScreenIndex = 1;
    public InputField PlayerName;

    public void Play()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        if (SceneManager.GetActiveScene().buildIndex == gameOverScreenIndex)
        {
            nextScene = 1;
        } else
        {
            GameManager.playerName = PlayerName.text == string.Empty ? "Surfer" : PlayerName.text;
        }

        SceneManager.LoadScene(nextScene);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
