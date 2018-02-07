using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempGameController : MonoBehaviour {

    [SerializeField]
    private GameObject SceneSwitcherObject;

    private SceneSwitcher switcher;

	// Use this for initialization
	void Start () {
        switcher = SceneSwitcherObject.GetComponent<SceneSwitcher>();
        DontDestroyOnLoad(SceneSwitcherObject);
	}

    private void OnGUI()
    {
        GUI.Label(new Rect(100, 100, 200, 20), "Press z to win or x to lose");
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown("z"))
        {
            switcher.result = SceneSwitcher.Result.Win;
            switcher.SwitchScenes();
        }
        else if (Input.GetKeyDown("x"))
        {
            switcher.result = SceneSwitcher.Result.Lose;
            switcher.SwitchScenes();
        }
	}
}
