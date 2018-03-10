using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRDrift : MonoBehaviour {

    public float XDistance;
    public float XSpeed;
    public float YDistance;
    public float YSpeed;

    public float RandomMin = .01f;
    public float RandomMax = 0.1f;

    private float XRandomizer;
    private float YRandomizer;



    private bool stopped = false;

    private Vector3 StartPosition;

    private Vector3 ResumedPosition;
    private float ResumedTime;

	void Start () {

        XRandomizer = Random.Range(RandomMin, RandomMax);
        YRandomizer = Random.Range(RandomMin, RandomMax);
        StartPosition = transform.localPosition;
    }

    void Update () {
		if (!stopped)
        {
            float XDrift = XRandomizer * XDistance * Mathf.Sin(XSpeed * Time.time);
            float newX = StartPosition.x + (XDrift * XSpeed);

            float YDrift = YRandomizer * YDistance * Mathf.Sin(YSpeed * Time.time);
            float newY = StartPosition.y + (YDrift * YSpeed);

            transform.localPosition = new Vector3(newX, newY, StartPosition.z);
        }
	}

    public void Stop()
    {
        stopped = true;
    }

    public void Resume()
    {
        ResumedPosition = transform.localPosition;
        ResumedTime = Time.time;
        StartCoroutine(GoToStart());
    }

    private IEnumerator GoToStart()
    {
        while (transform.localPosition != StartPosition)
        {
            float xt = (Time.time - ResumedTime) / XSpeed;
            float yt = (Time.time - ResumedTime) / YSpeed;
            transform.position = new Vector3(Mathf.SmoothStep(ResumedPosition.x, StartPosition.x, xt), Mathf.SmoothStep(ResumedPosition.y, StartPosition.y, yt), StartPosition.z);
            yield return null;
        }

        stopped = false;
    }
}
