using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour {

    float score = 0;
    public GameObject scoreText;

    // Update is called once per frame
    void Update () {

        // Increment player score
        //playerScore += Time.deltaTime;

        // Display player score on screen
        scoreText.GetComponent<Text>().text = (((int)score)*10).ToString();

    }

    public void increaseScore(int amount)
    {
        score += amount;
    }
}
