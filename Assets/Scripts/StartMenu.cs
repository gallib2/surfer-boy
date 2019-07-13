using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    private readonly int gameOverScreenIndex = 2;
    private readonly int StartScreenIndex = 1;

    public void Play()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        if (SceneManager.GetActiveScene().buildIndex == gameOverScreenIndex)
        {
            nextScene = 1;
        }
        SceneManager.LoadScene(nextScene);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
