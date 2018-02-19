using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pulsate : MonoBehaviour {

	public float PulseSpeed = 1f;

	private Image image;
    private Text text;

	// Use this for initialization
	void Start () {
		image = this.GetComponent<Image> ();
        if (image == null)
        {
            text = this.GetComponent<Text>();
        }
	}
	
	// Update is called once per frame
	void Update () {
        Color temp;

        if (image != null)
        {
            temp = image.color;
        } else
        {
            temp = text.color;
        }

		float alpha = (1 + Mathf.Sin (PulseSpeed * Time.time)) / 2;
        
		temp.a = alpha;

        if (image != null)
        {
            image.color = temp;
        }
        else
        {
            text.color = temp;
        }
	}
}
