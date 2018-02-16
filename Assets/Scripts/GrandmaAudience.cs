using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandmaAudience : MonoBehaviour {

    [SerializeField]
    private GameObject AudienceBar;
    private AudienceBarScript AudienceScript;

    void Start () {
        AudienceScript = AudienceBar.GetComponent<AudienceBarScript>();
        AudienceScript.ShowAll(AudienceUIScript.Notice.Glow);
    }

    void Update()
    {
        // Check if each audience member is pressing a button
        int sections = AudienceScript.Size();
        for (int i = 0; i < sections; i++)
        {
            if (Input.GetButtonDown("Audience" + i + "Red"))
            {
                if (NeedsReload(true))
                {

                } else
                {
                    
                }
            }
            if (Input.GetButtonDown("Audience" + i + "Blue"))
            {
                if (NeedsReload(false))
                {

                } else
                {

                }
            }
        }
    }

    private bool NeedsReload(bool red)
    {
        return false;
    }
}
