using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour {

    float score = 0;
    public GameObject scoreText;
    private Text text;

    private void Start()
    {
        text = scoreText.GetComponent<Text>();
        text.text = "0";
    }

    public void increaseScore(int amount)
    {
        score += amount;
        text.text = (((int)score) * 10).ToString();
    }
}
