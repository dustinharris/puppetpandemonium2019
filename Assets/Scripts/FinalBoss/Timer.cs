using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {


    public interface ITimerSubscriber {
        void Timeout();
    }

    public float TimeLimit;
    [SerializeField]
    private ITimerSubscriber[] subscribers;

    private Text TimerText;


	// Use this for initialization
	void Start () {
        TimerText = GetComponent<Text>();

        StartCoroutine(Countdown());
	}
	
    private IEnumerator Countdown()
    {
        while (TimeLimit > 0)
        {
            TimeLimit -= Time.deltaTime;
            TimerText.text = ToString(TimeLimit);
            yield return null;
        }

        Timeout();
    }

    private void Timeout()
    {
        for (int i = 0; i < subscribers.Length; i++)
        {
            subscribers[i].Timeout();
        }
    }

    private string ToString(float time)
    {
        return time.ToString("00.000");
    }
}
