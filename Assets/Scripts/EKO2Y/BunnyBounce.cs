using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyBounce : MonoBehaviour {

    [SerializeField] private float bounceSpeed = 1f;
    [SerializeField] private float distanceMultiplier = 1f;
    private float individualRandomizer = 0.1f;
    [SerializeField] private bool randomize = true;
    private float startingY;

    // Use this for initialization
    void Start()
    {
        if (randomize)
        {
            individualRandomizer = Random.Range(0, 1f);
            startingY = transform.position.y;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float bounce = (Mathf.Sin(bounceSpeed * (Time.time + individualRandomizer)));
        transform.localPosition = new Vector3(transform.localPosition.x, bounce * distanceMultiplier + startingY/3, transform.localPosition.z);
    }

    public static float Clamp(float value, float min, float max)
    {
        return (value < min) ? min : (value > max) ? max : value;
    }
}
