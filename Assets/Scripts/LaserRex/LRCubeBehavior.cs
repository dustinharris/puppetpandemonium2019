﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRCubeBehavior : MonoBehaviour {

    [SerializeField] private int playerNumber;
    [SerializeField] private int health = 100;

    private int currentHealth;
    private bool hitable = true;
    private Rigidbody rigidBody;
    private LRDrift drift;

    private Vector3 startPosition;
    
    void Awake()
    {
        // Listen for game-triggered events
        Messenger.AddListener(GameEvent.P1_CUBE_HIT, P1CubeHit);
        Messenger.AddListener(GameEvent.P2_CUBE_HIT, P2CubeHit);
        Messenger.AddListener(GameEvent.P1_REX_DONE_MUNCHING, P1Reenable);
        Messenger.AddListener(GameEvent.P2_REX_DONE_MUNCHING, P2Reenable);

        rigidBody = GetComponent<Rigidbody>();
        drift = GetComponent<LRDrift>();

        currentHealth = health;
        startPosition = transform.position;
    }

    public int GetPlayerNumber()
    {
        return playerNumber;
    }

    private void CubeHitByLaser()
    {
        currentHealth -= 1;
        if (currentHealth == 0)
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

            // Drop cube
            drift.Stop();
            rigidBody.useGravity = true;
            StartCoroutine(StopFalling());
        }
    }

    public void EndGameDropCube()
    {
        // Drop cube
        drift.Stop();
        rigidBody.useGravity = true;
    }

    private IEnumerator StopFalling()
    {
        while (transform.position.y > 0)
        {
            yield return null;
        }

        // Stop falling
        rigidBody.useGravity = false;
        rigidBody.velocity = new Vector3();
        transform.rotation = new Quaternion();

        // TODO remove this when rex broadcasts done munching
        yield return new WaitForSeconds(5f);
        Messenger.Broadcast(playerNumber == 0 ? GameEvent.P1_REX_DONE_MUNCHING : GameEvent.P2_REX_DONE_MUNCHING);
    }

    private IEnumerator Reenable()
    {
        Vector3 disabledPosition = transform.position;
        float startTime = Time.time;
        float duration = 2.0f;

        // Float back to original position
        while (transform.position.y != startPosition.y)
        {
            float t = (Time.time - startTime) / duration;
            transform.position = new Vector3(Mathf.SmoothStep(disabledPosition.x, startPosition.x, t),
                Mathf.SmoothStep(disabledPosition.y, startPosition.y, t), startPosition.z);
            yield return null;
        }

        drift.Resume();
        currentHealth = health;
        hitable = true;
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
