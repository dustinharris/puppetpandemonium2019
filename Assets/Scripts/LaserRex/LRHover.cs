using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRHover : MonoBehaviour {

    public float hoverSpeed = 6;
    private Vector3 startingPosition;

    // Use this for initialization
    void Start()
    {
        startingPosition = this.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float hover  = 0 + (0.1f * Mathf.Sin(hoverSpeed * Time.time));
        transform.localPosition = new Vector3(startingPosition.x, (startingPosition.y + hover), startingPosition.z);
    }
}
