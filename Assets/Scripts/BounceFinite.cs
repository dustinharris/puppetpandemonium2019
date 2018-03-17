using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceFinite : MonoBehaviour
{

    public float duration = 1f;
    public float distance = 10f;
    public int Bounces = 2;
    public int bounceDampening = 2;


    // Use this for initialization
    void Start()
    {
        StartCoroutine(Bounce());
    }

    private IEnumerator Bounce()
    {
        float halfDuration = .5f * duration;

        for (int i = 0; i < Bounces; i++)
        {
            Vector3 start = transform.localPosition;
            Vector3 goal = new Vector3(start.x, start.y + distance, start.z);
            float startTime = Time.time;

            while (Time.time < startTime + halfDuration)
            {
                float t = (Time.time - startTime) / halfDuration;
                transform.localPosition = new Vector3(start.x, Mathf.SmoothStep(start.y, goal.y, t), start.z);

                yield return null;
            }

            startTime = Time.time;
            while (Time.time < startTime + halfDuration)
            {
                float t = (Time.time - startTime) / halfDuration;
                transform.localPosition = new Vector3(start.x, Mathf.SmoothStep(goal.y, start.y, t), start.z);

                yield return null;
            }

            distance = distance / bounceDampening;
        }
    }

    // Update is called once per frame
    void Update()
    {


    }
}
