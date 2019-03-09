using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertBounce : MonoBehaviour {

    [SerializeField] private float bounceSpeed = 1f;
    [SerializeField] private float distanceMultiplier = 1f;
    private Vector3 startingPosition;

    void Start()
    {
        startingPosition = this.transform.localPosition;
    }

    void Update()
    {
        float bounce = Mathf.Sin(bounceSpeed * Time.time);
        //Debug.Log(bounce);
        transform.localPosition = new Vector3(startingPosition.x, startingPosition.y + bounce*distanceMultiplier, startingPosition.z);
    }
}
