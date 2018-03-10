using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandmaTimingController : MonoBehaviour
{

    public float InitialTime = 3.5f;
    public float MinTime = 1.5f;
    public float TimeDecrease = 1f;
    public int RoundsPerTime = 3;

    public float TimePerPath = 1.5f;

    [SerializeField]
    CPC_CameraPath CameraPath;

    private float CurrentTime;
    private int CurrentRound = 0;

    private GrandmaUI UiScript;

    private Coroutine RunningCoroutine = null;

    // Signal to show cutouts
    // Wait for timeout or to receive signal back that both have been shot
    // speed up
    // repeat

    private void Awake()
    {
        CurrentTime = InitialTime;

        UiScript = GetComponent<GrandmaUI>();
    }

    private void Start()
    {
        CameraPath.RefreshTransform();
    }

    // Call when ready to start
    public void Ready()
    {
        CameraPath.PlayPath(CameraPath.points.Count * TimePerPath);

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
        Debug.Log("Current round length: " + CurrentTime.ToString());
        UiScript.ShowTargets();
        yield return new WaitForSeconds(CurrentTime);
        RunningCoroutine = null;
        UiScript.Timeout();
    }

    private IEnumerator MoveCamera()
    {
        yield return new WaitForSeconds(CurrentTime);

        DecreaseTime();

        if (RunningCoroutine != null)
        {
            StopTimer();
        }

        RunningCoroutine = StartCoroutine(ShowTargets());
    }

    private void DecreaseTime()
    {
        if (CurrentTime > MinTime)
        {
            CurrentRound++;

            if (CurrentRound == RoundsPerTime)
            {
                CurrentTime = Mathf.Max(CurrentTime - TimeDecrease, MinTime);
                CurrentRound = 0;
            }
        }
    }
}
