using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRCandyBehavior : MonoBehaviour {

    [SerializeField] private int PlayerNumber;
    [SerializeField] private GameObject Candy;
    [SerializeField] private GameObject candyParent;

    private void Awake()
    {
        // Listen for game-triggered events
        Messenger.AddListener(GameEvent.P1_CUBE_NEW_CANDY, P1Candy);
        Messenger.AddListener(GameEvent.P2_CUBE_NEW_CANDY, P2Candy);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void GenerateCandy()
    {
        // Instatiate new laser object
        GameObject candy = Instantiate(Candy, this.transform.position, this.transform.rotation, candyParent.transform);
    }


    private void P1Candy()
    {
        if (PlayerNumber == 0)
        {
            GenerateCandy();
        }
    }

    private void P2Candy()
    {
        if (PlayerNumber == 1)
        {
            GenerateCandy();
        }
    }
}
