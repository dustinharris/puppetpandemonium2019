using UnityEngine;

public class GrandmaPuppets : MonoBehaviour
{
    private GrandmaAmmo ammo;
    private bool RedReloading = false;
    private bool BlueReloading = false;

    private bool started = false;

    void Awake()
    {
        ammo = GetComponent<GrandmaAmmo>();
    }

    // Sent from ammo script on game start
    public void OutOfAmmo(bool red)
    {
        started = true;
    }

    void Update()
    {
        if (!started)
        {
            return;
        }

        var redCanFire = ammo.CanFire(true);
        var blueCanFire = ammo.CanFire(false);

        if (redCanFire)
        {
            RedReloading = false;
        }
        if (blueCanFire)
        {
            BlueReloading = false;
        }

        if (Input.GetButtonDown("RedPuppet"))
        {
            if (redCanFire)
            {
                ammo.Fire(true);
            } 
            else if (!RedReloading)
            {
                RedReloading = true;
                ammo.AddBullet(true);
            }  
        }
        if (Input.GetButtonDown("BluePuppet"))
        {
            if (blueCanFire)
            {
                ammo.Fire(false);
            } 
            else if (!BlueReloading)
            {
                BlueReloading = true;
                ammo.AddBullet(false);
            }
        }
    }
}
