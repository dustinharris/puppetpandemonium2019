using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneryBounce : MonoBehaviour {

    public float bounceSpeed = 1f;
    public float distanceMultiplier = 1f;
    private float individualRandomizer = 0.1f;
	public bool randomize = true;

    // Use this for initialization
    void Start()
    {
		if (randomize) {
			individualRandomizer = Random.Range (0.01f, 0.1f);
		}
    }

    // Update is called once per frame
    void Update()
    {
        float bounce = 0 + (individualRandomizer * Mathf.Sin(bounceSpeed * Time.time));
        transform.localPosition = new Vector3(transform.localPosition.x, bounce*distanceMultiplier, transform.localPosition.z);
    }
}
