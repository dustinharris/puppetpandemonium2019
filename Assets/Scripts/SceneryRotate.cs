using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneryRotate : MonoBehaviour {

    public float rotationSpeed = 10f;
    private int rotationDirection = 1;
    private float individualRandomizer = 1f;

    // Use this for initialization
    void Start()
    {
        rotationDirection = Random.Range(0, 2) * 2 - 1;
        individualRandomizer = Random.Range(.01f, 10f);
    }

    // Update is called once per frame
    void Update()
    {

        transform.Rotate(new Vector3(0, 0, (rotationDirection * 1)) * rotationSpeed*individualRandomizer * Time.deltaTime);

    }
}
