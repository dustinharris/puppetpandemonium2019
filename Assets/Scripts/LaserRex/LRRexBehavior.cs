using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRRexBehavior : MonoBehaviour {

    [SerializeField] private GameObject redLaserAim;
    [SerializeField] private GameObject blueLaserAim;
    [SerializeField] private SpriteRenderer watchWarningIndicator;
    [SerializeField] private float watchWarningTime = 1f;
    [SerializeField] private float minEatingTime = 3f;
    [SerializeField] private float maxEatingTime = 6f;
    [SerializeField] private float dazedTime = 2f;
    [SerializeField] private SpriteRenderer eatingIndicator;
    [SerializeField] private GameObject[] jets;
    [SerializeField] private GameObject[] sparks;
    [SerializeField] private GameObject explosion;
    [SerializeField] private RenderEnabler starCrown;

    private LRLaserAimBehavior laserAimRed;
    private LRLaserAimBehavior laserAimBlue;
    private Animator animator;
    private string[] CandyAnimations;
    private string[] NoddingAnimations;

    private bool p1Moving = true;
    private bool p2Moving = true;
    private bool p1Invincible = false;
    private bool p2Invincible = false;
    private bool rexInWatchState = true;
    private bool rexDefeated = false;
    private bool dazed = false;
    private bool[] eating;
    private bool[] nodding;
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

        Messenger.AddListener(GameEvent.GAME_START, GameStart);

        eating = new bool[2];
        eating[0] = false;
        eating[1] = false;
        nodding = new bool[2];
        nodding[0] = false;
        nodding[1] = false;

        CandyAnimations = new string[2];
        CandyAnimations[0] = "Laser_Rex_Eating Candy";
        CandyAnimations[1] = "Laser_Rex_Eating_Candy_Right_side";
        NoddingAnimations = new string[2];
        NoddingAnimations[0] = "Laser_Rex_Candy_Time";
        NoddingAnimations[1] = "Laser_Rex_Nodding_Right_Side";
    }

    void Start () {

        // Get laser behavior scripts attached to laser aims
        laserAimRed = redLaserAim.GetComponent<LRLaserAimBehavior>();
        laserAimBlue = blueLaserAim.GetComponent<LRLaserAimBehavior>();

        animator = GetComponent<Animator>();
        animator.Play("LAser_REx_Idle no look");
        
        // At the beginning rex is in watch state
        rexInWatchState = false;

        starCrown.SetEnabled(false);

        // Test flag to test various functions
        if (testFunctions == true)
        {
            StartCoroutine(TestLaser(1f));
            StartCoroutine(TestWarningIndicator(1f));
        }
    }

    private void GameStart()
    {
        RexStartWatchWarning();   
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
                laserAimBlue.CreateNewLaser();
                Messenger.Broadcast(GameEvent.P2_REX_STARTING_POS);
            }
        }
    }

    private void RexEatCandy(int candyPlayerNumber)
    {
        rexInWatchState = false;

        // Candy snaps rex out of a daze
        dazed = false;
        starCrown.SetEnabled(false);

        // Show heart over Rex head
        eatingIndicator.enabled = true;
        watchWarningIndicator.enabled = false;

        StartCoroutine(Nodding(candyPlayerNumber));
    }

    private IEnumerator Nodding(int candyPlayerNumber)
    {
        nodding[candyPlayerNumber] = true;

        int otherPlayer = GetOtherPlayer(candyPlayerNumber);

        // Play nodding anim
        animator.Play(NoddingAnimations[candyPlayerNumber]);

        // Destroy other players candy if switching side
        if (eating[otherPlayer])
        {
            DestroyCandy(otherPlayer);
        }

        // Choose a random eating time
        float candyEatTime = Random.Range(minEatingTime, maxEatingTime) - 1;

        // Keep nodding for a second
        yield return new WaitForSeconds(1);

        nodding[candyPlayerNumber] = false;

        if (!nodding[otherPlayer] && !dazed)
        {
            // Start eating
            StartCoroutine(RexEat(candyEatTime, candyPlayerNumber));
        } else
        {
            DestroyCandy(candyPlayerNumber);
            yield return new WaitForSeconds(candyEatTime);
            BroadcastDoneMunching(candyPlayerNumber);
        }
    }

    private IEnumerator RexEat(float eatTime, int candyPlayerNumber)
    {
        int otherPlayer = GetOtherPlayer(candyPlayerNumber);
    
        eating[candyPlayerNumber] = true;
        
        // Play eating anim
        animator.Play(CandyAnimations[candyPlayerNumber]);

        // Destroy other players candy if switching eating side
        if (eating[otherPlayer])
        {
            DestroyCandy(otherPlayer);
        }

        // Wait for eatTime
        yield return new WaitForSeconds(eatTime);
        
        eating[candyPlayerNumber] = false;

        // Destroy candy
        DestroyCandy(candyPlayerNumber);
        BroadcastDoneMunching(candyPlayerNumber);
        

        // If not still eating other side
        if (!eating[GetOtherPlayer(candyPlayerNumber)])
        {
            // Remove heart over Rex head
            eatingIndicator.enabled = false;

            if (!dazed)
            {
                animator.Play("LAser_REx_Idle no look");

                // Start watch warning
                Messenger.Broadcast(GameEvent.REX_START_WATCH_WARNING);
            }
        }
    }

    private void BroadcastDoneMunching(int player)
    {
        if (player == 0)
        {
            Messenger.Broadcast(GameEvent.P1_REX_DONE_MUNCHING);
        }
        else
        {

            Messenger.Broadcast(GameEvent.P2_REX_DONE_MUNCHING);
        }
    }

    private void DestroyCandy(int player)
    {
        GameObject candy;
        if (player == 0)
        {
            candy = GameObject.Find("[Candy_Red_Cube](Clone)");
        }
        else
        {
            candy = GameObject.Find("[Candy_Blue_Cube](Clone)");
        }
        if (candy != null) { 
            Destroy(candy);
        }
    }

    private int GetOtherPlayer(int player)
    {
        return player == 0 ? 1 : 0;
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

        // Hide other state indicators
        starCrown.SetEnabled(true);
        eatingIndicator.enabled = false;
        watchWarningIndicator.enabled = false;
        eating[0] = false;
        eating[1] = false;
        nodding[0] = false;
        nodding[1] = false;

        yield return new WaitForSeconds(dazedTime);

        if (!rexDefeated)
        {
            starCrown.SetEnabled(false);
        }
        dazed = false;

        Messenger.Broadcast(GameEvent.REX_START_WATCH_WARNING);
    }

    private void RexStartWatchWarning()
    {
        StartCoroutine(RexStartWatchWarningTiming());
    }

    private IEnumerator RexStartWatchWarningTiming()
    {
        // If not still in another state
        if (!RexPreoccupied())
        {
            // Show watch warning indicator
            watchWarningIndicator.enabled = true;

            // Wait for X seconds, per watchWarningTime
            yield return new WaitForSeconds(watchWarningTime);

            // Hide watch warning indicator
            watchWarningIndicator.enabled = false;

            // If not hit or started eating during warning time start watching
            if (!RexPreoccupied())
            {
                // Broadcast message to start mamarex watch cycle
                Messenger.Broadcast(GameEvent.REX_START_WATCH);
            }
        }
    }

    // Rex in a state that would prevent her from watching
    private bool RexPreoccupied()
    {
        return rexDefeated || eating[0] || eating[1] || dazed || nodding[0] || nodding[1];
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
        p1Invincible = false;
    }

    private void RexP2StopInvincibility()
    {
        p2Invincible = false;
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
            watchWarningIndicator.enabled = true;

            // Wait for X seconds, per watchWarningTime
            yield return new WaitForSeconds(watchWarningTime);

            // Hide watch warning indicator
            watchWarningIndicator.enabled = false;

            // Wait for X seconds, per watchWarningTime
            yield return new WaitForSeconds(watchWarningTime);
        }
    }
}
