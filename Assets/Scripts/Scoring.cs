using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
    public static Scoring Instance { get; private set; } //Singleton
    public static int PlayerHighscore { get; private set; }

    public int score = 0;
    int multiplier = 0;
    readonly int pickup_score = 5;
    readonly int flip_score = 10;
    readonly int score_per_second = 1;
    public Text scoretxt;
    public Text multitxt;

    public Animator scoreAnimation;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        //else
        //{
        //    Destroy(gameObject);
        //}


    }

    void Start()
    {
        InvokeRepeating("ScoreOverTime", 0.0f, 1.0f);
        InvokeRepeating("IncreaseMultiplier", 0.0f, 20.0f);
    }

    private void OnEnable()
    {
        PlayerController.OnGameOver += GameOver;
        Fliper.OnFlips += ScoreOnFlip;
    }

    private void OnDisable()
    {
        PlayerController.OnGameOver -= GameOver;
        Fliper.OnFlips -= ScoreOnFlip;
    }

    void ScoreOverTime()
    {
        SetScore(score_per_second);
        //int new_score = score;
        //int score_calculated = score_per_second * multiplier;
        //new_score += score_calculated;
        //score = new_score;
        //scoretxt.text = "Score: " + score;
    }

    void IncreaseMultiplier()
    {
        int new_multiplier = multiplier;
        new_multiplier += 1;
        multiplier = new_multiplier;
        multitxt.text = "x" + multiplier;
    }

    public void ScorePickup()
    {
        SetScore(pickup_score);
        //int new_score = score;
        //int score_calculated = pickup_score * multiplier;
        //new_score += score_calculated;
        //score = new_score;
        //scoretxt.text = "Score: " + score;
    }

    private void ScoreOnFlip(bool isFlip)
    {
        Debug.Log("is flips: " + isFlip);
        if(isFlip)
        {
            SetScore(flip_score);
            scoreAnimation.SetBool("isFlip", true);
            Debug.Log(scoreAnimation.GetCurrentAnimatorStateInfo(0).IsName("scoreAnim"));
        }
        else
        {
            if(!scoreAnimation.GetCurrentAnimatorStateInfo(0).IsName("ScoreAnim"))
            {
                scoreAnimation.SetBool("isFlip", false);
            }
        }
    }

    private void SetScore(int multiplierP)
    {
        int new_score = score;
        int score_calculated = multiplierP * multiplier;
        new_score += score_calculated;
        score = new_score;
        scoretxt.text = "Score: " + score;
    }

    private void GameOver()
    {
        PlayerHighscore = score;
        CancelInvoke();
    }


}
