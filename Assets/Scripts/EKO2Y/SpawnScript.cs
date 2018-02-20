using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour {

    public GameObject[] obj;
    public float spawnMinTime = 1f;
    public float spawnMaxTime = 1f;
    public float spawnMinScale = 1f;
    public float spawnMaxScale = 1f;
    public float spawnMinRotation = 1f;
    public float spawnMaxRotation = 1f;
    private float spawnFinalRotation = 0f;
    private float spawnFinalScale = 1f;
    private float spawnFinalYHeight = 0f;
    public float spawnMinYHeight = 0f;
    public float spawnMaxYheight = 0f;
    public GameObject sceneryEmpty;

	// Use this for initialization
	void Start () {
        Spawn();
	}

    void Spawn()
    {
        // Choose random floats for scale and rotation
        spawnFinalRotation = Random.Range(spawnMinRotation, spawnMaxRotation);
        spawnFinalScale = Random.Range(spawnMinScale, spawnMaxScale);
        spawnFinalYHeight = Random.Range(spawnMinYHeight, spawnMaxYheight);
        // Instantiate new object with rotation
        GameObject newObject = Instantiate(obj[Random.Range (0, obj.Length)], new Vector3(transform.position.x, (transform.position.y+spawnFinalYHeight), transform.position.z), Quaternion.Euler(0, 0, spawnFinalRotation));
        // Update new object with final scale
        newObject.transform.localScale = new Vector3(spawnFinalScale, spawnFinalScale, 1);
        newObject.transform.SetParent(sceneryEmpty.transform);
        Invoke("Spawn", Random.Range(spawnMinTime, spawnMaxTime));
    }
}
