using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FFAudience : MonoBehaviour
{

    [SerializeField] private AudienceBarScript AudienceScript;
    [SerializeField] private GameObject blueCarAlert;
    [SerializeField] private GameObject redCarAlert;
    private bool p1ButtonDown;
    private bool p2ButtonDown;
    private bool[] chargingRed;
    private bool[] chargingBlue;
    [SerializeField] private FBLaserAimBehavior[] allLaserAims;
    private bool allCharged;

    public bool testing;

    // Use this for initialization
    void Start()
    {
        // Show all audience member alerts
        AudienceScript.ShowAll(AudienceUIScript.Notice.Alert);
        
        // Initiate arrays
        int arraySize = AudienceScript.Size() + 1;
        chargingRed = new bool[arraySize];
        chargingBlue = new bool[arraySize];
        if (testing)
        {
            StartCoroutine(Testing());
        }
    }

    private IEnumerator Testing()
    {
        yield return new WaitForSeconds(3f);
        StartCoroutine(FireLasers());
        Messenger.Broadcast(GameEvent.ALL_GO);
    }

    void Update()
    {
        if (!allCharged)
        {
            // Check if each audience member is pressing a button
            int sections = AudienceScript.Size();

            // Audience member sections
            for (int i = 0; i < sections; i++)
            {
                if (Input.GetButtonDown("Audience" + i + "Red"))
                {
                    HoldCharge(true, i);
                }
                if (Input.GetButtonUp("Audience" + i + "Red"))
                {
                    ReleaseCharge(true, i);
                }
                if (Input.GetButtonDown("Audience" + i + "Blue"))
                {
                    HoldCharge(false, i);
                }
                if (Input.GetButtonUp("Audience" + i + "Blue"))
                {
                    ReleaseCharge(false, i);
                }
            }

            // Players
            int playerIndex = AudienceScript.Size();
            if (Input.GetButtonDown("RedPuppet"))
            {
                chargingRed[playerIndex] = true;
                redCarAlert.GetComponent<SpriteRenderer>().enabled = false;
            }
            if (Input.GetButtonUp("RedPuppet"))
            {
                chargingRed[playerIndex] = false;
                redCarAlert.GetComponent<SpriteRenderer>().enabled = true;
            }
            if (Input.GetButtonDown("BluePuppet"))
            {
                chargingBlue[playerIndex] = true;
                blueCarAlert.GetComponent<SpriteRenderer>().enabled = false;
            }
            if (Input.GetButtonUp("BluePuppet"))
            {
                chargingBlue[playerIndex] = false;
                blueCarAlert.GetComponent<SpriteRenderer>().enabled = true;
            }
        }

        // Check to see if all buttons are pressed
        if (AllTrue(chargingRed) && AllTrue(chargingBlue))
        {
            // Fire all lasers
            Debug.Log("ALLLLLL GOOO");
            allCharged = true;

            StartCoroutine(FireLasers());

            Messenger.Broadcast(GameEvent.ALL_GO);
        }
    }

    private IEnumerator FireLasers()
    {
        while (true)
        {
            foreach (FBLaserAimBehavior laserAim in allLaserAims)
            {
                laserAim.CreateNewLaser();
            }
            yield return new WaitForSeconds(.4f);
        }
    }

    private void HoldCharge(bool red, int index)
    {
        AudienceScript.Show(index, AudienceUIScript.Notice.Correct, red);
        if (red)
        {
            chargingRed[index] = true;
        } else
        {
            chargingBlue[index] = true;
        }
    }

    private void ReleaseCharge(bool red, int index)
    {
        AudienceScript.Show(index, AudienceUIScript.Notice.Alert, red);
        if (red)
        {
            chargingRed[index] = false;
        }
        else
        {
            chargingBlue[index] = false;
        }
    }

    private bool AllTrue(bool[] MissionList)
    {
        for (int i = 0; i < MissionList.Length; ++i)
        {
            if (MissionList[i] == false)
            {
                return false;
            }
        }

        return true;
    }
}