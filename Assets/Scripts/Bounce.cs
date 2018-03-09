using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour {
    public float bounceSpeed = 1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       float bounce = 0 + (0.05f * Mathf.Sin (bounceSpeed * Time.time));
       transform.localPosition = new Vector3(0, bounce, 0);
	}
}
