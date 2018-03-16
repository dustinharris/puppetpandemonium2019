using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FBLaserAimBehavior : MonoBehaviour
{

    [SerializeField] private GameObject laserReference;
    [SerializeField] private GameObject laserTargetReference;

    void Start()
    {
        // Change object rotation to look at the corresponding laser cube
        transform.LookAt(laserTargetReference.transform);
    }

    public void CreateNewLaser()
    {
        Vector3 targetPosition = laserTargetReference.transform.position;
        Vector3 look = new Vector3(targetPosition.x, Mathf.Max(0, targetPosition.y), targetPosition.z);

        transform.LookAt(look);
        // Instatiate new laser object
        Instantiate(laserReference, this.transform.position, this.transform.rotation);
    }
}
