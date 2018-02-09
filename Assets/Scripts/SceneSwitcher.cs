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
    // Loading should be delayed if scene set in inspector is not necessarily the one that will be loaded
    [SerializeField]
    private bool DelayLoading = true;

    
    
    public Result result = Result.NA;

    private AsyncOperation LoadOp = null;
    private bool Loading;

    // Use this for initialization
    void Start()
    {
        if (!DelayLoading) {
            LoadAsync();
        }
    }

    // No turning back after this
    public void StartLoad()
    {
        LoadAsync();
    }

    public void SetNextScene(string scene)
    {
        if (!Loading)
        {
            NextScene = scene;
            if (!DelayLoading) {
                LoadAsync();
            }
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
            LoadOp.allowSceneActivation = true;
        }
        else
        {
            if (!string.IsNullOrEmpty(NextScene))
            {
                SceneManager.LoadSceneAsync(NextScene);
            }
            else
            {
                throw new UnityException("No scene to switch to");
            }
        }
    }
}