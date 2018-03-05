using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EKO2YGameOver : MonoBehaviour {

    [SerializeField] private GameObject gameOverBackground;
    [SerializeField] private GameObject blueCharacterObject;
    [SerializeField] private GameObject redCharacterObject;
    [SerializeField] private GameObject gameOverEmpty;
    [SerializeField] private GameObject audienceBar;
    [SerializeField] private GameObject enemiesEmpty;
    private Vector3 blueCharacterEndGamePosition;
    private Vector3 redCharacterEndGamePosition;
    private Vector3 target;
    [SerializeField] private float timeToReachTarget = 3000f;
    [SerializeField] private GameObject inGameScoreText;
    [SerializeField] private GameObject SceneSwitcherObject;
    private SceneSwitcher sceneSwitcher;

    private void Awake()
    {
        // Listen for the Game Over message, then start game over sequence
        Messenger.AddListener(GameEvent.EKO2Y_GAME_OVER, StartGameOver);
        sceneSwitcher = SceneSwitcherObject.GetComponent<SceneSwitcher>();
    }

    // Use this for initialization
    void Start () {
        target = new Vector3(0, 0, gameOverBackground.transform.position.z);
        blueCharacterEndGamePosition = new Vector3(3.7f, 0, 0);
        redCharacterEndGamePosition = new Vector3(2.4f, 0, 0);
    }
	
	// Update is called once per frame
	void Update () {
	}

    private void StartGameOver()
    {
        // Hide Blue score text
        inGameScoreText.GetComponent<Text>().enabled = false;

        // Disable falling images
        GameObject redFall = GameObject.Find("[Red_Kawaii_Character_Falling]");
        redFall.SetActive(false);
        GameObject blueFall = GameObject.Find("[Blue_Kawaii_Character_Falling]");
        blueFall.SetActive(false);

        // Move background in
        StartCoroutine(MoveOverSeconds(gameOverBackground, target, timeToReachTarget));

        // Move blue character in
        StartCoroutine(MoveInCharacters());

        // Move red character in
        StartCoroutine(TurnOnText());

        // Go to next scene
        StartCoroutine(NextScene());
    }

    public IEnumerator MoveOverSeconds(GameObject objectToMove, Vector3 end, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.position;
        while (elapsedTime < (seconds/2))
        {
            objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            //Debug.Log("Elapsed time: " + elapsedTime);
            yield return new WaitForEndOfFrame();
        }
        //objectToMove.transform.position = new Vector3 (0, 0, 0);
    }

    IEnumerator MoveInCharacters()
    {
        //Debug.Log("Starting to Move Characters");
        // Wait 5 seconds before starting player animations
        yield return new WaitForSeconds(1.5f);
        //Debug.Log("Finished waiting 5 seconds");

        // Delete enemies empty
        enemiesEmpty.SetActive(false);

        // Move blue character in
        StartCoroutine(MoveOverSeconds(blueCharacterObject, blueCharacterEndGamePosition, timeToReachTarget));

        // Move red character in
        StartCoroutine(MoveOverSeconds(redCharacterObject, redCharacterEndGamePosition, timeToReachTarget));
    }

    IEnumerator TurnOnText()
    {
        // disable audience bar
        audienceBar.SetActive(false);

        //Debug.Log("Starting to Show Text");
        // Wait 5 seconds before starting player animations
        yield return new WaitForSeconds(3.1f);
        //Debug.Log("Finished rendering text");

        gameOverEmpty.GetComponentInChildren<Text>().enabled = true;

        // Update Score Screen Text with final score
        gameOverEmpty.GetComponentInChildren<Text>().text = inGameScoreText.GetComponent<Text>().text;

       StartCoroutine(BlinkTextInChild(gameOverEmpty, 3));
    }

    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(8.1f);
        sceneSwitcher.result = SceneSwitcher.Result.NA;
        sceneSwitcher.SwitchScenes();
    }

    IEnumerator BlinkTextInChild(GameObject blinkee, float delay)
    {
        int totalBlinkTimes = 10;

        // Enable double jump
        for (int i = 0; i < totalBlinkTimes; i++)
        {
            if (i % 2 == 0)
            {
                // Even -- blink on
                blinkee.GetComponentInChildren<Text>().enabled = true;
            }
            if (i % 2 == 1)
            {
                // Odd - blink off
                blinkee.GetComponentInChildren<Text>().enabled = false;

            }
            // Delay until next blink time
            yield return new WaitForSeconds(.5f);
        }
    }

}
