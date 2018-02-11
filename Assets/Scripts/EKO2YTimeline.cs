using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EKO2YTimeline : MonoBehaviour {

    [SerializeField] private GameObject timeline;
    [SerializeField] private int gameLength;
    [SerializeField] private GameObject scoreScreen;
    [SerializeField] private GameObject gameScore;
    private float timelineObjectLength;
    private float newX;
    private Vector3 startingLocation;
    private bool gameOngoing;

	// Use this for initialization
	void Start () {
        // Get the width of the timeline object
        timelineObjectLength = timeline.GetComponent<SpriteRenderer>().bounds.size.x;
        //Debug.Log("length: " + timelineObjectLength);
        startingLocation = transform.position;
        startingLocation = new Vector3(startingLocation.x, startingLocation.y, startingLocation.z);
        // At the start, the game is ongoing
        gameOngoing = true;
        // Don't render score screen
        scoreScreen.GetComponentInChildren<Renderer>().enabled = false;
        scoreScreen.GetComponentInChildren<Text>().enabled = false;
    }

    // Update is called once per frame
    void Update () {
        if (gameOngoing) {
            // Every millisecond, move this GameObject closer to the end of the timeline
            newX = ((((Time.time * 1000) / gameLength) * timelineObjectLength) / timelineObjectLength) / 2;
            transform.position = new Vector3(startingLocation.x + newX, startingLocation.y, (startingLocation.z));
            if (newX >= timelineObjectLength)
            {
                // The game has reached an end
                gameOngoing = false;
                showEKO2YScoreScreen();
            }
        }
	}

    private void showEKO2YScoreScreen()
    {
        // Show background & Text
        scoreScreen.GetComponentInChildren<Renderer>().enabled = true;
        scoreScreen.GetComponentInChildren<Text>().enabled = true;

        // Update Score Screen Text with final score
        scoreScreen.GetComponentInChildren<Text>().text = gameScore.GetComponent<Text>().text;
    }
}
