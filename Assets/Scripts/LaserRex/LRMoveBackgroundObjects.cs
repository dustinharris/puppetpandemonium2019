using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRMoveBackgroundObjects : MonoBehaviour {

    [SerializeField] private GameObject sceneryEmpty;
    [SerializeField] private float scenerySpeed=1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // Determine how fast to move the scenery based on variable
        float newZ = sceneryEmpty.transform.position.z - (float)(scenerySpeed * .1);

        // Change scenery position each frame
        sceneryEmpty.transform.position = new Vector3(sceneryEmpty.transform.position.x, sceneryEmpty.transform.position.y, newZ);
	}
}
