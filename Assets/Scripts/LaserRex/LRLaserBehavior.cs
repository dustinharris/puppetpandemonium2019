using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRLaserBehavior : MonoBehaviour {

    [SerializeField] private float laserDuration = .5f;
    
    void Start () {
        // Call coroutine to wait X seconds, then destroy coin.
        StartCoroutine(DestroyLaser(laserDuration));
    }
	
	void Update () {
        // TODO
        // Decrease opacity
	}

    private IEnumerator DestroyLaser(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(this.gameObject);
    }
}
