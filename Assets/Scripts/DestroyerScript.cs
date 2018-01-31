using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerScript : MonoBehaviour {

    public GameObject gameController;

	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            gameController.GetComponent<RunnerController>().LoadGameOverScene();
        }
        else if (other.tag == "Block")
        {
            Destroy(other.gameObject);
        }
        else if (other.tag == "Powerup")
        {
            Destroy(other.gameObject);
        }
        /**else if (other.gameObject.transform.parent)
        {
            Destroy(other.gameObject.transform.parent.gameObject);
        } else
        {
            Destroy(other.gameObject);
        }**/

    }
}
