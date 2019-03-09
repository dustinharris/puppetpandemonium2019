using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateArduinoObjectIfNeeded : MonoBehaviour {

    public GameObject arduinoControllerPrefab;
    
	void Start () {
        // If no arduino controller in scene, create one.
		if (!GameObject.Find("ArduinoController(Clone)"))
        {
            GameObject.Instantiate(arduinoControllerPrefab);
        }
	}
}
