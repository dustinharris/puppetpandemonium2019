using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // Check to see if the user has pressed A in the last frame
        if (Input.GetKeyUp("a"))
        {
            Debug.Log("Pressed A");
            // If yes, move to next scene
            SceneManager.LoadScene(0);
        }
    }
}
