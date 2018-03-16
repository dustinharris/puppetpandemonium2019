using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRCarBounce : MonoBehaviour {

    [SerializeField] private float launchForce;
    [SerializeField] private float launchDirection;
    [SerializeField] private float launchHeightAngle;
    private GameObject carLauncher;

    // Use this for initialization
    void Start () {
		carLauncher = GameObject.Find("MamaRex_Character_Hit_Launcher");
        launchForce = carLauncher.GetComponent<LRCharacterHitLauncher>().GetLaunchForce();
        launchDirection = carLauncher.GetComponent<LRCharacterHitLauncher>().GetLaunchDirection();
        launchHeightAngle = carLauncher.GetComponent<LRCharacterHitLauncher>().GetLaunchHeightAngle();
        Quaternion quaternionRotation = Quaternion.Euler(new Vector3(launchHeightAngle, launchDirection, 0));
        //this.transform.rotation = quaternionRotation;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BounceCarOffRex()
    {
        this.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * launchForce * 2f);
    }
}
