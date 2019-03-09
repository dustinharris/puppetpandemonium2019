using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRCoinLauncherBehavior : MonoBehaviour {

    [SerializeField] private Rigidbody coinReference;
    [SerializeField] private float coinMinUpwardForce = 2f;
    [SerializeField] private float coinMaxUpwardForce = 5f;
    [SerializeField] private float coinMinDirectionAngle = 0;
    [SerializeField] private float coinMaxDirectionAngle = 360f;
    [SerializeField] private float coinMinHeightAngle = -50f;
    [SerializeField] private float coinMaxHeightAngle = -80f;

    private int playerNumber;
    private float finalCoinForce;
    private float finalCoinDirectionAngle;
    private float finalCoinHeightAngle;

    private Animator animator;
    private string idleAnimation;

    private void Awake()
    {
        // Listen for game-triggered events
        Messenger.AddListener(GameEvent.P1_CUBE_NEW_COIN, P1CubeNewCoin);
        Messenger.AddListener(GameEvent.P2_CUBE_NEW_COIN, P2CubeNewCoin);
    }

    private void Start()
    {
        // Get player number from parent object
        LRCubeBehavior cubeBehavior = this.gameObject.GetComponentInParent<LRCubeBehavior>();
        playerNumber = cubeBehavior.GetPlayerNumber();
        idleAnimation = "";
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.P1_CUBE_NEW_COIN, P1CubeNewCoin);
        Messenger.RemoveListener(GameEvent.P2_CUBE_NEW_COIN, P2CubeNewCoin);
    }

    private void LaunchNewCoin()
    {
        // Generate random values for new Coin's force, direction angle, and height angle
        finalCoinForce = Random.Range(coinMinUpwardForce, coinMaxUpwardForce);
        finalCoinDirectionAngle = Random.Range(coinMinDirectionAngle, coinMaxDirectionAngle);
        finalCoinHeightAngle = Random.Range(coinMinHeightAngle, coinMaxHeightAngle);

        // Convert rotation from Euler angles to Quaternion
        Quaternion quaternionRotation = Quaternion.Euler(new Vector3(finalCoinHeightAngle, finalCoinDirectionAngle, 0));

        // Change rotation of coin launcher empty
        this.transform.rotation = quaternionRotation;

        // Instatiate new coin object. Require a rigidbody
        Rigidbody newCoin = (Rigidbody)Instantiate(coinReference, this.transform.position, quaternionRotation);
        
        newCoin.AddForce(transform.forward * finalCoinForce * 100);
    }

    private void P1CubeNewCoin()
    {
        if (playerNumber == 0)
        {
            LaunchNewCoin();
        }
    }

    private void P2CubeNewCoin()
    {
        if (playerNumber == 1)
        {
            LaunchNewCoin();
        }
    }
}
