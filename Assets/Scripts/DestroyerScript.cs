using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerScript : MonoBehaviour {

    public GameObject gameController;

	void OnTriggerEnter2D(Collider2D other)
    {
        // If Blocks or powerups are triggered, destroy them 
        if (other.tag == "Block" || other.tag == "Powerup")
        {
            // Destroy this game object
            Destroy(other.gameObject);
        }
        // If scenery (clouds, mountains) are triggered, destroy their parents
        // These are a combination of objects
        else if (tag == "SceneryDestroyer" && other.tag == "Scenery")
        {
            GameObject parentReference = other.gameObject.transform.parent.gameObject;
            Destroy(parentReference);
        }
        /**if (other.tag == "Player")
        {
            gameController.GetComponent<RunnerController>().LoadGameOverScene();
        }
        else if (other.tag == "Powerup")
        {
            Destroy(other.gameObject);
        }
        else if (other.gameObject.transform.parent)
        {
            Destroy(other.gameObject.transform.parent.gameObject);
        } else
        {
            Destroy(other.gameObject);
        }**/

    }
}
