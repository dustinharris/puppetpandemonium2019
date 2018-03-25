using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{

    /*
     * Script to switch to next scene
     * Can transfer data to next scene by setting DontDestroyOnLoad from calling script
     * Currently only whether player won or lost game (or Not Applicaple)
     */

    public enum Result
    {
        NA, Win, Lose
    }

    [SerializeField]
    private string NextScene = "";
    
    public Result result = Result.NA;

    private AsyncOperation LoadOp = null;
    private bool Loading;

    void Start()
    {
        LoadAsync();
    }

    public void SetNextScene(string scene)
    {
        if (!Loading)
        {
            NextScene = scene;
            LoadAsync();
        }
    }

    public string GetNextScene()
    {
        return NextScene;
    }

    private void LoadAsync()
    {
        if (!string.IsNullOrEmpty(NextScene) && !Loading)
        {
            Loading = true;
            LoadOp = SceneManager.LoadSceneAsync(NextScene);
            LoadOp.allowSceneActivation = false;
        }
    }

    public void SwitchScenes()
    {
        if (LoadOp != null)
        {
            ClearInputs();
            LoadOp.allowSceneActivation = true;
        }
        else
        {
            if (!string.IsNullOrEmpty(NextScene))
            {
                ClearInputs();
                SceneManager.LoadSceneAsync(NextScene);
            }
            else
            {
                throw new UnityException("No scene to switch to");
            }
        }
    }

    private void ClearInputs()
    {
        GameObject ctrl = GameObject.Find("ArduinoController");
        if (ctrl != null)
        {
            ArduinoThread thread = ctrl.GetComponent<ArduinoManager>().GetArduinoThread();
            if (thread != null)
            {
                thread.ClearStreams();
            }
        }
    }
}