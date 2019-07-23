using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public AudioClip waterSplash;
    private AudioSource audioSource;
    public TimerManager timerManager;
    public Camera mainCamera;

    public static string playerName;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.Play();
    }

    private void OnEnable()
    {
        PlayerController.OnGameOver += GameOver;
    }

    private void OnDisable()
    {
        PlayerController.OnGameOver -= GameOver;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LoadGameOverAfterSeconds()
    {
        //Time.timeScale = 0.0f;
        Time.timeScale = 0.1f;
        float pauseEndTime = Time.realtimeSinceStartup + 2;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            //audioSource.volume -= 0.01f;
            if (mainCamera.orthographicSize > 30)
            {
                mainCamera.orthographicSize -= 1;
            }
            yield return 0;
        }
        Time.timeScale = 1;
        // timerManager.DoSlowMotion();
        // yield return new WaitForSeconds(1);
        SceneManager.LoadScene(StartMenu.gameOverScreenIndex);
    }

    void GameOver()
    {
        //audioSource.PlayOneShot(waterSplash);
        StartCoroutine(PlaySound());
        Highscores.AddNewHighScore(playerName, Scoring.PlayerHighscore);
        StartCoroutine(LoadGameOverAfterSeconds());
        //timerManager.DoSlowMotion();
    }

    IEnumerator PlaySound()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(waterSplash);
        yield return new WaitWhile(() => audioSource.isPlaying);
    }

}
