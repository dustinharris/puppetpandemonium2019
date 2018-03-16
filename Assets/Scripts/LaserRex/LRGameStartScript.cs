using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LRGameStartScript : MonoBehaviour {

    public int LogoLength = 3;

    [SerializeField]
    private GameObject Background;
    [SerializeField]
    private Text CountdownText;

    private int countdown = 3;

	void Awake () {
        StartCoroutine(Logo());
	}

    private IEnumerator Logo()
    {
        float now = Time.realtimeSinceStartup;

        yield return new WaitForSeconds(LogoLength);

        Background.SetActive(false);
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        CountdownText.enabled = true;
        for (int i = countdown; i > 0; i--)
        {
            CountdownText.text = i.ToString();
            yield return new WaitForSeconds(1);
        }

        CountdownText.text = "GO!";

        yield return new WaitForSeconds(1);

        CountdownText.enabled = false;
        CountdownFinished();
    }

    private void CountdownFinished()
    {
        
        Messenger.Broadcast(GameEvent.COUNTDOWN_FINISHED);
    }
}
