using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainBounce : MonoBehaviour {

    [SerializeField] private float bounceSpeed = 1f;
    [SerializeField] private float distanceMultiplier = 1f;
    private float individualRandomizer = 0.1f;
	[SerializeField] private bool randomize = true;

    // Use this for initialization
    void Start()
    {
		if (randomize) {
			individualRandomizer = Random.Range (0, 1f);
		}
    }

    // Update is called once per frame
    void Update()
    {
        float bounce = (Mathf.Sin(bounceSpeed * (Time.time+individualRandomizer)));
        transform.localPosition = new Vector3(transform.localPosition.x, bounce*distanceMultiplier-.16f, transform.localPosition.z);
    }

    public static float Clamp(float value, float min, float max)
    {
        return (value < min) ? min : (value > max) ? max : value;
    }
}
