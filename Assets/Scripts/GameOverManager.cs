using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public Text scoreText;
    public Text highscoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = Scoring.PlayerHighscore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //if(highscoreText.text == "fetching")
        //{

        //}
    }
}
