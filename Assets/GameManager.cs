using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TimerManager timerManager;
    public Camera mainCamera;

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
            if (mainCamera.orthographicSize > 30)
            {
                mainCamera.orthographicSize -= 1;
            }
            yield return 0;
        }
        Time.timeScale = 1;
        // timerManager.DoSlowMotion();
        // yield return new WaitForSeconds(1);
        SceneManager.LoadScene(0);
    }

    void GameOver()
    {
        StartCoroutine(LoadGameOverAfterSeconds());
        //timerManager.DoSlowMotion();
    }
}
