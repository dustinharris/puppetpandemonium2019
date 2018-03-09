using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRSpawnScript : MonoBehaviour {

    public GameObject[] obj;
    [SerializeField] private float spawnFrequency = 1f;
    [SerializeField] private GameObject sceneryEmpty;
    private Quaternion spawnRotation;
    private Vector3 spawnLocalScale;
    private Vector3 spawnPosition;

    // Use this for initialization
    void Start () {
        Spawn();
	}
	
	void Spawn()
    {
        // Get rotation, scale, position from parent spawn object
        spawnRotation = this.transform.rotation;
        spawnLocalScale = this.transform.localScale;
        spawnPosition = this.transform.position;

        // Instantiate new object with spawn's position and rotation
        // Parent to the scenery empty
        GameObject newObject = Instantiate(obj[Random.Range(0, obj.Length)], spawnPosition, spawnRotation, sceneryEmpty.transform);

        // Update new object with spawn's scale
        newObject.transform.localScale = spawnLocalScale;

        // After spawn frequency time has passed, invoke Spawn again to spawn another object
        Invoke("Spawn", spawnFrequency);
    }
}
