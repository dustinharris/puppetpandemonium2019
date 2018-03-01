using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrandmaCatLives : MonoBehaviour {

    public int StartingLives = 9;

    [SerializeField]
    private GameObject CatLivesTextObject;
    private Text CatLivesText;
    private int lives;


	// Use this for initialization
	void Start () {
        CatLivesText = CatLivesTextObject.GetComponent<Text>();
        lives = StartingLives;
        DisplayLives();    
    }

    public int GetCatLives()
    {
        return lives;
    }

    public void DecreaseCatLives()
    {
        if (lives > 0) {
            lives--;
        }

        DisplayLives();
    }

    private void DisplayLives()
    {
        CatLivesText.text = lives.ToString();
    }
}
