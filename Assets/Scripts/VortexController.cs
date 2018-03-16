using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VortexController : MonoBehaviour {

    [SerializeField]
    private float VortexTime;

    private SceneSwitcher sceneSwitcher;
    
    void Start()
    {
        sceneSwitcher = GetComponent<SceneSwitcher>();

        StartCoroutine(WaitToSwitchScenes());
    }

    private IEnumerator WaitToSwitchScenes()
    {
        yield return new WaitForSeconds(VortexTime);
        sceneSwitcher.SwitchScenes();
    }
}
