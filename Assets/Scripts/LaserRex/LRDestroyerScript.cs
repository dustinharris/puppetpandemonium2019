using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRDestroyerScript : MonoBehaviour {
    
    void OnTriggerEnter(Collider other)
    {
        // If roads or cartridges pass, destroy them 
        if (other.tag == "Road" || other.tag == "LRScenery")
        {
            // Destroy this game object
            Destroy(other.gameObject);
        }
    }
}
