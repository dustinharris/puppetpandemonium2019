using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRRoadSpawn : MonoBehaviour {

    [SerializeField] private int playerNumber;
    [SerializeField] private GameObject spawnerObject;
    [SerializeField] private GameObject characterObject;
    [SerializeField] private GameObject roadObject;

	// Use this for initialization
	void Start () {
		
	}
	
    // Update is called once per frame
	void Update () {
		
	}

    void createNewRoad()
    {
        // Create new Red Road at Red Road Spawn// Instantiate new object with rotation
        GameObject newObject = Instantiate(obj[Random.Range(0, obj.Length)], new Vector3(transform.position.x, (transform.position.y + spawnFinalYHeight), transform.position.z), Quaternion.Euler(0, 0, spawnFinalRotation));
        newObject.transform.localScale = new Vector3(spawnFinalScale, spawnFinalScale, 1);
        newObject.transform.SetParent(sceneryEmpty.transform);
    }

    void OnTriggerEnter(Collider collider)
    {
        if ((collider.gameObject.name == characterObject.name) && playerNumber == 0)
        {
            createNewRoad();
        }

        if ((collider.gameObject.name == characterObject.name) && playerNumber == 1)
        {
            createNewRoad();
        }
    }
}
