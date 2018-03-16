using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRRoadSpawn : MonoBehaviour {

    [SerializeField] private int playerNumber;
    [SerializeField] private GameObject spawnerObject;
    [SerializeField] private GameObject roadObject;
    [SerializeField] private GameObject sceneryEmpty;
    private Vector3 existingRoadScale;
    private Quaternion existingRoadRotation;

    void createNewRoad()
    {
        // Create new road at specified spawn point
        GameObject newObject = Instantiate(roadObject, spawnerObject.transform.position, existingRoadRotation);
        newObject.transform.localScale = existingRoadScale;
        newObject.transform.SetParent(sceneryEmpty.transform);
    }

    void OnTriggerEnter(Collider collider)
    {
        existingRoadScale = collider.transform.lossyScale;
        existingRoadRotation = collider.transform.rotation;
        createNewRoad();
    }
}
