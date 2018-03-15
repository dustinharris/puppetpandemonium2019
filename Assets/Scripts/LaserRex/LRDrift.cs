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
    private float driftTime;

	void Start ()
    {
        XRandomizer = Random.Range(RandomMin, RandomMax);
        YRandomizer = Random.Range(RandomMin, RandomMax);
        StartPosition = transform.localPosition;
        driftTime = Time.time;
    }

    void Update () {
		if (!stopped)
        {
            driftTime += Time.deltaTime;
            float XDrift = XRandomizer * XDistance * Mathf.Sin(XSpeed * (driftTime));
            float newX = StartPosition.x + (XDrift * XSpeed);

            float YDrift = YRandomizer * YDistance * Mathf.Sin(YSpeed * (driftTime));
            float newY = StartPosition.y + (YDrift * YSpeed);

            transform.localPosition = new Vector3(newX, newY, this.transform.localPosition.z);
        }
	}

    public void SetPosition(Vector3 pos)
    {
        StartPosition = pos;
    }

    public void Stop()
    {
        stopped = true;
    }

    public void Resume()
    {
        stopped = false;
    }
}
