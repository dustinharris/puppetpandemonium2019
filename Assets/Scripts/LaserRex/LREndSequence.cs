using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LREndSequence : MonoBehaviour {

    [SerializeField] private GameObject redCube;
    [SerializeField] private GameObject blueCube;
    [SerializeField] private GameObject mamaRex;
    [SerializeField] private GameObject[] scenerySpawnObjects;
    [SerializeField] private GameObject[] mamaRexJets;
    [SerializeField] private bool testEndGame = false;

    void Awake()
    {
        // Listen for game-triggered events
        Messenger.AddListener(GameEvent.REX_INITIATE_END_SEQUENCE, RexInitiateEndSequence);
    }
    
    // Use this for initialization
    void Start () {
		if (testEndGame == true)
        {
            StartCoroutine(TestEndSequence());
        }
	}
	
    public void RexInitiateEndSequence()
    {
        // Turn off audience lasers
        Messenger.Broadcast(GameEvent.REX_DISABLE_AUDIENCE_LASERS);
        
        // Boxes fall away
        redCube.GetComponent<LRCubeBehavior>().DropCube();
        blueCube.GetComponent<LRCubeBehavior>().DropCube();

        // Boss enters defeated state; can no longer shoot
        // This also stops player movement forward
        Messenger.Broadcast(GameEvent.REX_DEFEATED);

        // Destroy scenery spawn game objects
        foreach (GameObject go in scenerySpawnObjects)
        {
            Destroy(go);
        }

        // Stop scenery moving
        Messenger.Broadcast(GameEvent.REX_STOP_SCENERY);
    }

    private IEnumerator TestEndSequence()
    {
        yield return new WaitForSeconds(2f);

        RexInitiateEndSequence();
    }
}
