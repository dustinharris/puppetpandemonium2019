using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandmaPuppets : MonoBehaviour
{

    private GrandmaAmmo ammo;
    public RandomPitch FireSFX;
    public RandomPitch LoadClickSFX;

    void Awake()
    {
        ammo = GetComponent<GrandmaAmmo>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("RedPuppet"))
        {
            Fire(true);
        }
        if (Input.GetButtonDown("BluePuppet"))
        {
            Fire(false);
        }
    }

    private void Fire(bool red)
    {
        if (ammo.CanFire(red))
        {
            ammo.Fire(red);
            FireSFX.PlayRandomPitch();
        }
        else
        {
            LoadClickSFX.PlayRandomPitch();
        }
    }
}
