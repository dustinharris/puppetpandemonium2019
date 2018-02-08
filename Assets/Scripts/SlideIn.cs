using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideIn : MonoBehaviour {


    [SerializeField]
    public Vector3 StartPosition;
    public Vector3 EndPosition;
    public float Duration = 1;
    public float Delay = 0;
    private float StartTime;

	// Use this for initialization
	void Start () {
        transform.localPosition = StartPosition;


        StartTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        float t = (Time.time - StartTime) / Duration;
        transform.localPosition = new Vector3(
            Mathf.SmoothStep(StartPosition.x, EndPosition.x, t),
            Mathf.SmoothStep(StartPosition.y, EndPosition.y, t),
            Mathf.SmoothStep(StartPosition.z, EndPosition.z, t));
	}
}
