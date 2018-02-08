using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour {

    public float Duration = 1;
    public float Delay = 0;
    private float StartTime;
    private Image image;

	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
        Color temp = image.color;
        temp.a = 0;
        image.color = temp;

        StartTime = Time.time + Delay;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time > StartTime)
        {
            float t = (Time.time - StartTime) / Duration;
            Color temp = image.color;
            temp.a = t;
            image.color = temp;

        }
        
	}
}
