using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CountDownTimer : MonoBehaviour
{
    public float timeLeft = 0.0f;

    public float blinkTime = 2f;
    public float blinkInterval = .1f;

    Text text;

    void Awake()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft = Mathf.Max(timeLeft - Time.deltaTime, 0);
            text.text = "IMPACT " + timeLeft.ToString("00.000");

            if (timeLeft == 0)
            {
                StartCoroutine(Zero()); 
            }
        }
    }

    private IEnumerator Zero()
    {
        float blinkStartTime = Time.time;
        float blinkStopTime = blinkStartTime + blinkTime;

        while (Time.time < blinkStopTime)
        {
            // Hide car
            text.enabled = false;

            // Wait for blink interval
            yield return new WaitForSeconds(blinkInterval);

            // Show car
            text.enabled = true;

            // Wait for blink interval
            yield return new WaitForSeconds(blinkInterval);
        }

        // Afterwards, make sure car is visible
        text.enabled = true;

        Messenger.Broadcast(GameEvent.BOSS_GAME_OVER);
    }
}