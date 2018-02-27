using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandmaPuppets : MonoBehaviour {

    private GrandmaAmmo ammo;

	void Awake () {
		ammo = GetComponent<GrandmaAmmo>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("PuppetRed"))
        {
            Fire(true);
        }
        if (Input.GetButtonDown("PuppetBlue"))
        {
            Fire(false);
        }
	}

    private void Fire(bool red)
    {
        if (ammo.CanFire(red))
        {
            ammo.Fire(red);
        }
    }
}
