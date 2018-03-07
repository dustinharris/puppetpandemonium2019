using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartScript : MonoBehaviour {

    
    public interface ISubscribe
    {
        void CountdownFinished();
    }

    public int LogoLength = 3;

    [SerializeField]
    private GameObject Background;
    [SerializeField]
    private Text CountdownText;

    [SerializeField]
    MonoBehaviour[] Subscribers;

    private int countdown = 3;

	// Use this for initialization
	void Awake () {
        Time.timeScale = 0;

        StartCoroutine(Logo());
	}

    private IEnumerator Logo()
    {
        float now = Time.realtimeSinceStartup;

        // Can't use WaitForSeconds while timescale is 0
        yield return new WaitWhile(() => Time.realtimeSinceStartup < now + LogoLength);

        Background.SetActive(false);
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        CountdownText.enabled = true;
        float now;
        for (int i = 3; i > 0; i--)
        {
            CountdownText.text = i.ToString();
            now = Time.realtimeSinceStartup;
            yield return new WaitWhile(() => Time.realtimeSinceStartup < now + 1);
        }

        CountdownText.text = "GO!";
        Time.timeScale = 1;

        yield return new WaitForSeconds(1);

        CountdownText.enabled = false;
        CountdownFinished();
    }

    private void CountdownFinished()
    {
        for (int i = 0; i < Subscribers.Length; i++)
        {
            if (Subscribers[i] is ISubscribe)
            {
                ((ISubscribe)Subscribers[i]).CountdownFinished();
            }
        }
    }
}
