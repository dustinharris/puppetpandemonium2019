using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRLaserAimBehavior : MonoBehaviour {

    [SerializeField] private GameObject laserReference;
    [SerializeField] private GameObject laserTargetReference;

    void Start()
    {
        // Change object rotation to look at the corresponding laser cube
        transform.LookAt(laserTargetReference.transform);
    }

    public void CreateNewLaser()
    {
        transform.LookAt(laserTargetReference.transform);
        // Instatiate new laser object
        GameObject newLaser = Instantiate(laserReference, this.transform.position, this.transform.rotation);
    }
}
