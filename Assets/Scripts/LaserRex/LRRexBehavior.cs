using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRRexBehavior : MonoBehaviour {

    [SerializeField] private GameObject redLaserAim;
    [SerializeField] private GameObject blueLaserAim;
    private LRLaserAimBehavior laserAimRed;
    private LRLaserAimBehavior laserAimBlue;
    [SerializeField] private bool testTargeting = false;

    // Use this for initialization
    void Start () {

        // Get laser behavior scripts attached to laser aims
        laserAimRed = redLaserAim.GetComponent<LRLaserAimBehavior>();
        laserAimBlue = blueLaserAim.GetComponent<LRLaserAimBehavior>();

        // Test flag to test lasers
        if (testTargeting == true)
        {
            StartCoroutine(TestLaser(1f));
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator TestLaser(float waitTime)
    {
        while(true)
        {
            laserAimRed.CreateNewLaser();
            laserAimBlue.CreateNewLaser();
            yield return new WaitForSeconds(waitTime);
        }
    }
}
