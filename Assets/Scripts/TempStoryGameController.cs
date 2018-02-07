using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempStoryGameController : MonoBehaviour
{
    private SceneSwitcher switcher;

    // Use this for initialization
    void Start()
    {
        switcher = GetComponent<SceneSwitcher>();
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(100, 100, 200, 20), "Press z to continue");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("z"))
        {
            switcher.SwitchScenes();
        }
    }
}
