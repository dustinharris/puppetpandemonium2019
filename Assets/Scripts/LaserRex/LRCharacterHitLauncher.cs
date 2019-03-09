using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRCharacterHitLauncher : MonoBehaviour {

    [SerializeField] private GameObject gameCarRed;
    [SerializeField] private GameObject gameCarBlue;
    [SerializeField] private Rigidbody hitCarRed;
    [SerializeField] private Rigidbody hitCarBlue;
    [SerializeField] private float launchForce;
    [SerializeField] private float launchDirection;
    [SerializeField] private float launchHeightAngle;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera hitCamera;
    [SerializeField] private GameObject mamaRex;
    private Vector3 hitCarPosition;
    private Vector3 hitCarScale;
    private Quaternion hitCarRotation;
    private Rigidbody hitObject;
    private GameObject hitObjectCar;

    private Animator animator;

    void Awake()
    {
        Messenger.AddListener(GameEvent.P1_HIT_REX, P1HitRex);
        Messenger.AddListener(GameEvent.P2_HIT_REX, P2HitRex);
    }

    // Use this for initialization
    void Start () {
		
	}

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.P1_HIT_REX, P1HitRex);
        Messenger.RemoveListener(GameEvent.P2_HIT_REX, P2HitRex);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown("c"))
        {
            HitRex(0);
        }
    }

    private void HitRex(int playerNumber)
    {
        // Change to hit camera
        hitCamera.enabled = true;
        mainCamera.enabled = false;

        // Get scale and rotation from gamecar
        // Get position from hit car launcher
        hitCarPosition = this.transform.position;
        hitCarScale = gameCarRed.transform.lossyScale;
        hitCarRotation = gameCarRed.transform.rotation;

        // Create new hit object car
        // Position: hit car launcher position
        // Rotation: game car rotation
        if (playerNumber == 0)
        {
            hitObject = (Rigidbody)Instantiate(hitCarRed, hitCarPosition, hitCarRotation);
        } else
        {
            hitObject = (Rigidbody)Instantiate(hitCarBlue, hitCarPosition, hitCarRotation);
        }

        // Set scale based on game car scale
        hitObject.transform.localScale = hitCarScale;

        // Set game object tag
        hitObject.gameObject.tag = "LRHitCar";
        hitObjectCar = hitObject.gameObject;

        launchHitCar(playerNumber);
    }

    private void launchHitCar(int playerNumber)
    {
        // Rotate Hit Car Launcher to face Rex
        // Convert rotation from Euler angles to Quaternion
        Quaternion quaternionRotation = Quaternion.Euler(new Vector3(launchHeightAngle, launchDirection, 0));

        // Change rotation of hit car launcher
        this.transform.rotation = quaternionRotation;
        
        hitObject.AddForce(transform.forward * launchForce);
        
        StartCoroutine(ResetMainCamera(1.6f));
    }

    private IEnumerator ResetMainCamera(float waitTime)
    {
        // Wait N seconds
        yield return new WaitForSeconds(waitTime);

        // Switch back to main camera
        mainCamera.enabled = true;
        hitCamera.enabled = false;

        // Destroy hit car
        Destroy(hitObjectCar);


        yield return new WaitForSeconds(waitTime);
    }

    private void P1HitRex()
    {
        HitRex(0);
    }

    private void P2HitRex()
    {
        HitRex(1);
    }

    public float GetLaunchForce()
    {
        return launchForce;
    }

    public float GetLaunchDirection()
    {
        return launchDirection;
    }

    public float GetLaunchHeightAngle()
    {
        return launchHeightAngle;
    }
}
