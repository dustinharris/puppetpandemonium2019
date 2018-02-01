using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pulsate : MonoBehaviour {

	public float PulseSpeed = 1f;

	private Image image;

	// Use this for initialization
	void Start () {
		image = this.GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		float alpha = (1 + Mathf.Sin (PulseSpeed * Time.time)) / 2;
		Color temp = image.color;
		temp.a = alpha;

		image.color = temp;
	}
}
