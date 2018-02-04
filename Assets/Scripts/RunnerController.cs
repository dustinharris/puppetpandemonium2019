using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class RunnerController : MonoBehaviour {

    // Update is called once per frame
    void Update () {
        
    }

    public void LoadGameOverScene ()
    {
        SceneManager.LoadScene(2);
    }

    
}
