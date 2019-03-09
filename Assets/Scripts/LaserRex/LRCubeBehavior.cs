using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRCubeBehavior : MonoBehaviour {

    [SerializeField] private int playerNumber;
    [SerializeField] private int health = 100;
    [SerializeField] private GameObject[] sparks;
    [SerializeField] private GameObject[] jets;

    private int currentHealth;
    private bool hitable = true;
    private Rigidbody rigidBody;
    private LRDrift drift;
    private LRDrift shake;
    private bool defeated = false;

    private Vector3 startPosition;
    
    void Awake()
    {
        // Listen for game-triggered events
        Messenger.AddListener(GameEvent.P1_CUBE_HIT, P1CubeHit);
        Messenger.AddListener(GameEvent.P2_CUBE_HIT, P2CubeHit);
        Messenger.AddListener(GameEvent.P1_REX_DONE_MUNCHING, P1Reenable);
        Messenger.AddListener(GameEvent.P2_REX_DONE_MUNCHING, P2Reenable);
        Messenger.AddListener(GameEvent.REX_DEFEATED, RexDefeated);

        rigidBody = GetComponent<Rigidbody>();

        LRDrift[] drifts = GetComponents<LRDrift>();
        drift = drifts[0];
        shake = drifts[1];
        shake.Stop();

        currentHealth = health;
        startPosition = transform.position;
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.P1_CUBE_HIT, P1CubeHit);
        Messenger.RemoveListener(GameEvent.P2_CUBE_HIT, P2CubeHit);
        Messenger.RemoveListener(GameEvent.P1_REX_DONE_MUNCHING, P1Reenable);
        Messenger.RemoveListener(GameEvent.P2_REX_DONE_MUNCHING, P2Reenable);
        Messenger.RemoveListener(GameEvent.REX_DEFEATED, RexDefeated);
    }

    public int GetPlayerNumber()
    {
        return playerNumber;
    }

    private void CubeHitByLaser()
    {
        currentHealth -= 1;
        if (currentHealth > 0)
        {
            StartCoroutine(Shake());
            StartCoroutine(Spark());
        }
        else
        {
            hitable = false;
            if (playerNumber == 0)
            {
                Messenger.Broadcast(GameEvent.P1_CUBE_NEW_CANDY);
            } else
            {
                Messenger.Broadcast(GameEvent.P2_CUBE_NEW_CANDY);
            }

            startPosition = transform.position;

            DropCube();
            StartCoroutine(StopFalling());
        }
    }

    private IEnumerator Spark()
    {
        foreach (GameObject spark in sparks)
        {
            spark.SetActive(true);
        }

        yield return new WaitForSeconds(.2f);

        if (hitable)
        {

            foreach (GameObject spark in sparks)
            {
                spark.SetActive(false);
            }
        }
    }

    private IEnumerator Shake()
    {
        shake.SetPosition(transform.localPosition);

        drift.Stop();
        shake.Resume();
        yield return new WaitForSeconds(.1f);
        shake.Stop();

        // If not hitable then in drop state
        if (hitable)
        {
            drift.Resume();
        }
    }

    public void DropCube()
    {
        // Drop cube
        drift.Stop();
        shake.Stop();
        rigidBody.useGravity = true;

        foreach (GameObject jet in jets)
        {
            jet.SetActive(false);
        }

        foreach (GameObject spark in sparks)
        {
            spark.SetActive(true);
        }
    }

    private IEnumerator StopFalling()
    {
        while (transform.position.y > -1)
        {
            yield return null;
        }

        // Stop falling
        rigidBody.useGravity = false;
        rigidBody.velocity = new Vector3();
        transform.rotation = new Quaternion();
    }

    private IEnumerator Reenable()
    {
        if (!defeated)
        {
            Vector3 disabledPosition = transform.position;
            float startTime = Time.time;
            float duration = 2.0f;

            foreach (GameObject spark in sparks)
            {
                spark.SetActive(false);
            }

            foreach (GameObject jet in jets)
            {
                jet.SetActive(true);
            }

            // Float back to original position
            while (transform.position.y != startPosition.y)
            {
                float t = (Time.time - startTime) / duration;
                transform.position = new Vector3(Mathf.SmoothStep(disabledPosition.x, startPosition.x, t),
                    Mathf.SmoothStep(disabledPosition.y, startPosition.y, t), startPosition.z);
                yield return null;

                // If rex defeated while floating back up don't keep floating
                if (defeated)
                {
                    yield break;
                }
            }

            drift.Resume();
            currentHealth = health;
            hitable = true;
        }
    }

    private void RexDefeated()
    {
        defeated = true;
    }

    private void P1CubeHit()
    {
        if (hitable && playerNumber == 0)
        {
            Messenger.Broadcast(GameEvent.P1_CUBE_NEW_COIN);
            CubeHitByLaser();
        }
    }

    private void P2CubeHit()
    {
        if (hitable && playerNumber == 1)
        {
            Messenger.Broadcast(GameEvent.P2_CUBE_NEW_COIN);
            CubeHitByLaser();
        }
    }

    private void P1Reenable()
    {
        if (playerNumber == 0)
        {
            StartCoroutine(Reenable());
        }
    }

    private void P2Reenable()
    {
        if (playerNumber == 1)
        {
            StartCoroutine(Reenable());
        }
    }
}
