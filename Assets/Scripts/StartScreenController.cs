using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenController : MonoBehaviour
{
    [SerializeField]
    private GameObject RedPuppetUI;
    [SerializeField]
    private GameObject BluePuppetUI;

    private PlayerUIScript RedScript;
    private PlayerUIScript BlueScript;

    [SerializeField]
    private GameObject AudienceBar;
    private AudienceBarScript AudienceScript;

    private SceneSwitcher SceneSwitcher;

    private const AudienceUIScript.Notice ALERT = AudienceUIScript.Notice.Alert;
    private const AudienceUIScript.Notice CORRECT = AudienceUIScript.Notice.Correct;

    void Start()
    {
        RedScript = RedPuppetUI.GetComponent<PlayerUIScript>();
        BlueScript = BluePuppetUI.GetComponent<PlayerUIScript>();
        if (RedScript != null)
        {
            RedScript.Show(true);
        }
        if (BlueScript != null)
        {
            BlueScript.Show(true);
        }

        AudienceScript = AudienceBar.GetComponent<AudienceBarScript>();

        AudienceScript.ShowAll(AudienceUIScript.Notice.Alert);

        SceneSwitcher = GetComponent<SceneSwitcher>();

        StartCoroutine(WaitForReady());
    }

    private IEnumerator WaitForReady()
    {
        while (!this.IsReady())
        {
            yield return null;
        }

        SceneSwitcher.SwitchScenes();
    }

    public bool IsReady()
    {
        bool ready = true;

        if (RedScript != null)
        {
            if (RedScript.IsVisible(true))
            {
                if (Input.GetButtonDown("RedPuppet"))
                {
                    RedScript.Show(false);
                }
                else
                {
                    ready = false;
                }
            }
        }

        if (BlueScript != null)
        {
            if (BlueScript.IsVisible(true))
            {
                if (Input.GetButtonDown("BluePuppet"))
                {
                    BlueScript.Show(false);
                }
                else
                {
                    ready = false;
                }
            }
        }

        // Check if each audience member has pressed buttons
        int sections = AudienceScript.Size();
        for (int i = 0; i < sections; i++)
        {
            if (AudienceScript.IsVisible(i, ALERT, true))
            {
                if (Input.GetButtonDown("Audience" + i + "Red"))
                {
                    AudienceScript.Show(i, CORRECT, true);
                }
                else
                {
                    ready = false;
                }
            }
            if (AudienceScript.IsVisible(i, ALERT, false))
            {
                if (Input.GetButtonDown("Audience" + i + "Blue"))
                {
                    AudienceScript.Show(i, CORRECT, false);
                }
                else
                {
                    ready = false;
                }
            }
        }

        return ready;
    }
}