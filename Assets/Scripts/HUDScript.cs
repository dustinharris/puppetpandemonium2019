using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour {

    float player1Score = 0;
    float player2Score = 0;
    public GameObject player1ScoreText;
    public GameObject player2ScoreText;

    // Update is called once per frame
    void Update () {

        // Increment player score
        //playerScore += Time.deltaTime;

        // Display player score on screen
        player1ScoreText.GetComponent<Text>().text = (((int)player1Score)*10).ToString();
        player2ScoreText.GetComponent<Text>().text = (((int)player2Score) * 10).ToString();

    }

    public void player1IncreaseScore
        (int amount)
    {
        player1Score += amount;
    }

    public void player2IncreaseScore
        (int amount)
    {
        player2Score += amount;
    }
}
