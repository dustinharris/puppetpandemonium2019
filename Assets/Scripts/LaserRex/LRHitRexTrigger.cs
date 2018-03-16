using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRHitRexTrigger : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "LRHitCar")
        {
            other.gameObject.GetComponent<LRCarBounce>().BounceCarOffRex();
        }
    }
}
