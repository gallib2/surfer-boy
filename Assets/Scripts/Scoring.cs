using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
    public static Scoring Instance { get; private set; } //Singleton

    int score = 0;
    int multiplier = 0;
    int pickup_score = 5;
    int score_per_second = 1;
    public Text scoretxt;
    public Text multitxt;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    void Start()
    {
        InvokeRepeating("ScoreOverTime", 0.0f, 1.0f);
        InvokeRepeating("IncreaseMultiplier", 0.0f, 20.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void ScoreOverTime()
    {
        int new_score = score;
        int score_calculated = score_per_second * multiplier;
        new_score += score_calculated;
        score = new_score;
        scoretxt.text = "Score: " + score;
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
        int new_score = score;
        int score_calculated = pickup_score * multiplier;
        new_score += score_calculated;
        score = new_score;
        scoretxt.text = "Score: " + score;
    }
    
}
