using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LRInstructionsScript : MonoBehaviour {

    [SerializeField] private Text InstructionsText;
    [SerializeField] private Image BGImage;
    [SerializeField] [TextArea] private string StartInstructions;
    [SerializeField] [TextArea] private string DistractedInstructions;
    [SerializeField] private float ShowTime = 5f;

    private bool DistractedShown = false;

	void Awake () {
        Messenger.AddListener(GameEvent.COUNTDOWN_FINISHED, CountdownFinished);
        Messenger.AddListener(GameEvent.P1_CUBE_NEW_CANDY, CubeDestroyed);
        Messenger.AddListener(GameEvent.P2_CUBE_NEW_CANDY, CubeDestroyed);
	}

    private void ShowInstructions(bool shown)
    {
        InstructionsText.enabled = shown;
        BGImage.enabled = shown;
    }
	
	private void CountdownFinished()
    {
        InstructionsText.text = StartInstructions;
        ShowInstructions(true);
        StartCoroutine(WaitForReady());
    }

    private IEnumerator WaitForReady()
    {
        while (!Input.GetButton("RedPuppet") || !Input.GetButton("BluePuppet"))
        {
            yield return null;
        }

        ShowInstructions(false);
        Messenger.Broadcast(GameEvent.GAME_START);
    }

    private void CubeDestroyed()
    {
        if (!DistractedShown)
        {
            Messenger.Broadcast(GameEvent.REX_STOP_SCENERY);
            Time.timeScale = 0;
            DistractedShown = true;
            InstructionsText.text = DistractedInstructions;
            ShowInstructions(true);

            StartCoroutine(WaitToHide());
        }
    }

    private IEnumerator WaitToHide()
    {
        float now = Time.realtimeSinceStartup;

        // Can't use WaitForSeconds while timescale is 0
        yield return new WaitWhile(() => Time.realtimeSinceStartup < now + ShowTime);

        Time.timeScale = 1;
        ShowInstructions(false);
        Messenger.Broadcast(GameEvent.REX_START_SCENERY);
    }
}
