using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashScript : MonoBehaviour {

    [SerializeField]
    private GameObject CrashIcon;

    public float Length = .5f;

	public void Crash(Vector3 position)
    {
        CrashIcon.transform.position = position;
        StartCoroutine(ShowCrash());
    }
    
    private IEnumerator ShowCrash()
    {
        CrashIcon.SetActive(true);
        yield return new WaitForSeconds(Length);
        CrashIcon.SetActive(false);
    }
}
