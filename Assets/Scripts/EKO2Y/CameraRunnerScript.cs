using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRunnerScript : MonoBehaviour {

    public Transform player;

    private void Awake()
    {
        // Needed so that first object spawn will be in the correct position
        transform.position = new Vector3(player.position.x, 0, this.transform.position.z);
    }

    // Update is called once per frame
    void Update () {
        //transform.position = new Vector3(player.position.x, 0, this.transform.position.z);
	}
}
