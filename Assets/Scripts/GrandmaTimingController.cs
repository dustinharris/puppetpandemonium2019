using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandmaTimingController : MonoBehaviour {

    public float InitialTime = 5f;
    public float MinTime = 1f;
    public float TimeDecrease = 1f;

    private float CurrentTime;

    private GrandmaUI UiScript;

    private Coroutine RunningCoroutine = null;

    private void Awake()
    {
        CurrentTime = InitialTime;

        UiScript = GetComponent<GrandmaUI>();
    }

    // Call when ready to start
    public void Ready()
    {

        // Signal to show cutouts
        // Wait for time or to receive signal back that both have been shot
        // Signal to move locations
        // Wait for signal that moving is finished
        // speed up
        // repeat

        RunningCoroutine = StartCoroutine(ShowTargets());
    }

    // Call when round is finished and ready to move camera
    public void RoundComplete()
    {
        Debug.Log("RoundComplete");
        StopTimer();
        StartCoroutine(MoveCamera());
    }

    public void StopTimer()
    {
        if (RunningCoroutine != null)
        {
            StopCoroutine(RunningCoroutine);
            RunningCoroutine = null;
        }
    }

    private IEnumerator ShowTargets()
    {
        UiScript.ShowTargets();
        yield return new WaitForSeconds(CurrentTime);
        RunningCoroutine = null;
        UiScript.Timeout();
    }

    private IEnumerator MoveCamera()
    {
        // TODO Tell camera to move

        // Remove this when hooked up to camera
        yield return new WaitForSeconds(CurrentTime);

        CurrentTime = Mathf.Max(CurrentTime - TimeDecrease, MinTime);

        RunningCoroutine = StartCoroutine(ShowTargets());
    }
}
