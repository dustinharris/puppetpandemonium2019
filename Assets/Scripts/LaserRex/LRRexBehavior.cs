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
    private bool p1Moving = true;
    private bool p2Moving = true;
    private bool p1Invincible = false;
    private bool p2Invincible = false;
    private bool rexInWatchState = true;
    private bool rexDefeated = true;
    private bool rexInEatingState = false;
    [SerializeField] private bool testFunctions = false;

    private void Awake()
    {
        // Rex state listeners
        Messenger.AddListener(GameEvent.REX_START_WATCH_WARNING, RexStartWatchWarning);
        Messenger.AddListener(GameEvent.REX_START_WATCH, RexStartWatch);
        Messenger.AddListener(GameEvent.REX_DEFEATED, RexDefeated);
        Messenger.AddListener(GameEvent.P1_CUBE_NEW_CANDY, P1CubeNewCandy);
        Messenger.AddListener(GameEvent.P2_CUBE_NEW_CANDY, P2CubeNewCandy);

        // Car state listeners
        Messenger.AddListener(GameEvent.REX_P1_START_MOVING, RexP1StartMoving);
        Messenger.AddListener(GameEvent.REX_P2_START_MOVING, RexP2StartMoving);
        Messenger.AddListener(GameEvent.REX_P1_STOP_MOVING, RexP1StopMoving);
        Messenger.AddListener(GameEvent.REX_P2_STOP_MOVING, RexP2StopMoving);
        Messenger.AddListener(GameEvent.REX_P1_START_INVINCIBILITY, RexP1StartInvincibility);
        Messenger.AddListener(GameEvent.REX_P2_START_INVINCIBILITY, RexP2StartInvincibility);
        Messenger.AddListener(GameEvent.REX_P1_STOP_INVINCIBILITY, RexP1StopInvincibility);
        Messenger.AddListener(GameEvent.REX_P2_STOP_INVINCIBILITY, RexP2StopInvincibility);
    }

    void Start () {

        // Get laser behavior scripts attached to laser aims
        laserAimRed = redLaserAim.GetComponent<LRLaserAimBehavior>();
        laserAimBlue = blueLaserAim.GetComponent<LRLaserAimBehavior>();

        // At the beginning rex is in watch state
        rexInWatchState = true;

        // Test flag to test various functions
        if (testFunctions == true)
        {
            StartCoroutine(TestLaser(1f));
            StartCoroutine(TestWarningIndicator(1f));
        }
    }

    void Update()
    {
        // If rex is in watch state and either player is moving, shoot that player
        if (rexInWatchState)
        {
            if (p1Moving && !p1Invincible)
            {
                // Rex caught P1:
                // Shoot laser and send back to start
                //Debug.Log("shoot player");
                laserAimRed.CreateNewLaser();
                Messenger.Broadcast(GameEvent.P1_REX_STARTING_POS);
            }
            if (p2Moving && !p2Invincible)
            {
                // Rex caught P2:
                // Shoot laser and send back to start
                //Debug.Log("shoot player");
                laserAimBlue.CreateNewLaser();
                Messenger.Broadcast(GameEvent.P2_REX_STARTING_POS);
            }
        }
    }

    private void P1CubeNewCandy()
    {

    }

    private void P2CubeNewCandy()
    {

    }

    private void RexDefeated()
    {
        rexInWatchState = false;
        rexDefeated = true;
    }

    private void RexStartWatch()
    {
        // Start watching animation
    }

    private void RexP1StartMoving()
    {
        p1Moving = true;
    }

    private void RexP2StartMoving()
    {
        p2Moving = true;
    }

    private void RexP1StopMoving()
    {
        p1Moving = false;
    }

    private void RexP2StopMoving()
    {
        p2Moving = false;
    }

    private void RexP1StartInvincibility()
    {
        p1Invincible = true;
    }

    private void RexP2StartInvincibility()
    {
        p2Invincible = true;
    }

    private void RexP1StopInvincibility()
    {
        //Debug.Log("rex received stop invincibility");
        p1Invincible = false;
    }

    private void RexP2StopInvincibility()
    {
        //Debug.Log("rex received stop invincibility");
        p2Invincible = false;
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
