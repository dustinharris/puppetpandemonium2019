using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandmaAudience : MonoBehaviour
{

    [SerializeField]
    private AudienceBarScript AudienceScript;
    [SerializeField]
    RandomPitch reloadSFX;

    private GrandmaAmmo AmmoScript;
    private GrandmaUI UIScript;

    private bool[] ReloadingRed;
    private bool[] ReloadingBlue;

    private void Awake()
    {
        AmmoScript = GetComponent<GrandmaAmmo>();
        UIScript = GetComponent<GrandmaUI>();
    }

    void Start()
    {
        AudienceScript.ShowAll(AudienceUIScript.Notice.Glow);

        ReloadingRed = new bool[AudienceScript.Size()];
        ReloadingBlue = new bool[AudienceScript.Size()];

        ResetAudience();
    }

    void Update()
    {
        bool RedNeedsReload = NeedsReload(true);
        bool BlueNeedsReload = NeedsReload(false);

        // Check if each audience member is pressing a button
        int sections = AudienceScript.Size();
        for (int i = 0; i < sections; i++)
        {
            if (Input.GetButtonDown("Audience" + i + "Red"))
            {
                if (RedNeedsReload)
                {
                    HoldReload(true, i);
                }
                else
                {
                    UIScript.ShowAlert(true);
                }
            }
            if (Input.GetButtonUp("Audience" + i + "Red"))
            {
                ReloadingRed[i] = false;
                if (RedNeedsReload)
                {
                    AudienceScript.Show(i, AudienceUIScript.Notice.Alert, true);
                }
            }
            if (Input.GetButtonDown("Audience" + i + "Blue"))
            {
                if (BlueNeedsReload)
                {
                    HoldReload(false, i);
                }
                else
                {
                    UIScript.ShowAlert(false);
                }
            }
            if (Input.GetButtonUp("Audience" + i + "Blue"))
            {
                ReloadingBlue[i] = false;
                if (BlueNeedsReload)
                {
                    AudienceScript.Show(i, AudienceUIScript.Notice.Alert, false);
                }
            }
        }

        if (RedNeedsReload)
        {
            int reloading = NumberReloading(true);
            if (reloading == GrandmaAmmo.MAG_SIZE)
            {
                AmmoScript.Reload(true);
            }
            else
            {
                UIScript.Reloading(true, reloading);
            }
        }
        if (BlueNeedsReload)
        {
            int reloading = NumberReloading(false);
            if (reloading == GrandmaAmmo.MAG_SIZE)
            {
                AmmoScript.Reload(false);
            }
            else
            {
                UIScript.Reloading(false, reloading);
            }
        }

    }

    private void HoldReload(bool red, int index)
    {
        bool[] reloading = red ? ReloadingRed : ReloadingBlue;
        reloading[index] = true;
        reloadSFX.PlayRandomPitch();
        AudienceScript.Show(index, AudienceUIScript.Notice.Correct, red);
    }

    // Called from ammo script
    public void OutOfAmmo(bool red)
    {
        AudienceScript.ShowAll(AudienceUIScript.Notice.Alert, red);
    }

    public void Reloaded(bool red)
    {
        ResetAudience(red);
    }

    private void ResetAudience()
    {
        int size = AudienceScript.Size();
        for (int i = 0; i < size; i++)
        {
            ReloadingRed[i] = false;
            ReloadingBlue[i] = false;
            AudienceScript.Hide(i, true);
            AudienceScript.Hide(i, false);
        }
    }

    private void ResetAudience(bool red)
    {
        bool[] Reloading = red ? ReloadingRed : ReloadingBlue;

        int size = AudienceScript.Size();
        for (int i = 0; i < size; i++)
        {
            Reloading[i] = false;
            AudienceScript.Hide(i, red);
        }
    }

    private int NumberReloading(bool red)
    {
        bool[] Reloading = red ? ReloadingRed : ReloadingBlue;

        int count = 0;

        int size = AudienceScript.Size();
        for (int i = 0; i < size; i++)
        {
            if (Reloading[i])
            {
                count++;
            }
        }

        return count;
    }

    private bool NeedsReload(bool red)
    {
        return !AmmoScript.CanFire(red);
    }
}
