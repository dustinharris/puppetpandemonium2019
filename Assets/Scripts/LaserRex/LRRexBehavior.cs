using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRRexBehavior : MonoBehaviour {

    [SerializeField] private GameObject redLaserAim;
    [SerializeField] private GameObject blueLaserAim;
    [SerializeField] private GameObject watchWarningIndicator;
    [SerializeField] private float watchWarningTime = 1f;
    private LRLaserAimBehavior laserAimRed;
    private LRLaserAimBehavior laserAimBlue;
    [SerializeField] private bool testFunctions = false;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.REX_START_WATCH_WARNING, RexStartWatchWarning);
        Messenger.AddListener(GameEvent.REX_START_WATCH, RexStartWatch);
    }

    void Start () {

        // Get laser behavior scripts attached to laser aims
        laserAimRed = redLaserAim.GetComponent<LRLaserAimBehavior>();
        laserAimBlue = blueLaserAim.GetComponent<LRLaserAimBehavior>();

        // Test flag to test various functions
        if (testFunctions == true)
        {
            StartCoroutine(TestLaser(1f));
            StartCoroutine(TestWarningIndicator(1f));
        }
    }

    private void RexStartWatch()
    {
        // Start watching animation
    }

    private IEnumerator RexStartWatchWarning()
    {
        // Show watch warning indicator
        watchWarningIndicator.SetActive(true);

        // Wait for X seconds, per watchWarningTime
        yield return new WaitForSeconds(watchWarningTime);

        // Hide watch warning indicator
        watchWarningIndicator.SetActive(false);

        // Broadcast message to start mamarex watch cycle
        Messenger.Broadcast(GameEvent.REX_START_WATCH);
    }

    private IEnumerator TestLaser(float waitTime)
    {
        while(true)
        {
            laserAimRed.CreateNewLaser();
            laserAimBlue.CreateNewLaser();
            yield return new WaitForSeconds(waitTime);
        }
    }

    private IEnumerator TestWarningIndicator(float waitTime)
    {
        while (true)
        {
            // Show watch warning indicator
            watchWarningIndicator.SetActive(true);

            // Wait for X seconds, per watchWarningTime
            yield return new WaitForSeconds(watchWarningTime);

            // Hide watch warning indicator
            watchWarningIndicator.SetActive(false);

            // Wait for X seconds, per watchWarningTime
            yield return new WaitForSeconds(watchWarningTime);
        }
    }
}
