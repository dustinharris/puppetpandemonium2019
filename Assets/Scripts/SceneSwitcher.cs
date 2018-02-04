using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour {

    /*
     * Script to switch to next scene
     * Can transfer data to next scene by setting DontDestroyOnLoad from calling script
     * Currently only whether player won or lost game (or Not Applicaple)
     */ 

    public enum Result
    {
        NA, Win, Lose
    }

    public string NextScene = "";
    public Result result = Result.NA;

    private AsyncOperation LoadOp = null;

    private void Awake()
    {
    }

    // Use this for initialization
    void Start () {
        if (!string.IsNullOrEmpty(NextScene))
        {
            LoadOp = SceneManager.LoadSceneAsync(NextScene);
            LoadOp.allowSceneActivation = false;
        }
    }

    public void SwitchScenes()
    {
        if (LoadOp != null)
        {
            LoadOp.allowSceneActivation = true;
        } else {
            if (!string.IsNullOrEmpty(NextScene))
            {
                SceneManager.LoadSceneAsync(NextScene);
            } else
            {
                throw new UnityException("No scene to switch to");
            }
        }
    }
}
