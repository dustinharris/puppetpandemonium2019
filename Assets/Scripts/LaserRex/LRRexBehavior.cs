using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRRexBehavior : MonoBehaviour {

    [SerializeField] private GameObject redLaserAim;
    [SerializeField] private GameObject blueLaserAim;
    [SerializeField] private GameObject watchWarningIndicator;
    [SerializeField] private float watchWarningTime = 1f;
    [SerializeField] private float minEatingTime = 3f;
    [SerializeField] private float maxEatingTime = 6f;
    [SerializeField] private float dazedTime = 2f;
    [SerializeField] private GameObject eatingIndicator;
    [SerializeField] private GameObject[] jets;
    [SerializeField] private GameObject[] sparks;
    [SerializeField] private GameObject explosion;
    [SerializeField] private RenderEnabler starCrown;

    private LRLaserAimBehavior laserAimRed;
    private LRLaserAimBehavior laserAimBlue;
    private Animator animator;

    private bool p1Moving = true;
    private bool p2Moving = true;
    private bool p1Invincible = false;
    private bool p2Invincible = false;
    private bool rexInWatchState = true;
    private bool rexInP1EatingState = false;
    private bool rexInP2EatingState = false;
    private bool rexDefeated = false;
    private bool dazed = false;
    [SerializeField] private bool testFunctions = false;

    private void Awake()
    {
        // Rex state listeners
        Messenger.AddListener(GameEvent.REX_START_WATCH_WARNING, RexStartWatchWarning);
        Messenger.AddListener(GameEvent.REX_START_WATCH, RexStartWatch);
        Messenger.AddListener(GameEvent.REX_DEFEATED, RexDefeated);
        Messenger.AddListener(GameEvent.P1_CUBE_NEW_CANDY, P1CubeNewCandy);
        Messenger.AddListener(GameEvent.P2_CUBE_NEW_CANDY, P2CubeNewCandy);

        Messenger.AddListener(GameEvent.P1_HIT_REX, HitRex);
        Messenger.AddListener(GameEvent.P2_HIT_REX, HitRex);

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

        animator = GetComponent<Animator>();
        
        // At the beginning rex is in watch state
        rexInWatchState = true;

        starCrown.SetEnabled(false);

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
            if (p1Moving && !p1Invincible && rexInWatchState)
            {
                // Rex caught P1:
                // Shoot laser and send back to start
                //Debug.Log("shoot player");
                laserAimRed.CreateNewLaser();
                Messenger.Broadcast(GameEvent.P1_REX_STARTING_POS);
            }
            if (p2Moving && !p2Invincible && rexInWatchState)
            {
                // Rex caught P2:
                // Shoot laser and send back to start
                //Debug.Log("shoot player");
                laserAimBlue.CreateNewLaser();
                Messenger.Broadcast(GameEvent.P2_REX_STARTING_POS);
            }
        }
    }

    private void RexEatCandy(int candyPlayerNumber)
    {
        if (!rexInP1EatingState && !rexInP2EatingState)
        {
            // Not currently eating. Safe to eat candy.
            rexInWatchState = false;

            // Choose a random eating time
            float candyEatTime = Random.Range(minEatingTime, maxEatingTime);

            // Show heart over Rex head
            eatingIndicator.GetComponent<SpriteRenderer>().enabled = true;

            // Play nodding anim
            // TODO

            // Start eating
            StartCoroutine(RexEat(candyEatTime, candyPlayerNumber));
            
        }
    }

    private IEnumerator RexEat(float eatTime, int candyPlayerNumber)
    {
        // Play eating anim
        animator.Play("Laser_Rex_Eating_Candy_Loop");

        // Wait for eatTime
        yield return new WaitForSeconds(eatTime);

        // Remove heart over Rex head
        eatingIndicator.GetComponent<SpriteRenderer>().enabled = false;

        // Destroy candy
        if (candyPlayerNumber == 0)
        {
            Destroy(GameObject.Find("[Candy_Red_Cube](Clone)"));
        } else
        {
            Destroy(GameObject.Find("[Candy_Blue_Cube](Clone)"));
        }

        // Start watch warning
        Messenger.Broadcast(GameEvent.REX_START_WATCH_WARNING);
    }

    private void P1CubeNewCandy()
    {
        RexEatCandy(0);
    }

    private void P2CubeNewCandy()
    {
        RexEatCandy(1);
    }

    private void RexDefeated()
    {
        rexInWatchState = false;
        rexDefeated = true;

        // Destroy all mama rex jets
        foreach (GameObject go in jets)
        {
            Destroy(go);
        }

        explosion.SetActive(true);
        starCrown.SetEnabled(true);
        animator.Play("Death animation");
        foreach (GameObject go in sparks)
        {
            go.SetActive(true);
        }

        GetComponent<LRDrift>().Stop();

        // Move mama rex to ground
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        rigidbody.useGravity = true;
        rigidbody.mass = 1f;
        GetComponent<BoxCollider>().enabled = true;
    }

    private void RexStartWatch()
    {
        if (!rexDefeated)
        {
            // Start watching animation
            animator.Play("Laser_Rex_Idle");

            rexInWatchState = true;
        }
    }

    private void HitRex()
    {
        rexInWatchState = false;

        if (!rexDefeated)
        {
            StartCoroutine(ReactToHit());
        }
    }

    private IEnumerator ReactToHit()
    {
        dazed = true;
        animator.Play("dazed");
        starCrown.SetEnabled(true);

        yield return new WaitForSeconds(dazedTime);

        if (!rexDefeated)
        {
            starCrown.SetEnabled(false);
        }
        dazed = false;

        Messenger.Broadcast(GameEvent.REX_START_WATCH);
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

    private void RexStartWatchWarning()
    {
        StartCoroutine(RexStartWatchWarningTiming());
    }

    private IEnumerator RexStartWatchWarningTiming()
    {
        if (!rexDefeated)
        {
            Debug.Log("Starting Watch Warning");

            // Show watch warning indicator
            watchWarningIndicator = GameObject.Find("MamaRex_Pre_Watch_Warning");
            watchWarningIndicator.GetComponent<SpriteRenderer>().enabled = true;

            // Wait for X seconds, per watchWarningTime
            yield return new WaitForSeconds(watchWarningTime);

            // Wait to not be dazed
            while (dazed)
            {
                yield return null;
            }

            // Hide watch warning indicator
            watchWarningIndicator.GetComponent<SpriteRenderer>().enabled = false;

            // Wait for X seconds, per watchWarningTime
            yield return new WaitForSeconds(watchWarningTime);

            // Broadcast message to start mamarex watch cycle
            Messenger.Broadcast(GameEvent.REX_START_WATCH);
        }
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
