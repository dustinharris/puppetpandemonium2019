using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRMoveBackgroundObjects : MonoBehaviour {

    [SerializeField] private GameObject sceneryEmpty;
    [SerializeField] private float scenerySpeed=1;
    private float currentSpeed = 0;

    void Awake()
    {
        // Listen for game-triggered events
        Messenger.AddListener(GameEvent.REX_STOP_SCENERY, RexStopScenery);
        Messenger.AddListener(GameEvent.REX_START_SCENERY, RexStartScenery);
    }

    // Use this for initialization
    void Start () {
        RexStartScenery();
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.REX_STOP_SCENERY, RexStopScenery);
        Messenger.RemoveListener(GameEvent.REX_START_SCENERY, RexStartScenery);
    }

    // Update is called once per frame
    void Update () {
        // Determine how fast to move the scenery based on variable
        float newZ = sceneryEmpty.transform.position.z - (float)(currentSpeed * .1);

        // Change scenery position each frame
        sceneryEmpty.transform.position = new Vector3(sceneryEmpty.transform.position.x, sceneryEmpty.transform.position.y, newZ);
	}

    private void RexStartScenery()
    {
        currentSpeed = scenerySpeed;
    }

    private void RexStopScenery()
    {
        currentSpeed = 0;
    }
}
