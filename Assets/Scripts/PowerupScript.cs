using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupScript : MonoBehaviour {

    HUDScript hud;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Triggered powerup");
        if (other.tag == "Player1")
        {
            hud = GameObject.Find("Main Camera").GetComponent<HUDScript>();
            hud.player1IncreaseScore(10);
            //Debug.Log("Trying to destroy powerup");
            Destroy(this.gameObject);
        }
        if (other.tag == "Player2")
        {
            hud = GameObject.Find("Main Camera").GetComponent<HUDScript>();
            hud.player2IncreaseScore(10);
            //Debug.Log("Trying to destroy powerup");
            Destroy(this.gameObject);
        }
    }
}
