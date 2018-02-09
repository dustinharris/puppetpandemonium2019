using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupScript : MonoBehaviour {

    HUDScript hud;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if 
            (other.tag == "Player1" || other.tag == "Player2") {
            hud = GameObject.Find("Main Camera").GetComponent<HUDScript>();
            hud.increaseScore(10);
            Destroy(this.gameObject);
        }
    }
}
