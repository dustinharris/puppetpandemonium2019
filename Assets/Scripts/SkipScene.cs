using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipScene : MonoBehaviour {

    int nextSceneIndex;

	// Use this for initialization
	void Start () {
        nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("n"))
        {
            ClearInputs();
            Debug.Log("Next scene: " + nextSceneIndex);
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    private void ClearInputs()
    {
        GameObject ctrl = GameObject.Find("ArduinoController");
        if (ctrl != null)
        {
            ArduinoThread thread = ctrl.GetComponent<ArduinoThread>();
            if (thread != null)
            {
                thread.ClearStreams();
            }
        }
    }
}
