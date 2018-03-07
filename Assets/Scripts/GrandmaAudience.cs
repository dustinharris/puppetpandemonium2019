using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandmaAudience : MonoBehaviour
{

    [SerializeField]
    private AudienceBarScript AudienceScript;

    private GrandmaAmmo AmmoScript;
    private GrandmaUI UIScript;

    private bool[] ReloadingRed;
    private bool[] ReloadingBlue;

    bool started = false;

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
        if (!started)
        {
            return;
        }

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
                ReleaseReload(true, i);
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
                ReleaseReload(false, i);
            }
        }
    }

    private void HoldReload(bool red, int index)
    {
        bool[] reloading = red ? ReloadingRed : ReloadingBlue;
        AudienceScript.Show(index, AudienceUIScript.Notice.Correct, red);

        if (!reloading[index])
        {
            reloading[index] = true;
            AmmoScript.AddBullet(red);
        }
    }

    private void ReleaseReload(bool red, int index)
    {
        bool[] reloading = red ? ReloadingRed : ReloadingBlue;
        if (NeedsReload(red))
        {
            AudienceScript.Show(index, AudienceUIScript.Notice.Alert, red);
        }

        if (reloading[index])
        {
            reloading[index] = false;
            AmmoScript.RemoveBullet(red);
        }
    }

    public void Reloaded(bool red)
    {
        ResetAudience(red);
    }

    // Called from ammo script
    public void OutOfAmmo(bool red)
    {
        started = true;
        AudienceScript.ShowAll(AudienceUIScript.Notice.Alert, red);
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
